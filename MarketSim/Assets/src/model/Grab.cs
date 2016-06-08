namespace Assets.src.model
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Controls the grab function
    /// </summary>
    public abstract class Grab
    {
        #region Fields

        /// <summary>
        /// The basket
        /// </summary>
        public ItemHolder basket;

        /// <summary>
        /// The throw force
        /// </summary>
        protected readonly float throwForce = 1500;

        /// <summary>
        /// An object in proximity has to be within this distance.
        /// </summary>
        protected float proxDist = 2.0f;

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
        protected Vector3 prevPos;

        /// <summary>
        /// The previous rotation.
        /// </summary>
        protected Vector3 prevRot;

        /// <summary>
        /// Object currently selected.
        /// </summary>
        public GameObject highlighted { get; protected set; }

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
        /// Gets or sets the grabbed object.
        /// </summary>
        /// <value>
        /// The grabbed object.
        /// </value>
        public GameObject GrabbedObject { get; set; }

        /// <summary>
        /// The GameObject that the grabbed item will follow.
        /// </summary>
        protected GameObject grabber { set; get; }

        /// <summary>
        /// Gets or sets the grabber.
        /// </summary>
        /// <value>
        /// The grabber.
        /// </value>
        protected GameObject Grabber { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Grab selected object.
        /// </summary>
        public void GrabHighlightedObject()
        {
            if (this.highlighted != null)
            {
                Debug.Log("grabbing: "+ highlighted);
                this.GrabbedObject = this.highlighted;
            }
        }

        /// <summary>
        /// Drop currently grabbed object.
        /// </summary>
        public virtual void DropObject()
        {
            if (GrabbedObject == null)
                return;

            if (InProximity(basket.holder) && !GrabbedObject.tag.Equals("basket") && !basket.items.Contains(GrabbedObject))
            {
                Vector3 newpos = basket.holder.transform.position ;
                newpos.y -= newpos.y;

                if (basket.items.Count < basket.rows * basket.cols)
                    basket.items.Add(GrabbedObject);
            }
            else
            {
                Vector3 targetPos = GrabbedObject.transform.position;
                Vector3 direction = targetPos - GetPrevPosition();
                GrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                GrabbedObject.GetComponent<Rigidbody>().AddForce(direction * throwForce, ForceMode.Force);
                GrabbedObject = null;
                highlighted = null;
            }
        }

        /// <summary>
        /// Updates the grabbed object.
        /// </summary>
        public virtual void UpdateGrabbedObject()
        {
            if (IsGrabbing())
            {
                SetPrevPosition(GrabbedObject.transform.position);
                Vector3 newpos = grabber.transform.position + grabber.transform.forward;
                //Quaternion rotation = GrabbedObject.transform.rotation;

                if (GrabbedObject.tag.Equals("basket"))
                {
                    float y = this.GrabbedObject.GetComponent<BoxCollider>().bounds.size.y;
                    newpos.y -= y;
                    GrabbedObject.transform.position = newpos;
                    GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                    Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(),
                    GrabbedObject.GetComponent<Collider>());
                    SetPrevPosition(grabber.transform.position);
                }

                GrabbedObject.transform.position = newpos;
               // GrabbedObject.transform.rotation = grabber.transform.rotation;
                GrabbedObject.transform.rotation = Quaternion.AngleAxis(90, Vector3.left);
                GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(),
                GrabbedObject.GetComponent<Collider>());
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
        /// Highlight selected object
        /// </summary>
        /// <param name="obj">Object to highlight</param>
        public void HighlightSelectedObject(GameObject obj)
        {
            ClearHighlights();
            if ((obj.tag.Equals("pickup") || obj.tag.Equals("basket")) && InProximity(obj))
            {
                this.prevHighlighted = obj;
                this.prevHighlightedColor = obj.GetComponent<Renderer>().sharedMaterial.color;
                this.highlighted = obj;
                if (Manager.HighlightOn)
                {
                    obj.GetComponent<Renderer>().material.color = this.highlightColor;
                }
            }
            else
            {
                this.highlighted = null;
            }
        }

        /// <summary>
        /// Clear previously highlighted objects
        /// </summary>
        public void ClearHighlights()
        {
            if (this.prevHighlighted != null)
            {
                this.prevHighlighted.GetComponent<Renderer>().material.color = this.prevHighlightedColor;
            }
        }

        /// <summary>
        /// Returns whether an object is in range of the grabber.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if object is in range, else false.</returns>
        public bool InProximity(GameObject obj)
        {
            return Vector3.Distance(this.Grabber.transform.position, obj.transform.position) <= this.proxDist;
        }

        /// <summary>
        /// Returns whether an object is in range of the grabber.
        /// </summary>
        /// <param name="pos">The position to check.</param>
        /// <returns>True if object is in range, else false.</returns>
        public bool InProximity(Vector3 pos)
        {
            return Vector3.Distance(this.Grabber.transform.position, pos) <= this.proxDist;
        }

        /// <summary>
        /// Sets the previous position.
        /// </summary>
        /// <param name="pos">The position.</param>
        public void SetPrevPosition(Vector3 pos)
        {
            this.prevPos = pos;
        }

        /// <summary>
        /// Gets the previous position.
        /// </summary>
        /// <returns>The previous position</returns>
        public Vector3 GetPrevPosition()
        {
            return this.prevPos;
        }

        /// <summary>
        /// Sets the previous rotation.
        /// </summary>
        /// <param name="rot">The rotation.</param>
        public void SetPrevRotation(Vector3 rot)
        {
            this.prevRot = rot;
        }

        /// <summary>
        /// Gets the previous rotation.
        /// </summary>
        /// <returns>The previous rotation</returns>
        public Vector3 GetPrevRotation()
        {
            return this.prevRot;
        }

        #endregion Methods
    }
}