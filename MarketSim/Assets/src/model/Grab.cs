using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.model
{
    /// <summary>
    /// Controls the grab function.
    /// </summary>
    internal abstract class Grab
    {
        #region Fields

        #region Properties

        /// <summary>
        /// An object in proximity has to be within this distance.
        /// </summary>
        protected float proxDist = 4.0f;

        /// <summary>
        /// GameObject player.
        /// </summary>
        protected GameObject player;

        /// <summary>
        /// Previously selected gameobject.
        /// Used to restore their colors when they're not selected.
        /// </summary>
        protected GameObject prevHighlighted;

        /// <summary>
        /// Previoulsy highlighted gameobject's color.
        /// </summary>
        protected Color prevHighlightedColor;

        /// <summary>
        /// Object currently selected.
        /// </summary>
        protected GameObject highlighted;

        /// <summary>
        /// Color used to highlight selected objects.
        /// </summary>
        protected Color highlightColor;

        /// <summary>
        /// The GameObject that the grabbed item will follow.
        /// </summary>
        protected GameObject grabber { set; get; }

        /// <summary>
        /// Object currently selected.
        /// </summary>
        protected GameObject grabbedObject { set; get; }

        #endregion Properties

        #region Methods

        public

        #endregion Methods

 Grab(GameObject grabber, Color highlightColor)
        {
            this.grabber = grabber;
            this.player = GameObject.FindGameObjectsWithTag("Player")[0];
            this.prevHighlightedColor = Color.clear;
            this.prevHighlighted = null;
            this.highlighted = null;
            this.highlightColor = highlightColor;
            this.grabbedObject = null;
        }

        /// <summary>
        /// Grab selected object.
        /// </summary>
        public void grabHighlightedObject()
        {
            if (this.highlighted != null)
            {
                this.grabbedObject = highlighted;
            }
        }

        /// <summary>
        /// Drop currently grabbed object.
        /// </summary>
        public abstract void dropObject();

        public void updateGrabbedObject()
        {
            if (isGrabbing())
            {
                Vector3 newpos = grabber.transform.position + grabber.transform.forward * .2f;
                grabbedObject.transform.position = newpos;
                grabbedObject.GetComponent<Collider>().enabled = false;
            }
        }

        /// <summary>
        /// Tells whether an object is being grabbed.
        /// </summary>
        /// <returns>True if an object is currently being held, else false.</returns>
        public bool isGrabbing()
        {
            return (grabbedObject != null);
        }

        /// <summary>
        /// Highlight selected object
        /// </summary>
        /// <param name="obj">Object to highlight</param>
        public void highlightSelectedObject(GameObject obj)
        {
            clearHighlights();
            if (obj.tag.Equals("pickup") && inProximity(obj))
            {
                prevHighlighted = obj;
                prevHighlightedColor = obj.GetComponent<Renderer>().material.color;
                highlighted = obj;
                obj.GetComponent<Renderer>().material.color = highlightColor;
            }
            else
            {
                highlighted = null;
            }
        }

        /// <summary>
        /// Clear previously highlighted objects
        /// </summary>
        public void clearHighlights()
        {
            if (prevHighlighted != null)
                prevHighlighted.GetComponent<Renderer>().material.color = prevHighlightedColor;
        }

        /// <summary>
        /// Returns whether an object is in range of the grabber.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if object is in range, else false.</returns>
        public bool inProximity(GameObject obj)
        {
            return (Vector3.Distance(grabber.transform.position, obj.transform.position) <= proxDist);
        }

        /// <summary>
        /// Returns whether an object is in range of the grabber.
        /// </summary>
        /// <param name="pos">The position to check.</param>
        /// <returns>True if object is in range, else false.</returns>
        public bool inProximity(Vector3 pos)
        {
            return (Vector3.Distance(grabber.transform.position, pos) <= proxDist);
        }

        #endregion Fields
    }
}