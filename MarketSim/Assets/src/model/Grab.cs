namespace Assets.src.model
{
    using UnityEngine;

    /// <summary>
    /// Controls the grab function
    /// </summary>
    public abstract class Grab
    {
        #region Fields

        /// <summary>
        /// The basket.
        /// </summary>
        public ItemHolder basket;

        /// <summary>

        /// GameObject player.
        /// </summary>
        protected GameObject player;

        /// <summary>
        /// Previously selected GameObject.
        /// Used to restore their colors when they're not selected.
        /// </summary>
        protected GameObject prevHighlighted;

        /// <summary>
        /// Previously highlighted GameObject's color.
        /// </summary>
        protected Color prevHighlightedColor;

        /// <summary>
        /// Color used to highlight selected objects.
        /// </summary>
        protected Color highlightColor;

        /// <summary>
        /// Last position of the grabbed object.
        /// </summary>
        private Vector3 prevPos;

        /// <summary>
        /// The previous grabber rot.
        /// </summary>
        public Quaternion prevGrabberRot { get; set; }

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Grab"/> class.
        /// </summary>
        /// <param name="grabber">The grabber.</param>
        /// <param name="highlightColor">Color of the highlight.</param>
        public Grab(GameObject grabber, Color highlightColor)
        {
            this.grabber = grabber;
            this.player = GameObject.FindGameObjectWithTag("Player");
            this.prevHighlightedColor = Color.clear;
            this.prevHighlighted = null;
            this.highlighted = null;
            this.highlightColor = highlightColor;
            this.GrabbedObject = null;

            this.basket = new ItemHolder(GameObject.FindGameObjectWithTag("basket"), 2, 3);
            basket.InitOffsets();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Object currently selected.
        /// </summary>
        public GameObject highlighted { get; protected set; }

        /// <summary>
        /// Gets or sets the grabbed object.
        /// </summary>
        /// <value>
        /// The grabbed object.
        /// </value>
        public GameObject GrabbedObject { get; set; }

        /// <summary>
        /// The GameObject that the grabbed item will follow.
        /// </summary>
        protected GameObject grabber { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Checks if there is an object highlighted,
        /// if there is, it is grabbed.
        /// </summary>
        public void GrabHighlightedObject()
        {
            if (this.highlighted != null)
            {
                GrabHighlightedNotNull();
            }

            ClearHighlights();
        }

        /// <summary>
        /// Checks if there is an object currently grabbed,
        /// if there is, drop it.
        /// </summary>
        public virtual void DropObject()
        {
            if (this.GrabbedObject != null)
            {
                DropObjectNotNull();
            }
        }

        /// <summary>
        /// Updates the position of the grabbed object.
        /// </summary>
        public virtual void UpdateGrabbedObject()
        {
            if (this.IsGrabbing())
                this.prevPos = this.GrabbedObject.transform.position;
        }

        /// <summary>
        /// Updates the position of the basket if it is grabbed.
        /// </summary>
        /// <param name="pos">The position.</param>
        public void UpdateGrabbedBasket(Vector3 pos)
        {
            if (this.GrabbedObject.tag.Equals("basket"))
            {
                float y = this.GrabbedObject.GetComponent<BoxCollider>().bounds.size.y;
                pos.y -= y;
                this.GrabbedObject.transform.position = pos;
                this.prevPos = this.grabber.transform.position;
                this.GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;

                Physics.IgnoreCollision(
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(),
                    this.GrabbedObject.GetComponent<Collider>());
            }
        }

        /// <summary>
        /// Tells whether an object is being grabbed.
        /// </summary>
        /// <returns>True if an object is currently being held, else false.</returns>
        public bool IsGrabbing()
        {
            return this.GrabbedObject != null;
        }

        /// <summary>
        /// If not currently grabbing an object, highlight
        /// the selected object.
        /// </summary>
        /// <param name="obj">Object to highlight</param>
        public void HighlightSelectedObject(GameObject obj)
        {
            if (IsGrabbing())
                return;

            this.ClearHighlights();
            if ((obj.tag.Equals("pickup") || obj.tag.Equals("basket")) && this.InProximity(obj))
            {
                this.Highlight(obj);
            }
            else
            {
                this.highlighted = null;
            }
        }

        /// <summary>
        /// Returns whether an object is in range of the grabber.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if object is in range, else false.</returns>
        public bool InProximity(GameObject obj)
        {
            return Vector3.Distance(this.grabber.transform.position, obj.transform.position) <= Manager.ProximityDist;
        }

        /// <summary>
        /// Returns whether an object is in range of the grabber.
        /// </summary>
        /// <param name="pos">The position to check.</param>
        /// <returns>True if object is in range, else false.</returns>
        public bool InProximity(Vector3 pos)
        {
            return Vector3.Distance(this.grabber.transform.position, pos) <= Manager.ProximityDist;
        }

        /// <summary>
        /// Set the previously highlighted object back to its original color.
        /// </summary>
        public void ClearHighlights()
        {
            if (this.prevHighlighted != null)
            {
                this.prevHighlighted.GetComponent<Renderer>().material.color = this.prevHighlightedColor;
            }
            highlighted = null;
        }

        /// <summary>
        /// Highlights the specified object.
        /// </summary>
        /// <param name="obj">The object to be highlighted.</param>
        private void Highlight(GameObject obj)
        {
            this.prevHighlighted = obj;
            this.prevHighlightedColor = obj.GetComponent<Renderer>().sharedMaterial.color;
            this.highlighted = obj;
            if (Manager.HighlightOn)
            {
                obj.GetComponent<Renderer>().material.color = this.highlightColor;
            }
        }

        /// <summary>
        /// Drops the grabbed object into the basket.
        /// </summary>
        private void DropInBasket()
        {
            Vector3 newpos = this.basket.holder.transform.position;
            newpos.y -= newpos.y;

            if (this.basket.items.Count < this.basket.rows * this.basket.cols)
            {
                this.basket.items.Add(this.GrabbedObject);
            }
        }

        /// <summary>
        /// Add force to object when throwing it away.
        /// </summary>
        private void ObjectForce()
        {
            Vector3 targetPos = this.GrabbedObject.transform.position;
            Vector3 direction = targetPos - this.prevPos;

            this.GrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            this.GrabbedObject.GetComponent<Rigidbody>().AddForce(direction * Manager.ThrowForce, ForceMode.Force);
            this.GrabbedObject = null;
            this.highlighted = null;
        }

        /// <summary>
        /// Grabs the highlighted object.
        /// </summary>
        private void GrabHighlightedNotNull()
        {
            this.GrabbedObject = this.highlighted;
            if (basket.items.Contains(highlighted))
            {
                basket.removeItem(highlighted);
            }
        }

        /// <summary>
        /// Drops the grabbed object into the basked if it is in range,
        /// else drop it with inertia applied.
        /// </summary>
        private void DropObjectNotNull()
        {
            GrabbedObject.GetComponent<Collider>().enabled = true;

            if (this.InProximity(this.basket.holder)
                && !this.GrabbedObject.tag.Equals("basket")
                && !this.basket.items.Contains(this.GrabbedObject))
            {
                this.DropInBasket();
            }
            else
            {
                this.ObjectForce();
            }
        }

        #endregion Methods
    }
}