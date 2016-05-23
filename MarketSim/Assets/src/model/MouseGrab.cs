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
    internal class MouseGrab : Grab
    {
        #region Fields

        /// <summary>
        /// The ray cast hit
        /// </summary>
        private RaycastHit hit;

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
            if (InProximity(basket.holder) && !GrabbedObject.tag.Equals("basket") && !basket.items.Contains(GrabbedObject))
            {
                Vector3 newpos = basket.holder.transform.position;
                newpos.y = newpos.y + 0.4f;

                if (basket.items.Count < basket.rows * basket.cols)
                    basket.items.Add(GrabbedObject);
            }
            else if (InProximity(cart.holder) && !GrabbedObject.tag.Equals("cart") && !cart.items.Contains(GrabbedObject))
            {
                Vector3 newpos = cart.holder.transform.position;
                newpos.y = newpos.y + 0.4f;

                if (cart.items.Count < cart.rows * cart.cols)
                    cart.items.Add(GrabbedObject);
            }
            else
            {
                GrabbedObject.GetComponent<Collider>().enabled = true;
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