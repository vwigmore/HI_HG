using Assets.src.model;
using ManusMachina;
using System.Collections;
using UnityEngine;

public class HandController : MonoBehaviour
{
    #region Fields

    public Transform RootTransform;

    /// <summary>
    /// The glove_hand.
    /// </summary>
    public GLOVE_HAND glove_hand;

    /// <summary>
    /// The hand.
    /// </summary>
    private IHand hand;

    /// <summary>
    /// The collision contacts.
    /// </summary>
    private ArrayList collisionContacts;

    /// <summary>
    /// The bend values.
    /// </summary>
    private bool[] bends;

    #endregion Fields

    #region Methods

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    private void Start()
    {
        hand = HandFactory.createHand(glove_hand, RootTransform);
        Debug.Log("roottrans: " + RootTransform.ToString());
        collisionContacts = new ArrayList();
        bends = new bool[5];
        ResetBends();
    }

    /// <summary>
    /// Tells that the fingers are bend.
    /// </summary>
    private void ResetBends()
    {
        for (int i = 0; i < bends.Length; i++)
            bends[i] = true;
    }

    /// <summary>
    ///  Update is called once per frame.
    /// </summary>
    private void Update()
    {
        hand.Update(bends);
        ProcessContacts();
        hand.GetManusGrab().UpdateGrabbedObject(CalculateContactPoint(), hand.GetRootTransform());
        hand.GetManusGrab().basket.UpdateList();
    }

    /// <summary>
    /// Calculates the contact point.
    /// </summary>
    /// <returns>The new point</returns>
    private Vector3 CalculateContactPoint()
    {
        if (collisionContacts.Count == 0)
            return ((Collider)hand.GetColliders()[0]).transform.position;
        return CalculateAverageContactPoint();
    }

    /// <summary>
    /// Transforms the x y and z positions.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="z">The z.</param>
    private Vector3 CalculateAverageContactPoint()
    {
        float x = 0f, y = 0f, z = 0f;
        for (int i = 0; i < collisionContacts.Count; i++)
        {
            ContactPoint c = (ContactPoint)collisionContacts[i];
            x += c.thisCollider.transform.position.x;
            y += c.thisCollider.transform.position.y;
            z += c.thisCollider.transform.position.z;
        }
        return new Vector3(x, y, z) / collisionContacts.Count;
    }

    /// <summary>
    /// Processes the contacts.
    /// </summary>
    private void ProcessContacts()
    {
        if (collisionContacts.Count >= 1)
        {
            bool thumbTouch = false;
            int othersTouch = 0;
            ContactDetected(thumbTouch, othersTouch);
        }
        else
        {
            hand.GetManusGrab().ClearHighlights();
        }
    }

    /// <summary>
    /// On contact detected, check which fingers are touching the object.
    /// </summary>
    /// <returns></returns>
    private void ContactDetected(bool thumbTouch, int othersTouch)
    {
        ResetBends();

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

        HandleTouch(thumbTouch, othersTouch);
    }

    /// <summary>
    /// Checks if the thumb touches other fingers.
    /// </summary>
    /// <param name="thumbTouch">if set to <c>true</c> [thumb touch].</param>
    /// <param name="othersTouch">The others touch.</param>
    private void HandleTouch(bool thumbTouch, int othersTouch)
    {
        if (thumbTouch && (othersTouch >= 1))
            hand.GetManusGrab().GrabHighlightedObject();
        else
            hand.GetManusGrab().DropObject();
    }

    /// <summary>
    /// Called when [collision enter].
    /// </summary>
    /// <param name="col">The col.</param>
    private void OnCollisionEnter(Collision col)
    {
        AddContactIfGreaterThanOne(col);

        hand.Touch(col.gameObject);
        hand.GetManusGrab().HighlightSelectedObject(col.gameObject);
    }

    /// <summary>
    /// Adds the contact if the length is greater than one.
    /// </summary>
    /// <param name="col">The col.</param>
    private void AddContactIfGreaterThanOne(Collision col)
    {
        if (col.contacts.Length >= 1)
        {
            Collider thisCollider = col.contacts[0].thisCollider;
            AddContact(col, thisCollider);
        }
    }

    /// <summary>
    /// Adds the contact.
    /// </summary>
    /// <param name="col">The col.</param>
    /// <param name="thisCollider">The this collider.</param>
    private void AddContact(Collision col, Collider thisCollider)
    {
        for (int j = 0; j < hand.GetColliders().Count; j++)
        {
            if (hand.GetColliders()[j].Equals(thisCollider))
                ContainmentCheck(col);
        }
    }

    /// <summary>
    /// Cecks if the contact list contains a certain contact, if it does not than the contact is added to the list.
    /// </summary>
    /// <param name="col">The col.</param>
    private void ContainmentCheck(Collision col)
    {
        if (!collisionContacts.Contains(col.contacts[0]))
            collisionContacts.Add(col.contacts[0]);
    }

    /// <summary>
    /// Called when [collision stay].
    /// </summary>
    /// <param name="col">The col.</param>
    private void OnCollisionStay(Collision col)
    {
        hand.GetManusGrab().HighlightSelectedObject(col.gameObject);
    }

    /// <summary>
    /// Called when [collision exit].
    /// </summary>
    /// <param name="col">The col.</param>
    private void OnCollisionExit(Collision col)
    {
        RemoveAllContact();

        hand.GetManusGrab().ClearHighlights();
    }

    /// <summary>
    /// Removes the contact from the list.
    /// </summary>
    /// <param name="c">The c.</param>
    private void RemoveContact(ContactPoint c)
    {
        for (int j = 0; j < hand.GetColliders().Count; j++)
        {
            if (hand.GetColliders()[j].Equals(c.thisCollider))
                collisionContacts.Remove(c);
        }
    }

    /// <summary>
    /// Removes all contacts from the list.
    /// </summary>
    private void RemoveAllContact()
    {
        for (int i = 0; i < collisionContacts.Count; i++)
        {
            ContactPoint c = (ContactPoint)collisionContacts[i];
            RemoveContact(c);
        }
    }

    #endregion Methods
}