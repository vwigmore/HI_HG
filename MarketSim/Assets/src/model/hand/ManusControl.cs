using UnityEngine;
using System.Collections;

public static class ManusControl
{
    #region Fields

    /// <summary>
    /// Magic numbers.
    /// </summary>
	private static readonly int FOUR = 4, FIVE = 5;

	#endregion Fields

    #region Methods

    /// <summary>
    /// Creates the game transforms.
    /// </summary>
    /// <returns></returns>
	public static Transform[][] CreateGameTransforms(Transform rootTransform)
    {
        Transform[][] gameTransforms = new Transform[FIVE][];
        for (int i = 0; i < FIVE; i++)
        {
            gameTransforms[i] = new Transform[FOUR];
            for (int j = 0; j < FOUR; j++)
				gameTransforms[i][j] = FindDeepChild(rootTransform, "Finger_" + i.ToString() + j.ToString());
        };
        return gameTransforms;
    }

    /// <summary>
    /// Creates the model transforms.
    /// </summary>
    /// <returns></returns>
	public static Transform[][] CreateModelTransforms(GameObject hand)
    {
          Transform[][] modelTransforms = new Transform[FIVE][];
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