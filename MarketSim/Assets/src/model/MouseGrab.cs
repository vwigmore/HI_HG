namespace Assets.src.model
{
    using UnityEngine;

    /// <summary>
    /// Implements specific grab functions for the Mouse.
    /// </summary>
    /// <seealso cref="Assets.src.model.Grab" />
    public class MouseGrab : Grab
    {
        #region Fields

        /// <summary>
        /// The throw force
        /// </summary>
        private readonly float throwForce = 500;

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
        public override void UpdateGrabbedObject(Vector3 grabPoint)
        {
            if (IsGrabbing())
                prevPos = GrabbedObject.transform.position;

            base.UpdateGrabbedObject(grabPoint);
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