using Assets.src.model;
using ManusMachina;
using System.Collections;
using UnityEngine;

public class HandFactory
{
    #region Methods

    /// <summary>
    /// Creates the hand.
    /// </summary>
    /// <param name="glovetype">The glove type (left or right)s.</param>
    /// <param name="rt">The RootTransformation.</param>
    /// <returns></returns>
    public static IHand createHand(GLOVE_HAND glovetype, Transform rt)
    {
        if (glovetype == GLOVE_HAND.GLOVE_LEFT)
        {
            return createLeft(glovetype, rt);
        }
        else
        {
            return createRight(glovetype, rt);
        }
    }

    private static IHand createLeft(GLOVE_HAND gt, Transform rt)
    {
        Glove glove = new Glove(gt);
        GameObject handModel = GameObject.Find("Manus_Handv2_Left");
        GameObject root = GameObject.Find("13_Hand_Left");
        GameObject handResource = Resources.Load<GameObject>("Manus_Handv2_Left");
        AnimationClip animation = Resources.Load<AnimationClip>("Manus_Handv2_Left");
        VibrateHand vh = new VibrateHand();
        return new LeftHand(glove, rt, handModel, root, handResource, animation, Color.green, vh);
    }

    private static IHand createRight(GLOVE_HAND gt, Transform rt)
    {
        Glove glove = new Glove(gt);
        GameObject handModel = GameObject.Find("Manus_Handv2_Right");
        GameObject root = GameObject.Find("23_Hand_Right");
        GameObject handResource = Resources.Load<GameObject>("Manus_Handv2_Right");
        AnimationClip animation = Resources.Load<AnimationClip>("Manus_Handv2_Right");
        VibrateHand vh = new VibrateHand();
        return new RightHand(glove, rt, handModel, root, handResource, animation, Color.red, vh);
    }

    #endregion Methods
}