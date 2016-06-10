using Assets.src.model;
using ManusMachina;
using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract hand class, implementable by left/righthand controller
/// </summary>
public abstract class Hand : IHand
{
    #region Fields

    /// <summary>
    /// The root transform
    /// </summary>
    public Transform RootTransform;

    /// <summary>
    /// Left or right hand.
    /// </summary>
    protected GLOVE_HAND glove_hand;

    /// <summary>
    /// Manus Glove instance.
    /// </summary>
    protected Glove glove;

    /// <summary>
    /// The root (wrist) of the hand.
    /// </summary>
    protected GameObject root;

    /// <summary>
    /// The hand model
    /// </summary>
    protected GameObject handModel;

    /// <summary>
    /// The manus grab
    /// </summary>
    protected ManusGrab manusGrab;

    /// <summary>
    /// The game transforms
    /// </summary>
    protected Transform[][] gameTransforms;

    protected BoxCollider baseCollider;

    /// <summary>
    /// The time factor
    /// </summary>
    private const float timeFactor = 10.0f;

    /// <summary>
    /// The bend threshold.
    /// </summary>
    private const float BendThreshold = 0.4f;

    /// <summary>
    /// The sphere collider radius
    /// </summary>
    private const float SphereColliderRadius = 0.035f;

    /// <summary>
    /// The vibrate time
    /// </summary>
    private readonly float vibrateTime = (float)Manager.VibrationTime / 1000;

    /// <summary>
    /// The vibration power
    /// </summary>
    private readonly float vibrationForce = Manager.VibrationForce;

    /// <summary>
    /// The base hand collider size
    /// </summary>
    private Vector3 BaseHandColliderSize = new Vector3(0.1f, 0.05f, 0.10f);

    /// <summary>
    /// Game object hand.
    /// </summary>
    private GameObject hand;

    /// <summary>
    /// The sphere collider
    /// </summary>
    private SphereCollider sphereCollider;

    /// <summary>
    /// The model transforms
    /// </summary>
    private Transform[][] modelTransforms;

    /// <summary>
    /// The animation clip
    /// </summary>
    private AnimationClip animationClip;

    /// <summary>
    /// The select color.
    /// </summary>
    private Color highlightColor;

    /// <summary>
    /// The timer
    /// </summary>
    private float timer;

    /// <summary>
    /// The last touched gameobject
    /// </summary>
    private GameObject lastTouched;

    /// <summary>
    /// Bool if the glove should be vibrated.
    /// </summary>
    private bool vibrateGlove;

    #endregion Fields

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Hand"/> class.
    /// </summary>
    /// <param name="handModel">The hand model.</param>
    /// <param name="handRoot">The hand root.</param>
    /// <param name="hand">The hand.</param>
    /// <param name="animation">The animation.</param>
    /// <param name="highlightColor">Color of the highlight.</param>
    public Hand(Glove glove, Transform RootTransform, GameObject handModel, GameObject handRoot, GameObject hand, AnimationClip animation, Color highlightColor)
    {
        Manus.ManusInit();
        this.glove = glove;
        this.RootTransform = RootTransform;
        this.handModel = handModel;
        this.root = handRoot;
        this.hand = hand;
        this.animationClip = animation;
        this.highlightColor = highlightColor;
        this.timer = 0f;
        this.vibrateGlove = false;
        this.lastTouched = null;

        InitTransforms();
        CreateColliders();

        this.manusGrab = new ManusGrab(baseCollider.gameObject, highlightColor);

        hand.SetActive(true);
    }

    #endregion Constructors

    #region Enums

    /// <summary>
    /// Contains count of fingers bent 0-5.
    /// </summary>
    public enum FingersBent { zero = 0, one = 1, two = 2, three = 3, four = 4, five = 5 }

    #endregion Enums

    #region Methods

    /// <summary>
    /// Update the position of the hand according to the arm.
    /// </summary>
    public abstract void UpdatePosition();

    /// <summary>
    /// Updates the hand.
    /// </summary>
    public void UpdateHand()
    {
        Quaternion q = glove.Quaternion;
        float[] fingers = glove.Fingers;
        RootTransform.localRotation = q;

        UpdateFingers(fingers);
    }

    /// <summary>
    /// Updates all fingers.
    /// </summary>
    /// <param name="f">Array of floats containing fingers.</param>
    public void UpdateFingers(float[] f)
    {
        for (int i = 0; i < (int)FingersBent.five; i++)
        {
            animationClip.SampleAnimation(hand, f[i] * timeFactor);
            for (int j = 0; j < (int)FingersBent.four; j++)
            {
                gameTransforms[i][j].localRotation = modelTransforms[i][j].localRotation;
            }
        }
    }

    /// <summary>
    /// Updates the gestures.
    /// </summary>
    public virtual void UpdateGestures()
    {
        Gestures gesture = GetGesture();
        if (!manusGrab.IsGrabbing())
        {
            if (gesture == Gestures.Grab)
            {
                manusGrab.GrabHighlightedObject();
                manusGrab.SetPrevGrabberRot(baseCollider.transform.rotation);
            }
        }
        else
        {
            if (gesture == Gestures.Open)
                manusGrab.DropObject();
        }
    }

