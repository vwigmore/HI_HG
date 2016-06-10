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
            //GrabbedObject.transform.rotation = trans.rotation;

            //Vector3 a = GrabbedObject.transform.rotation.eulerAngles.normalized;
            //Vector3 b = grabber.transform.rotation.eulerAngles.normalized;
           // double angle = Math.Acos(Vector3.Dot(a, b));
            //Debug.Log(angle);

           // GrabbedObject.transform.rotation = Quaternion.AngleAxis(90, Vector3.left);
            GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        public void UpdateGrabbedObjectsRotation(Transform trans)
        {
            Quaternion current = trans.rotation;
            
            if (!lastRotation.Equals(current))
            {
                Quaternion relative = Quaternion.Inverse(lastRotation) * trans.rotation;
                Quaternion newRot = GrabbedObject.transform.rotation * relative;
                GrabbedObject.transform.rotation = newRot;
                lastRotation = current;
            }
        }

        #endregion Methods
    }
}