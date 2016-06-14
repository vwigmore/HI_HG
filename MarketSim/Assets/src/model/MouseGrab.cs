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
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Updates the grabbed object.
        /// Also stores previous position of object.
        /// </summary>
        public void UpdateGrabbedObject(Vector3 grabPoint)
        {
            if (IsGrabbing())
            {
                base.UpdateGrabbedObject();

                this.UpdateGrabbedBasket(grabPoint);

                this.GrabbedObject.transform.position = grabPoint;
                this.GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                Physics.IgnoreCollision(
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(),
                    this.GrabbedObject.GetComponent<Collider>());
            }
        }

        #endregion Methods
    }
}