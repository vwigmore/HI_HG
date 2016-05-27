using Assets.src.model;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [TestFixture]
    public abstract class GrabTest
    {
        #region Fields

        private Grab grab;
        private GameObject pickUpItem;

        #endregion Fields

        #region Methods

        public abstract Grab PassGrab();

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

            Debug.Log("color: " + this.pickUpItem.GetComponent<MeshRenderer>().sharedMaterial.color);
        }

        [TearDown]
        public void TearDown()
        {
            this.grab = null;
            this.pickUpItem = null;
        }

        [Test]
        public void TestConstructorNotNull()
        {
            Assert.IsNotNull(this.grab);
        }

        [Test]
        public void TestHighlightSelectedObject()
        {
            Assert.IsNull(this.grab.highlighted);
            this.grab.HighlightSelectedObject(this.pickUpItem);
            Assert.IsNotNull(this.grab.highlighted);
            Assert.AreEqual(this.pickUpItem, this.grab.highlighted);
        }

        #endregion Methods
    }
}