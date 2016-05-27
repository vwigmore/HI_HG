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

        #endregion Fields

        #region Methods

        public override Grab PassGrab()
        {
            return new MouseGrab(new GameObject(), Color.cyan);
        }

        #endregion Methods
    }
}