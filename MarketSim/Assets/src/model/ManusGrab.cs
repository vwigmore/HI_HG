namespace Assets.src.model
{
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
        public ManusGrab(GameObject grabber, Color highlightColor)
            : base(grabber, highlightColor)
        {
            this.Grabber = grabber;
            this.highlightColor = highlightColor;
            this.lastRotation = Quaternion.identity;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Updates the grabbed object.
        /// </summary>
        /// <param name="trans">The trans.</param>
        public void UpdateGrabbedObject(float offset, Transform trans)
        {
            if (IsGrabbing())
            {
                SetPrevPosition(GrabbedObject.transform.position);
                Vector3 newpos = grabber.transform.position;
                if (GrabbedObject.tag.Equals("basket"))
                {
                    float y = this.GrabbedObject.GetComponent<BoxCollider>().bounds.size.y;
                    UpdateGrabbedObjectsPosition(newpos);
                    UpdateGrabbedObjectsRotation(trans);
                }
                newpos.z += offset;

                UpdateGrabbedObjectsPosition(newpos);
                UpdateGrabbedObjectsRotation(trans);
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
        }

        /// <summary>
        /// Updates the grabbed objects rotation.
        /// </summary>
        /// <param name="trans">The trans.</param>
        public void UpdateGrabbedObjectsRotation(Transform trans)
        {
            Quaternion current = trans.rotation;
            Quaternion offset = Quaternion.Inverse(GetPrevGrabberRot()) * current;
            Quaternion newrot = offset * GetPrevRotation();
            GrabbedObject.transform.rotation = Quaternion.Inverse(newrot);
        }

        #endregion Methods
    }
}