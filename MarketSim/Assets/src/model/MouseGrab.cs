namespace Assets.src.model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Implements specific grab functions for the Mouse.
    /// </summary>
    /// <seealso cref="Assets.src.model.Grab" />
    public class MouseGrab : Grab
    {
        #region Fields

        /// <summary>
        /// The ray cast hit
        /// </summary>
        private RaycastHit hit;

        /// <summary>
        /// The initial position of the object
        /// </summary>
        private Vector3 prevPos;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseGrab"/> class.
        /// </summary>
        /// <param name="grabber">The grabber.</param>
        /// <param name="highlightColor">Color of the highlight.</param>
        public MouseGrab(GameObject grabber, Color highlightColor)
            : base(grabber, highlightColor)
        {
            this.Grabber = grabber;
            this.highlightColor = highlightColor;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Updates the grabbed object.
        /// Also stores previous position of object.
        /// </summary>
        public override void UpdateGrabbedObject()
        {
            if (IsGrabbing())
                prevPos = GrabbedObject.transform.position;
            base.UpdateGrabbedObject();
            
        }

        /// <summary>
        /// Drop currently grabbed object.
        /// </summary>
        public override void DropObject()
        {
            if (GrabbedObject == null)
                return;

            if (InProximity(basket.holder) && !GrabbedObject.tag.Equals("basket") && !basket.items.Contains(GrabbedObject))
            {
                Vector3 newpos = basket.holder.transform.position;
                newpos.y = newpos.y + 0.4f;

                if (basket.items.Count < basket.rows * basket.cols)
                    basket.items.Add(GrabbedObject);
            }
            else
            {
                Vector3 targetPos = GrabbedObject.transform.position;
                Vector3 direction = targetPos - prevPos;
                GrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                GrabbedObject.GetComponent<Rigidbody>().AddForce(direction*10, ForceMode.Force);
                GrabbedObject = null;
                highlighted = null;
            }
        }

        /// <summary>
        /// Sets the Ray cast hit.
        /// </summary>
        /// <param name="hit">The Ray cast hit.</param>
        public void SetHit(RaycastHit hit)
        {
            this.hit = hit;
        }

        #endregion Methods
    }
}