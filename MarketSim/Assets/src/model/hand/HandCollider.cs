using Assets.src.model;
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Used to generate bounding boxes for the hands.
/// </summary>
public static class HandCollider
{
    #region Fields

    /// <summary>
    /// The size of the base collider.
    /// This is a collider for the palm of the hand.
    /// </summary>
    private static Vector3 baseColliderSize = new Vector3(0.1f, 0.02f, 0.1f);

    #endregion Fields

    #region Methods

    /// <summary>
    /// Creates the hand base collider.
    /// </summary>
    /// <returns>The hand base collider.</returns>
    /// <param name="root">Root.</param>
    public static Collider CreateHandBaseCollider(GameObject root)
    {
        BoxCollider baseCollider = new BoxCollider();
        baseCollider = root.AddComponent<BoxCollider>();
        baseCollider.size = baseColliderSize;
        baseCollider.center = TranslateBaseColliderPos(baseCollider.center);
        return baseCollider;
    }

    /// <summary>
    /// Translates the base collider position.
    /// The base collider will be translated to fit into the palm
    /// of the hand.
    /// </summary>
    /// <returns>The base collider position.</returns>
    /// <param name="pos">Position.</param>
    public static Vector3 TranslateBaseColliderPos(Vector3 pos)
    {
        pos.x -= 0.05f;
        pos.y -= 0.02f;
        return pos;
    }

    /// <summary>
    /// Initializes the finger colliders.
    /// For finger tips, create bigger sphere colliders.
    /// For other parts of the fingers,
    /// 	create box colliders which are cheaper to calculate with.
    /// Colliders are added to an arraylist, for future reference.
    /// </summary>
    /// <returns>The finger colliders.</returns>
    /// <param name="gameTransforms">Game transforms.</param>
    public static ArrayList InitializeFingerColliders(Transform[][] gameTransforms)
    {
        ArrayList colliders = new ArrayList();
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 4; j++)
            {
                Collider collider;
                if (j == 3)
                {
                    collider = CreateFingerTipCollider(gameTransforms[i][j].gameObject);
                    colliders.Add(collider);
                }
                else
                {
                    collider = CreateFingerPartCollider(gameTransforms[i][j].gameObject);
                }
            }
        return colliders;
    }

    /// <summary>
    /// Creates the finger tip collider.
    /// </summary>
    /// <returns>The finger tip collider.</returns>
    /// <param name="root">Root.</param>
    public static Collider CreateFingerTipCollider(GameObject root)
    {
        SphereCollider s = new SphereCollider();
        s = root.AddComponent<SphereCollider>();
        s.radius = .02f;
        return s;
    }

    /// <summary>
    /// Creates the finger part collider.
    /// These colliders are for the non-finger tip parts of the fingers.
    /// </summary>
    /// <returns>The finger part collider.</returns>
    /// <param name="root">Root.</param>
    public static Collider CreateFingerPartCollider(GameObject root)
    {
        BoxCollider b = new BoxCollider();
        b = root.AddComponent<BoxCollider>();
        b.size = new Vector3(.02f, .02f, .02f);
        return b;
    }

    #endregion Methods
}