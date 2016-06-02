using Assets.src.model;
using ManusMachina;
using System.Collections;
using UnityEngine;

public class HandController : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Left or right hand.
    /// </summary>
    public GLOVE_HAND glove_hand;

    /// <summary>
    /// The root transform
    /// </summary>
    public Transform RootTransform;

    /// <summary>
    /// Manus Glove instance.
    /// </summary>
    public Glove glove;

    /// <summary>
    /// The time factor
    /// </summary>
    private const float timeFactor = 10.0f;

    /// <summary>
    /// The root (wrist) of the hand.
    /// </summary>
    private GameObject root;

    /// <summary>
    /// Game object hand.
    /// </summary>
    private GameObject hand;

    /// <summary>
    /// The hand model
    /// </summary>
    private GameObject handModel;

    /// <summary>
    /// The manus grab
    /// </summary>
    private ManusGrab manusGrab;

    /// <summary>
    /// The sphere collider
    /// </summary>
    private SphereCollider sphereCollider;

    /// <summary>
    /// The game transforms
    /// </summary>
    private Transform[][] gameTransforms;

    /// <summary>
    /// The model transforms
    /// </summary>
    private Transform[][] modelTransforms;

    /// <summary>
    /// The animation clip
    /// </summary>
    private AnimationClip animationClip;

    private enum FingersBent { zero = 0, one = 1, two = 2, three = 3, four = 4, five = 5 }

    /// <summary>
    /// the gestures of hand
    /// </summary>
    private enum Gesture { none, grab, open, point, thumb, pinky };

    /// <summary>
    /// the rotation of the player
    /// </summary>
    private enum Rotation { none, right, left };

    #endregion Fields

    #region Methods

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
    /// Updates the hand.
    /// </summary>
    private void UpdateHand()
    {
        Quaternion q = glove.Quaternion;
        float[] fingers = glove.Fingers;
        RootTransform.localRotation = q;

        for (int i = 0; i < (int)FingersBent.five; i++)
        {
            animationClip.SampleAnimation(hand, fingers[i] * timeFactor);
            for (int j = 0; j < (int)FingersBent.four; j++)
            {
                gameTransforms[i][j].localRotation = modelTransforms[i][j].localRotation;
            }
        }
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        Manus.ManusInit();
        this.glove = new Glove(this.glove_hand);

        if (glove_hand == GLOVE_HAND.GLOVE_LEFT)
        {
            this.hand = Resources.Load<GameObject>("Manus_Handv2_Left");
            this.animationClip = Resources.Load<AnimationClip>("Manus_Handv2_Left");
            this.handModel = GameObject.Find("Manus_Handv2_Left");
            this.root = GameObject.Find("13_Hand_Left");
        }
        else if (glove_hand == GLOVE_HAND.GLOVE_RIGHT)
        {
            this.hand = Resources.Load<GameObject>("Manus_Handv2_Right");
            this.animationClip = Resources.Load<AnimationClip>("Manus_Handv2_Right");
            this.handModel = GameObject.Find("Manus_Handv2_Right");
            this.root = GameObject.Find("23_Hand_Right");
        }

        this.manusGrab = new ManusGrab(this.handModel, Color.green);

        //this.handModel.AddComponent<Rigidbody>();
        this.sphereCollider = this.handModel.AddComponent<SphereCollider>();
        this.sphereCollider.radius = 0.1f;
        this.sphereCollider.transform.position = handModel.transform.position;
        this.sphereCollider.isTrigger = true;

        // Associate the game transforms with the skeletal model.
        gameTransforms = new Transform[5][];
        modelTransforms = new Transform[5][];
        for (int i = 0; i < (int)FingersBent.five; i++)
        {
            gameTransforms[i] = new Transform[4];
            modelTransforms[i] = new Transform[4];
            for (int j = 0; j < (int)FingersBent.four; j++)
            {
                gameTransforms[i][j] = FindDeepChild(RootTransform, "Finger_" + i.ToString() + j.ToString());
                modelTransforms[i][j] = FindDeepChild(hand.transform, "Finger_" + i.ToString() + j.ToString());
            }
        };
        hand.SetActive(true);

        BoxCollider bc2 = new BoxCollider();
        bc2 = gameTransforms[0][0].parent.gameObject.AddComponent<BoxCollider>();
        bc2.size = new Vector3(0.05f, 0.02f, 0.08f);
        Vector3 pos2 = bc2.center;

        Debug.Log(this.glove + "\t" + this.glove_hand);
    }

    /// <summary>
    /// Called when [trigger enter].
    /// </summary>
    /// <param name="collision">The collision.</param>
    private void OnTriggerEnter(Collider collision)
    {
        GameObject collideObj = collision.gameObject;
        manusGrab.HighlightSelectedObject(collideObj);
    }

    /// <summary>
    /// Called when [trigger exit].
    /// </summary>
    /// <param name="collision">The collision.</param>
    private void OnTriggerExit(Collider collision)
    {
        manusGrab.ClearHighlights();
    }

    /// <summary>
    ///  Update is called once per frame.
    /// </summary>
    private void Update()
    {
        this.UpdatePosition();
        this.UpdateHand();

        Gesture gesture = GetGesture();

        Rotation rotation = getRotation();

        if (!manusGrab.IsGrabbing())
        {
            if (gesture == Gesture.grab)
                manusGrab.GrabHighlightedObject();
        }
        else
        {
            if (gesture == Gesture.open)
                manusGrab.DropObject();
        }

        if (glove_hand == GLOVE_HAND.GLOVE_LEFT && Manager.GestureMovementOn)
        {
            if (gesture == Gesture.thumb)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().walkBackwards();
            if (gesture == Gesture.point)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().walkForward();
            if (gesture == Gesture.pinky)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().rotateRight();
        }

        this.manusGrab.UpdateGrabbedObject(gameTransforms[0][0].parent.gameObject.transform);
        this.manusGrab.basket.UpdateList();

    }

    /// <summary>
    /// Returns which gesture the hand is making.
    /// </summary>
    /// <returns>Gesture the hand is making</returns>
    private Gesture GetGesture()
    {
        int fingersBent = 0;
        for (int i = 0; i < (int)FingersBent.five; i++)
        {
            if (this.glove.Fingers[i] >= 0.4f)
            {
                fingersBent++;
            }
        }

        if (fingersBent == (int)FingersBent.five)
            return Gesture.grab;
        else if (fingersBent == (int)FingersBent.four && glove.Fingers[0] < 0.4f)
            return Gesture.thumb;
        else if (fingersBent == (int)FingersBent.four && glove.Fingers[4] < 0.4f)
            return Gesture.pinky;
        else if (glove.Fingers[1] < 0.4f && glove.Fingers[2] < 0.4f && fingersBent == (int)FingersBent.three)
            return Gesture.point;
        else if (fingersBent == (int)FingersBent.zero)
            return Gesture.open;
        return Gesture.none;
    }

    /// <summary>
    /// Gets the rotation.
    /// </summary>
    /// <returns></returns>
    private Rotation getRotation()
    {
        return Rotation.none;
    }

    /// <summary>
    /// Update the position of the hand according to the arm.
    /// </summary>
    private void UpdatePosition()
    {
        Vector3 newpos = this.root.transform.position;
        this.handModel.transform.position = newpos;
        Vector3 newrot = this.root.transform.rotation.eulerAngles;

        if (glove_hand == GLOVE_HAND.GLOVE_LEFT)
            newrot.y += 90;
        else if (glove_hand == GLOVE_HAND.GLOVE_RIGHT)
            newrot.y -= 180;

        this.handModel.transform.rotation = Quaternion.Euler(newrot);
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