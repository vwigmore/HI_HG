using Assets.src.model;
using System.Collections;
using UnityEngine;

/// <summary>
/// Uses the mouse grab object to implement grabbing with the mouse
/// </summary>
public class MouseGrabController : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// The mouse grab object
    /// </summary>
    private MouseGrab mouseGrab;

    /// <summary>
    /// The camera
    /// </summary>
    private new Camera camera;

    #endregion Fields

    #region Methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        this.camera = Camera.main;
        this.mouseGrab = new MouseGrab(this.camera.gameObject, Color.blue);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        RaycastHit hit;
        Ray ray = this.camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            UpdateHelp(hit);
        }
        if (Input.GetMouseButtonDown(0))
        {
            RayCastHitHelp(hit);
        }
        mouseGrab.UpdateGrabbedObject();
        mouseGrab.basket.UpdateList();
    }

    /// <summary>
    /// Help method for RayCasting.
    /// </summary>
    /// <param name="hit">The hit.</param>
    public void RayCastHitHelp(RaycastHit hit)
    {
        if (!this.mouseGrab.IsGrabbing())
        {
            this.mouseGrab.GrabHighlightedObject();
        }
        else
        {
            this.mouseGrab.SetHit(hit);
            this.mouseGrab.DropObject();
        }
    }

    /// <summary>
    /// Help method for update.
    /// </summary>
    /// <param name="h">The h.</param>
    public void UpdateHelp(RaycastHit h)
    {
        GameObject objectHit = h.transform.gameObject;
        Debug.DrawLine(this.camera.transform.position, h.point, Color.blue);
        if (objectHit != null)
        {
            this.mouseGrab.HighlightSelectedObject(objectHit);
        }
    }
    #endregion Methods
}