    /// <summary>
    /// Returns which gesture the hand is making.
    /// </summary>
    /// <returns>Gesture the hand is making</returns>
    public Gestures GetGesture()
    {
        int fingersBent = 0;
        for (int i = 0; i < (int)FingersBent.five; i++)
        {
            if (this.glove.Fingers[i] >= BendThreshold)
            {
                fingersBent++;
            }
        }
        return GetGesturesHelp(fingersBent);
    }

    /// <summary>
    /// Returns a gesture by checking the number of fingers bent.
    /// </summary>
    /// <param name="fingersBent">The number of fingers bent.</param>
    /// <returns></returns>
    public Gestures GetGesturesHelp(int fingersBent)
    {
        if (fingersBent == (int)FingersBent.five)
            return Gestures.Grab;
        else if (fingersBent == (int)FingersBent.four && glove.Fingers[0] < 0.4f)
            return Gestures.Thumb;
        else if (fingersBent == (int)FingersBent.four && glove.Fingers[4] < 0.4f)
            return Gestures.Pinky;
        else if (glove.Fingers[1] < 0.4f && glove.Fingers[2] < 0.4f && fingersBent == (int)FingersBent.three)
            return Gestures.Point;
        else if (fingersBent <= (int)FingersBent.one)
            return Gestures.Open;
        return Gestures.None;
    }

    /// <summary>
    /// Creates the grab collider.
    /// </summary>
    public void CreateColliders()
    {
        baseCollider = new BoxCollider();
        baseCollider = gameTransforms[0][0].parent.gameObject.AddComponent<BoxCollider>();
        baseCollider.size = BaseHandColliderSize;
        Vector3 pos2 = baseCollider.center;

        pos2 = TranslateHandBoundingBox(pos2);

        baseCollider.center = pos2;
        baseCollider.isTrigger = true;

        BoxCollider rigid = new BoxCollider();
        rigid = gameTransforms[0][0].parent.gameObject.AddComponent<BoxCollider>();
        Vector3 rigidsize = new Vector3(0.15f, 0.02f, 0.15f);
        Vector3 rigidpos = rigid.center;
        rigidpos.x -= .05f;
        rigid.size = rigidsize;
        rigid.center = rigidpos;
    }

    /// <summary>
    /// Helper method for creating a collider.
    /// </summary>
    /// <param name="pos">The position.</param>
    public Vector3 TranslateHandBoundingBox(Vector3 pos)
    {
        pos.x -= .05f;
        pos.y -= .1f;
        return pos;
    }

    /// <summary>
    /// Returns the ManusGrabs.
    /// </summary>
    /// <returns>ManusGrab</returns>
    public ManusGrab GetManusGrab()
    {
        return this.manusGrab;
    }

    /// <summary>
    /// Initializes the transforms.
    /// </summary>
    public void InitTransforms()
    {
        gameTransforms = new Transform[(int)FingersBent.five][];
        modelTransforms = new Transform[(int)FingersBent.five][];
        for (int i = 0; i < (int)FingersBent.five; i++)
        {
            gameTransforms[i] = new Transform[(int)FingersBent.four];
            modelTransforms[i] = new Transform[(int)FingersBent.four];
            for (int j = 0; j < (int)FingersBent.four; j++)
            {
                gameTransforms[i][j] = FindDeepChild(RootTransform, "Finger_" + i.ToString() + j.ToString());
                modelTransforms[i][j] = FindDeepChild(hand.transform, "Finger_" + i.ToString() + j.ToString());

                BoxCollider b = new BoxCollider();
                b = gameTransforms[i][j].gameObject.AddComponent<BoxCollider>();
                b.size = new Vector3(.03f, .03f, .03f);
            }
        };
    }

    /// <summary>
    /// Touches the specified object.
    /// </summary>
    /// <param name="obj">The object.</param>
    public void Touch(GameObject obj)
    {
        if ((lastTouched == null || !lastTouched.Equals(obj))
            && Manager.EnableVibration)
        {
            Vibrate();
            lastTouched = obj;
        }
        else
        {
            lastTouched = null;
        }
    }

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void ResetTimer()
    {
        timer = 0;
    }

    /// <summary>
    /// Updates the timer.
    /// </summary>
    public void UpdateTimer()
    {
        timer += Time.deltaTime;
    }

    /// <summary>
    /// Updates the vibration of the glove.
    /// </summary>
    public void UpdateVibration()
    {
        if (timer <= vibrateTime && vibrateGlove)
        {
            glove.SetVibration(vibrationForce);
        }
        else
        {
            glove.SetVibration(0.0f);
            ResetTimer();
            vibrateGlove = false;
        }
    }

    /// <summary>
    /// Vibrates this glove.
    /// </summary>
    public void Vibrate()
    {
        vibrateGlove = true;
    }

    /// <summary>
    /// Finds a deep child in a transform
    /// </summary>
    /// <param name="aParent">Transform to be searched</param>
    /// <param name="aName">Name of the (grand)child to be found</param>
    /// <returns></returns>
    private static Transform FindDeepChild(Transform aParent, string aName)
    {
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = FindDeepChild(child, aName);
            if (result != null)
                return result;
        }
        return null;
    }

    /// <summary>
    /// When application is terminated, cancel Manus communication.
    /// </summary>
    private void OnApplicationQuit()
    {
        Manus.ManusExit();
    }

    #endregion Methods
}