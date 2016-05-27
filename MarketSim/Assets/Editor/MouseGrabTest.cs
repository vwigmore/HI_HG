using Assets.src.model;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor
{
    [TestFixture]
    public class MouseGrabTest : GrabTest
    {
        #region Fields

        /// <summary>
        /// The mouse grab
        /// </summary>
        private MouseGrab mouseGrab;

        /// <summary>
        /// The highlight color
        /// </summary>
        private Color highlightColor = Color.cyan;

        /// <summary>
        /// The highlighted
        /// </summary>
        private GameObject highlighted;

        /// <summary>
        /// the item holder
        /// </summary>
        private ItemHolder h;

        /// <summary>
        /// The basket 
        /// </summary>
        GameObject basket;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Setups of all instances
        /// </summary>
        [SetUp]
        public new void Setup()
        {
            basket = new GameObject("basket");
            basket.AddComponent<Rigidbody>();
            basket.AddComponent<BoxCollider>();
            h = new ItemHolder(basket, 2, 4);
            mouseGrab = (MouseGrab)PassGrab();
        }

        /// <summary>
        /// TearDown method.
        /// </summary>
        [TearDown]
        public new void TearDown()
        {
            mouseGrab = null;
            h = null;
            basket = null;

        }
        /// <summary>
        /// Pass Grab test 
        /// </summary>
        /// <returns></returns>
        public override Grab PassGrab()
        {
            GameObject player = new GameObject("player");
            return new MouseGrab(player, highlightColor);
        }

        /// <summary>
        /// Test to verify that the object is being grabbed
        /// </summary>
        [Test]
        public void GrabTest1()
        {
            mouseGrab.GrabHighlightedObject();
            mouseGrab.DropObject();

            Assert.IsFalse(mouseGrab.IsGrabbing());
        }

        /// <summary>
        /// Test to verify that the grabbed object is now dropped
        /// </summary>
        [Test]
        public void DropObject1()
        {
            mouseGrab.DropObject();
            Assert.AreEqual(h.items.Count, 0);
        }

        /// <summary>
        /// Test for verifying if the highlighted object is grabbed
        /// </summary>
        [Test]
        public void GrabTest2()
        {
            mouseGrab.GrabHighlightedObject();
            Assert.AreEqual(mouseGrab.GrabbedObject, highlighted);
        }

        /// <summary>
        /// Constructor test
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            Assert.NotNull(this.mouseGrab);
        }

        #endregion Methods
    }
}