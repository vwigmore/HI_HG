using Assets.src.model;
using System.Collections;
using UnityEngine;

public class MouseGrabController : MonoBehaviour
{
    #region Fields

    private MouseGrab mouseGrab;
    private Camera camera;

    #endregion Fields

    #region Methods

    // Use this for initialization
    private void Start()
    {
        camera = Camera.main;
        mouseGrab = new MouseGrab(camera.gameObject, Color.blue);
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.transform.gameObject;
            Debug.DrawLine(camera.transform.position, hit.point, Color.blue);
            if (objectHit != null)
            {
                mouseGrab.highlightSelectedObject(objectHit);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!mouseGrab.isGrabbing())
                {
                    mouseGrab.grabHighlightedObject();
                }
                else
                {
                    mouseGrab.setHit(hit);
                    mouseGrab.dropObject();
                }
            }
        }
        mouseGrab.updateGrabbedObject();
    }

    #endregion Methods
}