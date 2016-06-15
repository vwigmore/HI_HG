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
    /// Hand Vibration.
    /// </summary>
    public VibrateHand vibrateHand;
    /// <summary>
    /// Hand Movement.
    /// </summary>
    private HandMovement handMovement;

    /// <summary>
    /// The root transform.
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
    /// The hand model.
    /// </summary>
    protected GameObject handModel;

    /// <summary>
    /// The manus grab.
    /// </summary>
    protected ManusGrab manusGrab;

    /// <summary>
    /// The colliders.
    /// </summary>
    protected ArrayList colliders;

    /// <summary>
    /// The game transforms.
    /// </summary>
    protected Transform[][] gameTransforms;

    /// <summary>
    /// The base collider.
    /// </summary>
    protected BoxCollider baseCollider;

    /// <summary>
    /// The time factor.
    /// </summary>
    private const float timeFactor = 10.0f;

    /// <summary>
    /// The bend threshold.
    /// </summary>
    private const float BendThreshold = 0.4f;

    /// <summary>
    /// The sphere collider radius.
    /// </summary>
    private const float SphereColliderRadius = 0.035f;
    
    /// <summary>
    /// Game object hand.
    /// </summary>
    private GameObject hand;

    /// <summary>
    /// The sphere collider.
    /// </summary>
    private SphereCollider sphereCollider;

    /// <summary>
    /// The model transforms.
    /// </summary>
    private Transform[][] modelTransforms;

    /// <summary>
    /// The animation clip.
    /// </summary>
    private AnimationClip animationClip;

    /// <summary>
    /// The select color.
    /// </summary>
    private Color highlightColor;

    /// <summary>
    /// The last touched gameobject.
    /// </summary>
    private GameObject lastTouched;

    /// <summary>
    /// The correction bends.
    /// </summary>
    private float[] correctionBends;

    /// <summary>
    /// One.
    /// </summary>
    private static readonly int ONE = 1, TWO = 2, THREE = 3, FOUR = 4, FIVE = 5;

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
        this.lastTouched = null;

        this.correctionBends = new float[glove.Fingers.Length];
        for (int i = 0; i < glove.Fingers.Length; i++)
            correctionBends[i] = glove.Fingers[i];

        this.colliders = new ArrayList();

        //init die transforms

        //basehandcollider
        colliders.Add(HandCollider.CreateColliders(gameTransforms[0][0].parent.gameObject));

        this.manusGrab = new ManusGrab(baseCollider.gameObject, highlightColor, this);

        hand.SetActive(true);
    }

    #endregion Constructors

    #region Methods

    /// <summary>
    /// Update the position of the hand according to the arm.
    /// </summary>
    public abstract void UpdatePosition();

    /// <summary>
    /// Updates the hand.
    /// </summary>
    public void UpdateHand(bool[] bend)
    {
        Quaternion q = glove.Quaternion;
        float[] fingers = glove.Fingers;
        RootTransform.localRotation = q;

        for (int i = 0; i < fingers.Length; i++)
        {
            if (fingers[i] > correctionBends[i])
                correctionBends[i] = bend[i] ? fingers[i] : correctionBends[i];
            else
                correctionBends[i] = fingers[i];
        }

        //hm.UpdateFingers(correctionBends, bend);
    }

    /// <summary>
    /// Updates the gestures.
    /// </summary>
    public virtual void UpdateGestures()
    {
        Gestures gesture = GetGesture();

        if (manusGrab.IsGrabbing())
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
        for (int i = 0; i < FIVE; i++)
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
    /// <returns>Gesture the hand is making</returns>
    public Gestures GetGesturesHelp(int fingersBent)
    {
        if (fingersBent == FIVE)
            return Gestures.Grab;
        else if (fingersBent == FOUR && glove.Fingers[0] < 0.4f)
            return Gestures.Thumb;
        else if (fingersBent == FOUR && glove.Fingers[4] < 0.4f)
            return Gestures.Pinky;
        else if (glove.Fingers[1] < 0.4f && glove.Fingers[2] < 0.4f && fingersBent == THREE)
            return Gestures.Point;
        else if (fingersBent <= (int)ONE)
            return Gestures.Open;
        return Gestures.None;
    }

    /// <summary>
    /// Returns the ManusGrab.
    /// </summary>
    /// <returns>ManusGrab</returns>
    public ManusGrab GetManusGrab()
    {
        return this.manusGrab;
    }

    /// <summary>
    /// Checks if an object is touched.
    /// </summary>
    /// <param name="obj">The object.</param>
    public void Touch(GameObject obj)
    {
        if ((lastTouched == null || !lastTouched.Equals(obj))
            && Manager.EnableVibration)
        {
            //vh.Vibrate();
            lastTouched = obj;
        }
        else
        {
            lastTouched = null;
        }
    }

    /// <summary>
    /// Gets the colliders.
    /// </summary>
    /// <returns>The colliders</returns>
    public ArrayList GetColliders()
    {
        return this.colliders;
    }

    /// <summary>
    /// Gets the root transform.
    /// </summary>
    /// <returns></returns>
    public Transform GetRootTransform()
    {
        return this.RootTransform;
    }

    /// <summary>
    /// Gets the position.
    /// </summary>
    /// <returns>The position</returns>
    public Vector3 GetPosition()
    {
        return this.root.transform.position;
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