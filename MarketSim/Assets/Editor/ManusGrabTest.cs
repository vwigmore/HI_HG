using Assets.src.model;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor
{
    [TestFixture]
    public class ManusGrabTest : GrabTest
    {
        #region Fields

        /// <summary>
        /// The mouse grab
        /// </summary>
        private ManusGrab manusGrab;

        /// <summary>
        /// The highlight color
        /// </summary>
        private Color highlightColor = Color.green;

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
            manusGrab = (ManusGrab)PassGrab();
        }

        /// <summary>
        /// TearDown method.
        /// </summary>
        [TearDown]
        public new void TearDown()
        {
            manusGrab = null;
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
            return new ManusGrab(player, highlightColor);
        }   

        /// <summary>
        /// Test for verifying if the highlighted object is grabbed
        /// </summary>
        [Test]
        public void TestGrab()
        {
            manusGrab.GrabHighlightedObject();
            Assert.AreEqual(manusGrab.GrabbedObject, highlighted);
        }

        /// <summary>
        /// Constructor test
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            Assert.NotNull(this.manusGrab);
        }

        #endregion Methods
    }
}