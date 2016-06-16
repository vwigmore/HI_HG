using Assets.src.model;
using ManusMachina;
using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract hand class, implementable by left/right hand controller
/// </summary>
public abstract class Hand : IHand
{
    #region Fields

    /// <summary>
    /// Controls the vibration of the hand.
    /// </summary>
    public VibrateHand vh;

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
    /// The game and model transforms.
    /// </summary>
    protected Transform[][] gameTransforms, modelTransforms;

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
    /// Numbers used for counting bend fingers.
    /// </summary>
    private static readonly int FOUR = 4, FIVE = 5;

    /// <summary>
    /// Game object hand.
    /// </summary>
    private GameObject hand;

    /// <summary>
    /// The sphere collider.
    /// </summary>
    private SphereCollider sphereCollider;

    /// <summary>
    /// The animation clip.
    /// </summary>
    private AnimationClip animationClip;

    /// <summary>
    /// The last touched gameobject.
    /// </summary>
    private GameObject lastTouched;

    /// <summary>
    /// The correction bends.
    /// </summary>
    private float[] correctionBends;

    #endregion Fields

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Hand"/> class.
    /// </summary>
    /// <param name="glove">The glove.</param>
    /// <param name="RootTransform">The root transform.</param>
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
        this.lastTouched = null;
        this.correctionBends = InitCorrectionBends();

        this.gameTransforms = ManusControl.CreateGameTransforms(RootTransform);
        this.modelTransforms = ManusControl.CreateModelTransforms(hand);

        InitColliders();

        this.manusGrab = new ManusGrab(baseCollider.gameObject, highlightColor, this);
        this.vh = new VibrateHand(glove);

        hand.SetActive(true);
    }

    #endregion Constructors

    #region Methods

    /// <summary>
    /// Initializes the colliders.
    /// </summary>
    public void InitColliders()
    {
        this.colliders = new ArrayList();
        this.colliders.AddRange(HandCollider.InitializeFingerColliders(gameTransforms));
        this.baseCollider = (BoxCollider)HandCollider.CreateHandBaseCollider(RootTransform.gameObject);
        this.colliders.Add(baseCollider);
    }

    /// <summary>
    /// Update the position of the hand according to the arm.
    /// </summary>
    public abstract void UpdatePosition();

    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update(bool[] bends)
    {
        UpdatePosition();
        UpdateHand(bends);
        UpdateGestures();
        vh.Update();
    }

    /// <summary>
    /// Updates the hand.
    /// </summary>
    public void UpdateHand(bool[] bends)
    {
        Quaternion q = glove.Quaternion;
        float[] fingers = glove.Fingers;
        RootTransform.localRotation = q;

        for (int i = 0; i < fingers.Length; i++)
        {
            if (fingers[i] > correctionBends[i])
                correctionBends[i] = bends[i] ? fingers[i] : correctionBends[i];
            else
                correctionBends[i] = fingers[i];
        }

        UpdateFingers(correctionBends, bends);
    }

    /// <summary>
    /// Initializes the correction bends.
    /// </summary>
    /// <returns></returns>
    public float[] InitCorrectionBends()
    {
        float[] res = new float[this.glove.Fingers.Length];
        for (int i = 0; i < this.glove.Fingers.Length; i++)
            res[i] = this.glove.Fingers[i];

        return res;
    }

    /// <summary>
    /// Updates the gestures.
    /// </summary>
    public virtual void UpdateGestures()
    {
        IMoveGesture gesture = GestureController.GetGesture(this.glove);
        gesture.UpdateGrabbed(baseCollider.transform.rotation, this.manusGrab);
    }

    // Update is called once per frame
    /// <summary>
    /// Updates all fingers.
    /// </summary>
    /// <param name="f">Array of floats containing fingers.</param>
    public void UpdateFingers(float[] f, bool[] bend)
    {
        float avgbend = 0.0f;
        for (int i = 0; i < FIVE; i++)
        {
            animationClip.SampleAnimation(hand, f[i] * timeFactor);
            avgbend += f[i];
            for (int j = 0; j < FOUR; j++)
            {
                gameTransforms[i][j].localRotation = modelTransforms[i][j].localRotation;
            }
        }
        avgbend /= 4;

        float thumbvalue = (avgbend > f[0]) ? avgbend : f[0];
        animationClip.SampleAnimation(hand, thumbvalue * timeFactor);
        for (int j = 0; j < FOUR; j++)
        {
            gameTransforms[0][j].localRotation = modelTransforms[0][j].localRotation;
        }
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
            vh.Vibrate();
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