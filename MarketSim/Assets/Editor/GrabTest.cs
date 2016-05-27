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

        #endregion Fields

        #region Methods

        public abstract Grab PassGrab();

        [SetUp]
        public void Setup()
        {
            this.grab = PassGrab();
        }

        [TearDown]
        public void TearDown()
        {
            this.grab = null;
        }

        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(this.grab);
        }

        #endregion Methods
    }
}