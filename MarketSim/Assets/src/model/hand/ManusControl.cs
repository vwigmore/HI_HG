using UnityEngine;
using System.Collections;

public class ManusControl : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Game object hand.
    /// </summary>
    private GameObject hand;

    /// <summary>
    /// The root transform.
    /// </summary>
    public Transform RootTransform;

    /// <summary>
    /// Magic numbers.
    /// </summary>
    private readonly int FOUR = 4, FIVE = 5;

    /// <summary>
    /// The game transforms.
    /// </summary>
    protected Transform[][] gameTransforms;

    /// <summary>
    /// The model transforms.
    /// </summary>
    private Transform[][] modelTransforms;

    /// <summary>
    /// The colliders.
    /// </summary>
    protected ArrayList colliders;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="ManusControl"/> class.
    /// </summary>
    /// <param name="rootTransform">The root transform.</param>
    /// <param name="hand">The hand.</param>
    public ManusControl(Transform rootTransform, GameObject hand)
    {
        this.RootTransform = rootTransform;
        this.hand = hand;
        this.gameTransforms = CreateGameTransforms();
        this.modelTransforms = CreateModelTransforms();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Creates the game transforms.
    /// </summary>
    /// <returns></returns>
    public  Transform[][] CreateGameTransforms()
    {
        gameTransforms = new Transform[FIVE][];
        for (int i = 0; i < FIVE; i++)
        {
            gameTransforms[i] = new Transform[FOUR];
            for (int j = 0; j < FOUR; j++)
            {
                gameTransforms[i][j] = FindDeepChild(RootTransform, "Finger_" + i.ToString() + j.ToString());

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
        return gameTransforms;
    }

    /// <summary>
    /// Creates the model transforms.
    /// </summary>
    /// <returns></returns>
    public Transform[][] CreateModelTransforms()
    {
          modelTransforms = new Transform[FIVE][];
        for (int i = 0; i < FIVE; i++)
        {
            modelTransforms[i] = new Transform[FOUR];
            for (int j = 0; j < FOUR; j++)
            {
                modelTransforms[i][j] = FindDeepChild(hand.transform, "Finger_" + i.ToString() + j.ToString());

            }
        }
        return modelTransforms;
    }

    /// <summary>
    /// Finds the deep child. 
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