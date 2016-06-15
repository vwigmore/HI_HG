using Assets.src.model;
using UnityEngine;
using System.Collections;


public class HandCollider : MonoBehaviour {
    
    /// <summary>
    /// The base hand collider size.
    /// </summary>
    private static Vector3 BaseHandColliderSize = new Vector3(0.1f, 0.02f, 0.1f);

    /// <summary>
    /// Creates the grab collider.
    /// </summary>
    public static Collider CreateColliders(GameObject ob)
    {
        BoxCollider baseCollider = new BoxCollider();
        baseCollider = ob.AddComponent<BoxCollider>();
        baseCollider.size = BaseHandColliderSize;

        Vector3 pos2 = baseCollider.center;
        pos2 = TranslateHandBoundingBox(pos2);

        baseCollider.center = pos2;
        return baseCollider;
    }

    /// <summary>
    /// Helper method for creating a collider.
    /// </summary>
    /// <param name="pos">The position.</param>
    /// <returns>The new position</returns>
    public static Vector3 TranslateHandBoundingBox(Vector3 pos)
    {
        pos.x -= .05f;
        pos.y -= .02f;
        return pos;
    }
}
