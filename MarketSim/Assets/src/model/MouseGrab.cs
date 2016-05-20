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
            if (inProximity(basket) && !grabbedObject.tag.Equals("basket") && !basketItems.Contains(grabbedObject))
            {                
                Vector3 newpos = basket.transform.position;
                newpos.y = newpos.y + 0.4f;

                if (basketItems.Count < rows * cols)
                    basketItems.Add(grabbedObject);
            }
            else
            {
                grabbedObject.GetComponent<Collider>().enabled = true;
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