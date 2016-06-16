using Assets.src.model;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor
{
    /// <summary>
    /// Test for the Grab
    /// </summary>
    [TestFixture]
    public abstract class GrabTest
    {
        #region Fields

        /// <summary>
        /// The grab object
        /// </summary>
        private Grab grab;

        /// <summary>
        /// The item to pick up
        /// </summary>
        private GameObject pickUpItem;

        #endregion Fields

        #region Methods

        /// <summary>
        /// verifies that the grab function passes.
        /// </summary>
        /// <returns></returns>
        public abstract Grab PassGrab();

        /// <summary>
        /// setup for all instances.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.grab = PassGrab();

            this.pickUpItem = new GameObject("pickUpItem");
            this.pickUpItem.GetComponent<Transform>().position = new Vector3(0.0f, 0.0f, 0.0f);
            this.pickUpItem.gameObject.tag = "pickup";

            var mat = new Material(Shader.Find("Diffuse"));
            mat.color = Color.green;
            this.pickUpItem.AddComponent<MeshRenderer>();
            this.pickUpItem.GetComponent<MeshRenderer>().material = mat;
        }

        /// <summary>
        /// Tears down instance.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.grab = null;
            this.pickUpItem = null;
        }

        /// <summary>
        /// Tests the constructor that it is not null.
        /// </summary>
        [Test]
        public void ConstructorNotNullTest()
        {
            Assert.IsNotNull(this.grab);
        }

        /// <summary>
        /// Tests if the highlighted object is picked up.
        /// </summary>
        [Test]
        public void HighlightSelectedObjectTest()
        {
            Assert.IsNull(this.grab.highlighted);
            this.grab.HighlightSelectedObject(this.pickUpItem);
            Assert.IsNotNull(this.grab.highlighted);
            Assert.AreEqual(this.pickUpItem, this.grab.highlighted);
        }

        #endregion Methods
    }
}