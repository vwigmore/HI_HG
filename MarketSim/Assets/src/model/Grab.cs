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
        /// Last position of the grabbed object.
        /// </summary>
        protected Vector3 lastPos;

        /// <summary>
        /// The GameObject that the grabbed item will follow.
        /// </summary>
        protected GameObject grabber { set; get; }

        /// <summary>
        /// Object currently selected.
        /// </summary>
        protected GameObject grabbedObject { set; get; }

        /// <summary>
        /// Basket object.
        /// </summary>
        protected GameObject basket { set; get; }


        /// <summary>
        /// List with items that are in the basket.
        /// </summary>
        protected ArrayList basketItems;


        /// <summary>
        /// Offsets for each cel.
        /// </summary>
        protected Vector3[] offsets;

        /// <summary>
        /// Rows of grid.
        /// </summary>
        public int rows = 2;

        /// <summary>
        /// Columns of grid.
        /// </summary>
        public int cols = 3;

        /// <summary>
        /// X coordinate of start position.
        /// </summary>
        float startX;

        /// <summary>
        /// Y coordinate of start position.
        /// </summary>
        float startY;

        /// <summary>
        /// Z coordinate of start position.
        /// </summary>
        float startZ;

        /// <summary>
        /// Width of a cell
        /// </summary>
        float cellWidth;

        /// <summary>
        /// Height of a cell
        /// </summary>
        float cellHeight;

        /// <summary>
        /// Start position.
        /// </summary>
        public Vector3 start;

        #endregion Properties

        #region Methods

        public

        #endregion Methods

 Grab(GameObject grabber, Color highlightColor)
        {
            this.grabber = grabber;
            this.player = GameObject.FindGameObjectWithTag("Player");
            this.basket = GameObject.FindGameObjectWithTag("basket");
            this.prevHighlightedColor = Color.clear;
            this.prevHighlighted = null;
            this.highlighted = null;
            this.highlightColor = highlightColor;
            this.grabbedObject = null;

            basketItems = new ArrayList();
            
            cellWidth = basket.GetComponent<BoxCollider>().bounds.size.x / cols;
            cellHeight = basket.GetComponent<BoxCollider>().bounds.size.z / rows;
            startX = basket.transform.position.x - (cellWidth / 2);
            startY = basket.transform.position.y;
            startZ = basket.transform.position.z - (cellHeight / 2);

            initOffsets();
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

        public virtual void updateGrabbedObject()
        {
            if (isGrabbing())
            {
                Vector3 newpos = grabber.transform.position + grabber.transform.forward;
                grabbedObject.transform.position = newpos;
                grabbedObject.GetComponent<Collider>().enabled = false;
                lastPos = grabber.transform.position;
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
            if ((obj.tag.Equals("pickup") || obj.tag.Equals("basket")) && inProximity(obj))
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

        /// <summary>
        /// Updates the list with items that are in the basket
        /// </summary>
        public void updateList()
        {
            for (int i = 0; i < basketItems.Count; i++)
            {
                GameObject o = (GameObject)basketItems[i];
                Vector3 newpos = basket.transform.position;
                newpos += offsets[i];
                newpos.y += 0.15f;
                o.GetComponent<Collider>().enabled = false;
                o.transform.position = newpos;

            }

        }

        /// <summary>
        /// Calculate offset for each cell
        /// </summary>
        public void initOffsets()
        {
            offsets = new Vector3[rows * cols];
            int index = 0;
            for (int i = 0; i < cols; i++)
            {
                float newX = startX + i * cellWidth / 1.5f - 0.06f;
                for (int j = 0; j < rows; j++)
                {
                    float newZ = startZ + j * cellHeight / 1.8f + 0.08f;
                    offsets[index] = basket.transform.position - (new Vector3(newX, startY, newZ));
                    index++;
                }
            }
        }

        #endregion Fields
    }
}