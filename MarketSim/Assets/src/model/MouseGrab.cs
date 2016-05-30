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
        private  float force = 3000000;
        private Vector3 pos;
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
        /// Drop currently grabbed object.
        /// </summary>
        public override void DropObject()
        {
            pos = GrabbedObject.transform.position;

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
                float length = (pos.y - GrabbedObject.transform.position.y);
                GrabbedObject.GetComponent<Rigidbody>().AddForce(( GrabbedObject.transform.position - pos) * force*length* Time.smoothDeltaTime);
                GrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
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