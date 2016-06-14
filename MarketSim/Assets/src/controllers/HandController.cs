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

    private VibrateHand vhand;

    /// <summary>
    /// The collision contacts
    /// </summary>
    private ArrayList collisionContacts;

    /// <summary>
    /// The bend values
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
        collisionContacts = new ArrayList();
        bends = new bool[5];
        BendFingersIsTrue();
    }

    /// <summary>
    /// Tells if the fingers are bend.
    /// </summary>
    private void BendFingersIsTrue()
    {
        for (int i = 0; i < bends.Length; i++)
            bends[i] = true;
    }

    /// <summary>
    ///  Update is called once per frame.
    /// </summary>
    private void Update()
    {
        UpdateHandPosition();
        hand.UpdateHand(bends);
        hand.UpdateGestures();
        vhand.UpdateVibration();
        vhand.UpdateTimer();

        ProcessContacts();

        Vector3 contactPoint = CalculateContactPoint();
        hand.GetManusGrab().UpdateGrabbedObject(contactPoint, hand.GetRootTransform());
        hand.GetManusGrab().basket.UpdateList();
    }

    /// <summary>
    /// Updates the position of the hand.
    /// </summary>
    private void UpdateHandPosition()
    {

        if (!Manager.MKBOnly)
        {
            hand.UpdatePosition();
        }
    }

    /// <summary>
    /// Calculates the contact point.
    /// </summary>
    /// <returns></returns>
    private Vector3 CalculateContactPoint()
    {
        if (collisionContacts.Count == 0)
            return ((Collider)hand.GetColliders()[0]).transform.position;

        float x = 0;
        float y = 0;
        float z = 0;

        TransformXYZPosition(x, y, z);

        return new Vector3(x, y, z) / collisionContacts.Count;
    }

    /// <summary>
    /// Transforms the x y and z positions.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="z">The z.</param>
    private void TransformXYZPosition(float x, float y, float z)
    {
        for (int i = 0; i < collisionContacts.Count; i++)
        {
            ContactPoint c = (ContactPoint)collisionContacts[i];
            x += c.thisCollider.transform.position.x;
            y += c.thisCollider.transform.position.y;
            z += c.thisCollider.transform.position.z;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    private void ProcessContacts()
    {
        bool thumbTouch = false;
        int othersTouch = 0;

        ContactsCountSize(thumbTouch, othersTouch);
       
    }

    /// <summary>
    /// Contactses the size of the count.
    /// </summary>
    /// <param name="thumbTouch">if set to <c>true</c> [thumb touch].</param>
    /// <param name="othersTouch">The others touch.</param>
    public void ContactsCountSize(bool thumbTouch, int othersTouch)
    {
        if (collisionContacts.Count >= 1)
        {
            BendFingersIsTrue();

            ContactPoint c = (ContactPoint)collisionContacts[0];

            IndexUpdate(c, thumbTouch, othersTouch);

            GetManusHighlightedObject(thumbTouch, othersTouch);
        }

        else if (collisionContacts.Count == 0)
        {
            hand.GetManusGrab().ClearHighlights();
        }
    }

    /// <summary>
    /// Gets the manus highlighted object.
    /// </summary>
    /// <param name="thumbTouch">if set to <c>true</c> [thumb touch].</param>
    /// <param name="othersTouch">The others touch.</param>
    private void GetManusHighlightedObject(bool thumbTouch, int othersTouch)
    {
        if (thumbTouch && (othersTouch >= 1))
        {
            hand.GetManusGrab().GrabHighlightedObject();
        }
        else
        {
            hand.GetManusGrab().DropObject();
        }
    }

    /// <summary>
    /// Indexes the update.
    /// </summary>
    /// <param name="c">The c.</param>
    /// <param name="thumbTouch">if set to <c>true</c> [thumb touch].</param>
    /// <param name="othersTouch">The others touch.</param>
    private void IndexUpdate(ContactPoint c, bool thumbTouch, int othersTouch)
    {
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
    /// Adds the contact if greater than one.
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
    private void AddContact(Collision col, Collider thisCollider )
    {
        for (int j = 0; j < hand.GetColliders().Count; j++)
        {
            if (hand.GetColliders()[j].Equals(thisCollider))
            {
                ContainmentCheck(col);
            }

        }
    }

    /// <summary>
    /// Containments the check.
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
    /// Removes the contact.
    /// </summary>
    /// <param name="c">The c.</param>
    private void RemoveContact(ContactPoint c)
    {
        for (int j = 0; j < hand.GetColliders().Count; j++)
        {
            if (hand.GetColliders()[j].Equals(c.thisCollider))
            {
                collisionContacts.Remove(c);
            }

        }

    }

    /// <summary>
    /// Removes all contact.
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