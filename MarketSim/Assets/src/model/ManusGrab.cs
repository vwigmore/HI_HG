using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.model
{
    internal class ManusGrab : GrabController
    {
        #region Methods

        public ManusGrab(GameObject grabber)
            : base(grabber)
        {
            this.grabber = grabber;
        }

        public override void dropObject()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}