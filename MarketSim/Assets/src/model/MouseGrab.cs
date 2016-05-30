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
        /// The moving force of the object
        /// </summary>
        private float force = 50000;

        /// <summary>
        /// The initial position of the object
        /// </summary>
        private Vector3 Initialpos;
        private Vector3 speed = new Vector3 (0,20,25);
        private float dirSpeed = 15f;
        private Boolean moved = false;
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
          //  pos = GrabbedObject.transform.position;

            if (GrabbedObject == null)
                return;

            if (InProximity(basket.holder) && !GrabbedObject.tag.Equals("basket") && !basket.items.Contains(GrabbedObject))
            {
                Vector3 newpos = basket.holder.transform.position;
                newpos.y = newpos.y + 0.4f;
                moved = true;

                if (basket.items.Count < basket.rows * basket.cols)
                    basket.items.Add(GrabbedObject);     
            }
           
            else if(IsGrabbing())
            {
               
                Vector3 targetPos = Input.mousePosition;
                Vector3 direction = targetPos - GrabbedObject.transform.position;
                direction.Normalize();
                GrabbedObject.GetComponent<Rigidbody>().AddForce(direction*1000f,ForceMode.Force);
                moved = false;
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