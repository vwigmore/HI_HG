using Assets.src.model;
using ManusMachina;
using System.Collections;
using UnityEngine;

public class HandController : MonoBehaviour
{
	#region Fields

	public Transform RootTransform;

	/// <summary>
	/// The glove_hand
	/// </summary>
	public GLOVE_HAND glove_hand;

	/// <summary>
	/// The hand
	/// </summary>
	private IHand hand;

	private ArrayList collisionContacts;

	private bool[] bends;

	#endregion Fields

	#region Methods

	/// <summary>
	/// Use this for initialization
	/// </summary>
	private void Start()
	{
		hand = HandFactory.createHand(glove_hand, RootTransform);
		collisionContacts = new ArrayList();
		bends = new bool[5];
		for (int i = 0; i < bends.Length; i++)
			bends[i] = true;
	}

	/// <summary>
	///  Update is called once per frame.
	/// </summary>
	private void Update()
	{
		hand.Update (bends);
		ProcessContacts();
		hand.GetManusGrab().UpdateGrabbedObject(CalculateContactPoint(), hand.GetRootTransform());
		hand.GetManusGrab().basket.UpdateList();
	}

	private Vector3 CalculateContactPoint()
	{
		if (collisionContacts.Count == 0)
			return ((Collider)hand.GetColliders()[0]).transform.position;
		float x = 0;
		float y = 0;
		float z = 0;

		for (int i = 0; i < collisionContacts.Count; i++)
		{
			ContactPoint c = (ContactPoint)collisionContacts[i];
			x += c.thisCollider.transform.position.x;
			y += c.thisCollider.transform.position.y;
			z += c.thisCollider.transform.position.z;
		}

		return new Vector3(x, y, z) / collisionContacts.Count;
	}

	private void ProcessContacts()
	{
		bool thumbTouch = false;
		int othersTouch = 0;

		if (collisionContacts.Count >= 1)
		{
			for (int i = 0; i < bends.Length; i++)
				bends[i] = true;

			ContactPoint c = (ContactPoint)collisionContacts[0];
			for (int i = 0; i < collisionContacts.Count; i++)
			{
				c = (ContactPoint)collisionContacts[i];

				int index = hand.GetColliders().IndexOf(c.thisCollider);

				if (index < bends.Length && hand.GetManusGrab().IsGrabbing())
					bends[index] = false;

				if (hand.GetColliders().IndexOf(c.thisCollider) == 0)
					thumbTouch = true;
				else
					othersTouch++;
			}

			if (thumbTouch && (othersTouch >= 1))
			{
				hand.GetManusGrab().GrabHighlightedObject();
			}
			else
			{
				hand.GetManusGrab().DropObject();
			}
		}
		else if (collisionContacts.Count == 0)
		{
			hand.GetManusGrab().ClearHighlights();
		}
	}

	private void OnCollisionEnter(Collision col)
	{
		if (col.contacts.Length >= 1)
		{
			Collider thisCollider = col.contacts[0].thisCollider;
			for (int j = 0; j < hand.GetColliders().Count; j++)
			{
				if (hand.GetColliders()[j].Equals(thisCollider))
				{
					if (!collisionContacts.Contains(col.contacts[0]))
						collisionContacts.Add(col.contacts[0]);
				}
			}
		}

		hand.Touch(col.gameObject);
		hand.GetManusGrab().HighlightSelectedObject(col.gameObject);
	}

	private void OnCollisionStay(Collision col)
	{
		hand.GetManusGrab().HighlightSelectedObject(col.gameObject);
	}

	private void OnCollisionExit(Collision col)
	{
		for (int i = 0; i < collisionContacts.Count; i++)
		{
			ContactPoint c = (ContactPoint)collisionContacts[i];
			for (int j = 0; j < hand.GetColliders().Count; j++)
			{
				if (hand.GetColliders()[j].Equals(c.thisCollider))
				{
					collisionContacts.Remove(c);
				}
			}
		}

		hand.GetManusGrab().ClearHighlights();
	}

	///// <summary>
	///// Called when [trigger enter].
	///// </summary>
	///// <param name="collision">The collision.</param>
	//private void OnTriggerEnter(Collider collision)
	//{
	//    GameObject collisionObj = collision.gameObject;
	//    hand.GetManusGrab().HighlightSelectedObject(collisionObj);
	//    hand.Touch(collisionObj);
	//}

	///// <summary>
	///// Called when [trigger exit].
	///// </summary>
	///// <param name="collision">The collision.</param>
	//private void OnTriggerExit(Collider collision)
	//{
	//    hand.GetManusGrab().ClearHighlights();
	//}

	#endregion Methods
}