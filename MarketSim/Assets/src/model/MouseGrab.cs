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
            if (inProximity(basket.holder) && !grabbedObject.tag.Equals("basket") && !basket.items.Contains(grabbedObject))
            {
                Vector3 newpos = basket.holder.transform.position;
                newpos.y = newpos.y + 0.4f;

                if (basket.items.Count < basket.rows * basket.cols)
                    basket.items.Add(grabbedObject);
            }
            else if (inProximity(cart.holder) && !grabbedObject.tag.Equals("cart") && !cart.items.Contains(grabbedObject))
            {
                Vector3 newpos = cart.holder.transform.position;
                newpos.y = newpos.y + 0.4f;

                if (cart.items.Count < cart.rows * cart.cols)
                    cart.items.Add(grabbedObject);
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