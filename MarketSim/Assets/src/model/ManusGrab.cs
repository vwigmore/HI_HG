namespace Assets.src.model
{
    using ManusMachina;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Implements specific grab functions for the Manus.
    /// </summary>
    /// <seealso cref="Assets.src.model.Grab" />
    public class ManusGrab : Grab
    {
        #region Fields

        /// <summary>
        /// The throw force
        /// </summary>
        private readonly float throwForce = 500;

        /// <summary>
        /// The last position
        /// </summary>
        private Vector3 lastPos;

        private IHand hand;

        /// <summary>
        /// The last rotation
        /// </summary>
        private Quaternion lastRotation;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ManusGrab"/> class.
        /// </summary>
        /// <param name="grabber">The grabber.</param>
        /// <param name="highlightColor">Color of the highlight.</param>
        public ManusGrab(GameObject grabber, Color highlightColor, IHand hand)
            : base(grabber, highlightColor)
        {
            this.Grabber = grabber;
            this.highlightColor = highlightColor;
            this.lastRotation = Quaternion.identity;
            this.hand = hand;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Updates the grabbed object.
        /// </summary>
        /// <param name="trans">The trans.</param>
        public void UpdateGrabbedObject(Vector3 grabPos, Transform grabberTransform)
        {
            if (IsGrabbing())
            {
                SetPrevPosition(GrabbedObject.transform.position);

                //Vector3 newpos = grabber.transform.position;
                if (GrabbedObject.tag.Equals("basket"))
                {
                    float y = this.GrabbedObject.GetComponent<BoxCollider>().bounds.size.y;
                    UpdateGrabbedObjectsPosition(grabPos);

                    UpdateGrabbedObjectsRotation(grabberTransform);
                }

                //newpos.z += offset;

                UpdateGrabbedObjectsPosition(grabPos);

                UpdateGrabbedObjectsRotation(grabberTransform);
            }
        }

        /// <summary>
        /// Updates the grabbed objects position.
        /// </summary>
        /// <param name="newpos">The new position.</param>
        /// <param name="trans">The transform.</param>
        public void UpdateGrabbedObjectsPosition(Vector3 newpos)
        {
            GrabbedObject.transform.position = newpos;
            GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            GrabbedObject.GetComponent<Collider>().enabled = false;
        }

        /// <summary>
        /// Updates the grabbed objects rotation.
        /// </summary>
        /// <param name="trans">The trans.</param>
        public void UpdateGrabbedObjectsRotation(Transform trans)
        {
            if (GrabbedObject.tag.Equals("basket"))
                return;

            //Quaternion current = trans.rotation;
            //Quaternion offset = Quaternion.Inverse(GetPrevGrabberRot()) * current;
            //Quaternion newrot = offset * GetPrevRotation();
            //GrabbedObject.transform.rotation = Quaternion.Inverse(newrot);
            GrabbedObject.transform.rotation = trans.rotation;
            if (this.hand is RightHand)
                GrabbedObject.transform.Rotate(Vector3.up, 180);
        }

        #endregion Methods
    }
}