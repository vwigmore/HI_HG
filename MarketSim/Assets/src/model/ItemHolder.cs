﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.model
{
    public class ItemHolder
    {
        #region Fields

        /// <summary>
        /// The holder
        /// </summary>
        public GameObject holder;

        /// <summary>
        /// The items in the holder
        /// </summary>
        public ArrayList items;

        /// <summary>
        /// Rows of grid.
        /// </summary>
        public int rows;

        /// <summary>
        /// Columns of grid.
        /// </summary>
        public int cols;

        /// <summary>
        /// X coordinate of start position.
        /// </summary>
        private float startX;

        /// <summary>
        /// Y coordinate of start position.
        /// </summary>
        private float startY;

        /// <summary>
        /// Z coordinate of start position.
        /// </summary>
        private float startZ;

        /// <summary>
        /// Width of a cell
        /// </summary>
        private float cellWidth;

        /// <summary>
        /// Height of a cell
        /// </summary>
        private float cellHeight;

        /// <summary>
        /// Offsets for each cell.
        /// </summary>
        public Vector3[] offsets { get; private set; }

        #endregion Fields

        #region Constructors

        public ItemHolder(GameObject hd, int r, int c)
        {
            this.holder = hd;
            this.items = new ArrayList();
            this.rows = r;
            this.cols = c;

            this.cellWidth = this.holder.GetComponent<BoxCollider>().bounds.size.x / this.cols;
            this.cellHeight = this.holder.GetComponent<BoxCollider>().bounds.size.z / this.rows;
            this.startX = this.holder.transform.position.x - (this.cellWidth / 2);
            this.startY = this.holder.transform.position.y;
            this.startZ = this.holder.transform.position.z - (this.cellHeight / 2);

            this.InitOffsets();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Calculate offset for each cell
        /// </summary>
        public void InitOffsets()
        {
            offsets = new Vector3[this.rows * this.cols];
            int index = 0;
            for (int i = 0; i < this.cols; i++)
            {
                float newX = this.startX + i * this.cellWidth / 1.5f - 0.06f;
                for (int j = 0; j < this.rows; j++)
                {
                    float newZ = this.startZ + j * this.cellHeight / 1.8f + 0.08f;
                    this.offsets[index] = this.holder.transform.position - (new Vector3(newX, this.startY, newZ));
                    index++;
                }
            }
        }

        /// <summary>
        /// Updates the list with items that are in the basket
        /// </summary>
        public void UpdateList()
        {
            for (int i = 0; i < this.items.Count; i++)
            {
                GameObject o = (GameObject)this.items[i];
                Vector3 newpos = this.holder.transform.position;
                newpos += this.offsets[i];
                newpos.y += 0.15f;
                o.GetComponent<Collider>().enabled = false;
                o.transform.position = newpos;
            }
        }

        #endregion Methods
    }
}