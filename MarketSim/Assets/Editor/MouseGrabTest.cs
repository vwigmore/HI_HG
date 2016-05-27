using Assets.src.model;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [TestFixture]
    public class MouseGrabTest : GrabTest
    {
        #region Fields

        private MouseGrab mouseGrab;
        Color highlightColor = Color.cyan;
        private RaycastHit hit;
        GameObject highlighted;
        ItemHolder h;

        float x = 0.0f, y = 0.0f, z = 0.0f;
        #endregion Fields

        #region Methods

        [SetUp]
        public new void Setup()
        {

            GameObject basket = new GameObject("basket");
            basket.AddComponent<Rigidbody>();
            basket.AddComponent<BoxCollider>();
            h = new ItemHolder(basket, 2, 4);
            // GameObject player = new GameObject("player");
            //  mouseGrab = new MouseGrab(player, highlightColor);
            mouseGrab = (MouseGrab)PassGrab();
           // Debug.Log("maouseGrab: " + mouseGrab.GrabbedObject.GetComponent<Collider>());
        }

        public override Grab PassGrab()
        {
            GameObject player = new GameObject("player");
            return new MouseGrab(player, Color.cyan);
        }

        [Test]
        public void GrabTest1()
        {
            mouseGrab.GrabHighlightedObject();
            mouseGrab.DropObject();
          
            Assert.IsFalse(mouseGrab.IsGrabbing());
        }

        [Test]
        public void DropObject1()
        {
            mouseGrab.DropObject();
            Assert.AreEqual(h.items.Count, 0);
        }

        [Test]
        public void GrabTest2()
        {
            mouseGrab.GrabHighlightedObject();
            Assert.AreEqual(mouseGrab.GrabbedObject, highlighted );
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.NotNull(this.mouseGrab);
        }
        #endregion Methods
    }
}
