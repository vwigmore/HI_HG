namespace Assets.src.model
{
    using UnityEngine;

    /// <summary>
    /// Controls the grab function.
    /// </summary>
    internal abstract class Grab
    {
        #region Fields

        /// <summary>
        /// An object in proximity has to be within this distance.
        /// </summary>
        protected float proxDist = 4.0f;

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
        /// Object currently selected.
        /// </summary>
        protected GameObject highlighted;

        /// <summary>
        /// Color used to highlight selected objects.
        /// </summary>
        protected Color highlightColor;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Grab"/> class.
        /// </summary>
        /// <param name="grabber">The grabber.</param>
        /// <param name="highlightColor">Color of the highlight.</param>
        public Grab(GameObject grabber, Color highlightColor)
        {
            this.Grabber = grabber;
            this.player = GameObject.FindGameObjectsWithTag("Player")[0];
            this.prevHighlightedColor = Color.clear;
            this.prevHighlighted = null;
            this.highlighted = null;
            this.highlightColor = highlightColor;
            this.GrabbedObject = null;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the grabber.
        /// </summary>
        /// <value>
        /// The grabber.
        /// </value>
        protected GameObject Grabber { get; set; }

        /// <summary>
        /// Gets or sets the grabbed object.
        /// </summary>
        /// <value>
        /// The grabbed object.
        /// </value>
        protected GameObject GrabbedObject { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Grab selected object.
        /// </summary>
        public void GrabHighlightedObject()
        {
            if (this.highlighted != null)
            {
                this.GrabbedObject = this.highlighted;
            }
        }

        /// <summary>
        /// Drop currently grabbed object.
        /// </summary>
        public abstract void DropObject();

        /// <summary>
        /// Updates the grabbed object.
        /// </summary>
        public void UpdateGrabbedObject()
        {
            if (this.IsGrabbing())
            {
                Vector3 newpos = this.Grabber.transform.position + (this.Grabber.transform.forward * .2f);
                this.GrabbedObject.transform.position = newpos;
                this.GrabbedObject.GetComponent<Collider>().enabled = false;
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
            this.ClearHighlights();
            if (obj.tag.Equals("pickup") && this.InProximity(obj))
            {
                this.prevHighlighted = obj;
                this.prevHighlightedColor = obj.GetComponent<Renderer>().material.color;
                this.highlighted = obj;
                obj.GetComponent<Renderer>().material.color = this.highlightColor;
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

        #endregion Methods
    }
}