using UnityEngine;
using System.Collections;
using ManusMachina;
using Assets.src.model;

public class HandController : MonoBehaviour {

    #region Fields

    /// <summary>
    /// Left or right hand.
    /// </summary>
    public GLOVE_HAND glove_hand;

    public Transform RootTransform;

    /// <summary>
    /// Manus Glove instance.
    /// </summary>
    public Glove glove;

    private const float timeFactor = 10.0f;

    /// <summary>
    /// The root (wrist) of the hand.
    /// </summary>
    private GameObject root;

    /// <summary>
    /// Game object hand.
    /// </summary>
    private GameObject hand;

    private GameObject handModel;

    private ManusGrab manusGrab;
    private SphereCollider sphereCollider;

    private Transform[][] gameTransforms;
    private Transform[][] modelTransforms;
    private AnimationClip animationClip;

    private enum Gesture { none, grab, open, point, thumb, twoFingersPoint };

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

    private void UpdateHand()
    {
        Quaternion q = glove.Quaternion;
        float[] fingers = glove.Fingers;
        RootTransform.localRotation = q;

        for (int i = 0; i < 5; i++)
        {
            animationClip.SampleAnimation(hand, fingers[i] * timeFactor);
            for (int j = 0; j < 4; j++)
            {
                gameTransforms[i][j].localRotation = modelTransforms[i][j].localRotation;
            }
        }
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start() {
        Manus.ManusInit();
        this.glove = new Glove(this.glove_hand);

        if (glove_hand == GLOVE_HAND.GLOVE_LEFT)
        {
            this.hand = Resources.Load<GameObject>("Manus_Handv2_Left");
            this.animationClip = Resources.Load<AnimationClip>("Manus_Handv2_Left");
            this.handModel = GameObject.Find("Manus_Handv2_Left");
            this.root = GameObject.Find("left_wrist");
        } else if (glove_hand == GLOVE_HAND.GLOVE_RIGHT)
        {
            this.hand = Resources.Load<GameObject>("Manus_Handv2_Right");
            this.animationClip = Resources.Load<AnimationClip>("Manus_Handv2_Right");
            this.handModel = GameObject.Find("Manus_Handv2_Right");
            this.root = GameObject.Find("right_wrist");
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
        for (int i = 0; i < 5; i++)
        {
            gameTransforms[i] = new Transform[4];
            modelTransforms[i] = new Transform[4];
            for (int j = 0; j < 4; j++)
            {
                gameTransforms[i][j] = FindDeepChild(RootTransform, "Finger_" + i.ToString() + j.ToString());
                modelTransforms[i][j] = FindDeepChild(hand.transform, "Finger_" + i.ToString() + j.ToString());
            }
        };
        hand.SetActive(true);

        Debug.Log(this.glove + "\t" + this.glove_hand);

    }


    private void OnTriggerEnter(Collider collision)
    {
        GameObject collideObj = collision.gameObject;
        manusGrab.highlightSelectedObject(collideObj);
    }

    /// <summary>
    ///  Update is called once per frame.
    /// </summary>
    private void Update()
    {
        this.UpdatePosition();
        this.UpdateHand();

        Gesture gesture = getGesture();

        if (!manusGrab.isGrabbing())
        {
            if (gesture == Gesture.grab)
                manusGrab.grabHighlightedObject();
        }
        else
        {
            if (gesture == Gesture.open)
                manusGrab.dropObject();
        }

        if (glove_hand == GLOVE_HAND.GLOVE_LEFT)
        {
            if (gesture == Gesture.point)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().walkForward();

            if (gesture == Gesture.thumb)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().walkBackwards();

            if (gesture == Gesture.twoFingersPoint)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().rotateRight();
        }
        manusGrab.updateGrabbedObject();
    }

    /// <summary>
    /// Returns which gesture the hand is making.
    /// </summary>
    /// <returns>Gesture the hand is making</returns>
    private Gesture getGesture()
    {
        int fingersBent = 0;
        for (int i = 0; i < 5; i++)
        {
            if (this.glove.Fingers[i] >= 0.3f)
            {
                fingersBent++;
            }
        }

        if (fingersBent == 5)
            return Gesture.grab;
        else if (fingersBent == 3 && glove.Fingers[1] <= 0.3 && glove.Fingers[2] <= 0.3)
            return Gesture.twoFingersPoint;
        else if (fingersBent < 4)
            return Gesture.open;
        else if (fingersBent == 4 && glove.Fingers[1] <= 0.3)
            return Gesture.point;
        else if (fingersBent == 4 && glove.Fingers[0] <= 0.3)
            return Gesture.thumb;

        return Gesture.none;
    }

    /// <summary>
    /// Update the position of the hand according to the arm.
    /// </summary>
    private void UpdatePosition()
    {
        Vector3 newpos = this.root.transform.position;
        this.handModel.transform.position = newpos;
        this.handModel.transform.rotation = this.root.transform.rotation;
        if ( glove_hand == GLOVE_HAND.GLOVE_LEFT)
        {
            this.handModel.transform.Rotate(Vector3.up, -90);
        }
        else if ( glove_hand == GLOVE_HAND.GLOVE_RIGHT)
        {
            this.handModel.transform.Rotate(Vector3.up, 90);
        }
        
        this.handModel.transform.Rotate(Vector3.forward, -90);
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


