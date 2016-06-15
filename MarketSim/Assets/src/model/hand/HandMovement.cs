using UnityEngine;
using System.Collections;

public class HandMovement : MonoBehaviour
{

    #region Fields

    /// <summary>
    /// Four.
    /// </summary>
    private static readonly int FOUR = 4;

    /// <summary>
    /// Five.
    /// </summary>
    private static readonly int FIVE = 5;

    /// <summary>
    /// The animation clip.
    /// </summary>
    public AnimationClip animationClip;

    /// <summary>
    /// The model transforms.
    /// </summary>
    private Transform[][] modelTransforms;

    /// <summary>
    /// The game transforms.
    /// </summary>
    protected Transform[][] gameTransforms;

    /// <summary>
    /// The colliders.
    /// </summary>
    protected ArrayList colliders;

    /// <summary>
    /// Game object hand.
    /// </summary>
    public GameObject hand;

    /// <summary>
    /// The time factor.
    /// </summary>
    private const float timeFactor = 10.0f;

    /// <summary>
    /// The root transform.
    /// </summary>
    public Transform RootTransform;

    #endregion Fields

    #region Constructor

    // Use this for initialization
    public HandMovement()
    {
        InitTransforms();
    }
	
    #endregion Constructor

    #region Methods

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
    /// Initializes the transforms.
    /// </summary>
    public void InitTransforms()
    {
        gameTransforms = new Transform[FIVE][];
        modelTransforms = new Transform[FIVE][];
        for (int i = 0; i < FIVE; i++)
        {
            gameTransforms[i] = new Transform[FOUR];
            modelTransforms[i] = new Transform[FOUR];
            for (int j = 0; j < FOUR; j++)
            {
                gameTransforms[i][j] = FindDeepChild(RootTransform, "Finger_" + i.ToString() + j.ToString());
                modelTransforms[i][j] = FindDeepChild(hand.transform, "Finger_" + i.ToString() + j.ToString());

                if (j == 3)
                {
                    SphereCollider s = new SphereCollider();
                    s = gameTransforms[i][j].gameObject.AddComponent<SphereCollider>();

                    if (i == 0)
                        s.radius = .025f;
                    else s.radius = .015f;

                    colliders.Add(s);
                }
                else
                {
                    BoxCollider b = new BoxCollider();
                    b = gameTransforms[i][j].gameObject.AddComponent<BoxCollider>();
                    b.size = new Vector3(.02f, .02f, .02f);
                }
            }
        };
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
    #endregion Methods

  

}
