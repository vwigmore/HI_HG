using Assets.src.model;
using ManusMachina;
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
        private GameObject wrist;

        private IHand hand;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Setups of all instances
        /// </summary>
        [SetUp]
        public new void Setup()
        {
            wrist = GameObject.Find("Wrist");
            wrist.AddComponent<BoxCollider>();
            h = new ItemHolder(wrist, 2, 4);
            manusGrab = (ManusGrab)PassGrab();
            hand = HandFactory.createHand(GLOVE_HAND.GLOVE_RIGHT, wrist.transform);
        }

        /// <summary>
        /// TearDown method.
        /// </summary>
        [TearDown]
        public new void TearDown()
        {
            manusGrab = null;
            h = null;
            wrist = null;
        }

        /// <summary>
        /// Pass Grab test
        /// </summary>
        /// <returns></returns>
        public override Grab PassGrab()
        {
            GameObject player = new GameObject("player");
            return new ManusGrab(player, highlightColor, hand);
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

        #endregion Methods
    }
}