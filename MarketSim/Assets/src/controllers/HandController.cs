using Assets.src.model;
using ManusMachina;
using System.Collections;
using UnityEngine;

public class HandController : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// The root transform
    /// </summary>
    public Transform RootTransform;

    /// <summary>
    /// The glove_hand
    /// </summary>
    public GLOVE_HAND glove_hand;

    /// <summary>
    /// The hand
    /// </summary>
    private IHand hand;

    #endregion Fields

    #region Methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        Glove glove = new Glove(glove_hand); ;
        GameObject handModel;
        GameObject root;
        GameObject handResource;
        AnimationClip animation;
        Color hl;

        if (glove_hand == GLOVE_HAND.GLOVE_LEFT)
        {
            handModel = GameObject.Find("Manus_Handv2_Left");
            root = GameObject.Find("13_Hand_Left");
            handResource = Resources.Load<GameObject>("Manus_Handv2_Left");
            animation = Resources.Load<AnimationClip>("Manus_Handv2_Left");
            hl = Color.green;
            hand = new LeftHand(glove, RootTransform, handModel, root, handResource, animation, hl);
        }
        else
        {
            handModel = GameObject.Find("Manus_Handv2_Right");
            root = GameObject.Find("23_Hand_Right");
            handResource = Resources.Load<GameObject>("Manus_Handv2_Right");
            animation = Resources.Load<AnimationClip>("Manus_Handv2_Right");
            hl = Color.red;
            hand = new RightHand(glove, RootTransform, handModel, root, handResource, animation, hl);
        }
    }

    /// <summary>
    ///  Update is called once per frame.
    /// </summary>
    private void Update()
    {
        hand.UpdatePosition();
        hand.UpdateHand();
        hand.UpdateGestures();
    }

    /// <summary>
    /// Called when [trigger enter].
    /// </summary>
    /// <param name="collision">The collision.</param>
    private void OnTriggerEnter(Collider collision)
    {
        GameObject collisionObj = collision.gameObject;

        hand.GetManusGrab().HighlightSelectedObject(collisionObj);
    }

    #endregion Methods
}