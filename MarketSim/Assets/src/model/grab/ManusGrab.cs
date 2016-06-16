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
        /// The last position
        /// </summary>
        private Vector3 lastPos;

        private IHand hand;

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
            this.hand = hand;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Updates the grabbed object's position and rotation
        /// depending on whether it is a basket or not.
        /// </summary>
        /// <param name="grabPos">The collision point of with the grabbed object.</param>
        /// <param name="grabberTransform">The transform component of the grabber.</param>
        public void UpdateGrabbedObject(Vector3 grabPos, Transform grabberTransform)
        {
            if (IsGrabbing())
            {
                base.UpdateGrabbedObject();

                if (GrabbedObject.tag.Equals("basket"))
                {
                    UpdateGrabbedObjectsPosition(grabPos);
                    UpdateGrabbedObjectsRotation(grabberTransform);
                }

                UpdateGrabbedObjectsPosition(grabPos);
                UpdateGrabbedObjectsRotation(grabberTransform);
            }
        }

        /// <summary>
        /// Updates the grabbed objects position.
        /// </summary>
        /// <param name="newpos">The new position.</param>
        private void UpdateGrabbedObjectsPosition(Vector3 newpos)
        {
            GrabbedObject.transform.position = newpos;
            GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            GrabbedObject.GetComponent<Collider>().enabled = false;
        }

        /// <summary>
        /// Updates the grabbed objects rotation if it is not a basket.
        /// </summary>
        /// <param name="trans">The trans.</param>
        private void UpdateGrabbedObjectsRotation(Transform trans)
        {
            if (GrabbedObject.tag.Equals("basket"))
                return;

            GrabbedObject.transform.rotation = trans.rotation;
            if (this.hand is RightHand)
                GrabbedObject.transform.Rotate(Vector3.up, 180);
        }

        #endregion Methods
    }
}