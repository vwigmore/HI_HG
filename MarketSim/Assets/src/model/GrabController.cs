using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Assets.src.model
{
    /// <summary>
    /// Controls the grab function.
    /// </summary>
    abstract class GrabController
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
        /// Object currently selected.
        /// </summary>
        private GameObject grabbedObject;

        /// <summary>
        /// Detects objects to grab.
        /// </summary>
        abstract void detectObjects();

        /// <summary>
        /// Grab selected object.
        /// </summary>
        abstract void grabObject();

        /// <summary>
        /// Drop currently grabbed object.
        /// </summary>
        abstract void dropObject();

        /// <summary>
        /// Highlight selected object
        /// </summary>
        /// <param name="obj">Object to highlight</param>
        abstract void colorSelectedObject(GameObject obj);

        /// <summary>
        /// Clear previously highlighted objects
        /// </summary>
        abstract void clearSelectionColors();
    }
}
