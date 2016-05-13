using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.model
{
    internal class MouseGrab : Grab
    {
        #region Fields

        private RaycastHit hit;

        #endregion Fields

        #region Methods

        public MouseGrab(GameObject grabber, Color highlightColor)
            : base(grabber, highlightColor)
        {
            this.grabber = grabber;
            this.highlightColor = highlightColor;
        }

        public override void dropObject()
        {
            if (!inProximity(hit.point))
                return;
            if ((1.0 - hit.normal.y * hit.normal.y) < 0.4 &&
                !hit.transform.gameObject.tag.Equals("Player"))
            {
                Vector3 newpos = hit.point;
                grabbedObject.transform.position = newpos;
                grabbedObject.GetComponent<Collider>().enabled = true;
                grabbedObject.GetComponent<Rigidbody>().Sleep();
                grabbedObject = null;
                highlighted = null;
            }
        }

        public void setHit(RaycastHit hit)
        {
            this.hit = hit;
        }

        #endregion Methods
    }
}