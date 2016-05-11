using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.model
{
    class ManusGrabController : GrabController
    {

        /// <summary>
        /// The GameObject that the grabbed item will follow.
        /// </summary>
        private GameObject grabber;

        /// <summary>
        /// GameObject player.
        /// </summary>
        private GameObject player;

        /// <summary>
        /// List containing previous selected items.
        /// Used to restore their colors when they're not selected.
        /// </summary>
        private ArrayList prevSelectedItems;

        /// <summary>
        /// List containing previous selecte items' colors.
        /// </summary>
        private ArrayList prevSelectedColors;

        /// <summary>
        /// Object currently selected.
        /// </summary>
        private GameObject selected;

        /// <summary>
        /// Color used to highlight selected objects.
        /// </summary>
        private Color selectColor;

        /// <summary>
        /// Object currently grabbed.
        /// </summary>
        private GameObject grabbedObject;

        public ManusGrabController(GameObject grabber)
        {
            this.grabber = grabber;
            this.player = GameObject.FindGameObjectsWithTag("Player")[0];
            this.prevSelectedColors = new ArrayList();
            this.prevSelectedItems = new ArrayList();
            this.selected = null;
            this.selectColor = Color.blue;
            this.grabbedObject = null;

        }

        RaycastHit hit;

        public override void clearSelectionColors()
        {
            throw new NotImplementedException();
        }

        public override void colorSelectedObject(GameObject obj)
        {
            prevSelectedItems.Add(obj);
            prevSelectedColors.Add(obj.GetComponent<Renderer>().material.color);
            obj.GetComponent<Renderer>().material.color = selectColor;
        }

        public override void dropObject()
        {
            if ((1.0 - hit.normal.y * hit.normal.y) <= .04 &&
                  !hit.transform.gameObject.tag.Equals("Player"))
            {
                Vector3 newpos = hit.point;
                newpos.y = 0f;
                grabbedObject.transform.position = newpos;
                grabbedObject.GetComponent<Collider>().enabled = true;
                grabbedObject = null;
            }
        }
        public override void grabObject()
        {
            Ray ray = new Ray();

            GameObject right = GameObject.Find("Manus_Handv2_Right");
            ray.direction = right.transform.forward;

            if(Physics.Raycast(ray,out hit))
            {
                GameObject objHit = hit.transform.gameObject;
                Debug.DrawLine(right.transform.position, hit.point, Color.red);

                if(objHit.tag.Equals("pickup") && inProximity(hit.transform.gameObject))
                    {

                    selected = objHit;
                    Color objColor = objHit.GetComponent<Renderer>().material.color;
                    if (!objColor.Equals(selectColor) && grabbedObject == null)
                       colorSelectedObject(objHit);
                }

                else
                {
                    selected = null;
                }

                clearSelectionColors();
            }
        } 

        public override void moveGrabbedObject()
        {
            throw new NotImplementedException();
        }

        public override bool inProximity(GameObject obj)
        {
            return (Vector3.Distance(grabber.transform.position, obj.transform.position) <= 3f);
        }
    }
}
