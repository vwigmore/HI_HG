using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.model
{
    internal class ManusGrab : Grab
    {
        #region Methods

        public ManusGrab(GameObject grabber, Color highlightColor)
            : base(grabber, highlightColor)
        {
            this.grabber = grabber;
            this.highlightColor = highlightColor;
        }

        public override void dropObject()
        {
            Vector3 newpos = grabbedObject.transform.position;
            newpos.y = 0;
            grabbedObject.transform.position = newpos;
            grabbedObject.GetComponent<Collider>().enabled = true;
            grabbedObject.GetComponent<Rigidbody>().Sleep();
            grabbedObject = null;
        }

        #endregion Methods
    }
}