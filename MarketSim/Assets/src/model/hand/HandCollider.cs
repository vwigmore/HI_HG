using Assets.src.model;
using System;
using System.Collections;
using UnityEngine;

public static class HandCollider
{
	private static Vector3 baseColliderSize = new Vector3(0.1f, 0.02f, 0.1f);
	public static Collider CreateHandBaseCollider(GameObject root)
	{
		BoxCollider baseCollider = new BoxCollider();
		baseCollider = root.AddComponent<BoxCollider>();
		baseCollider.size = baseColliderSize;
		baseCollider.center = TranslateBaseColliderPos (baseCollider.center);
		return baseCollider;
	}

	public static Vector3 TranslateBaseColliderPos(Vector3 pos)
	{
		pos.x -= 0.05f;
		pos.y -= 0.2f;
		return pos;
	}

	public static ArrayList InitializeFingerColliders(Transform[][] gameTransforms)
	{
		ArrayList colliders = new ArrayList();
		for (int i = 0; i < 5; i++)
			for (int j = 0; j < 4; j++)
			{
				Collider collider;
				if (j == 3)
					collider = CreateFingerTipCollider (gameTransforms [i] [j].gameObject);
				else 
					collider = CreateFingerPartCollider (gameTransforms [i] [j].gameObject);
				colliders.Add (collider);
			}		
		return colliders;
	}

	public static Collider CreateFingerTipCollider(GameObject root)
	{
		SphereCollider s = new SphereCollider();
		s = root.AddComponent<SphereCollider>();
		s.radius = .02f;
		return s;
	}

	public static Collider CreateFingerPartCollider(GameObject root)
	{
		BoxCollider b = new BoxCollider();
		b = root.AddComponent<BoxCollider>();
		b.size = new Vector3(.02f, .02f, .02f);
		return b;
	}
}


