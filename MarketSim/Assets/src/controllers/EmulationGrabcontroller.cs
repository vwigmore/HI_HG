using UnityEngine;
using System.Collections;

public class GrabController : MonoBehaviour
{
    public Camera camera;
    public GameObject player;
    private ArrayList prevItems;
    private ArrayList prevColors;

    private GameObject selected;
    private Color selectColor;
    private GameObject grabbing;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        camera = GetComponent<Camera>();
        prevItems = new ArrayList();
        prevColors = new ArrayList();

        selectColor = Color.green;
        selected = null;
        grabbing = null;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        // If ray cast hit an object
        if (Physics.Raycast(ray, out hit))
        {
            //Transform objectHit = hit.transform;
            GameObject objectHit = hit.transform.gameObject;

            Debug.DrawLine(camera.transform.position, hit.point, Color.green);

            // If object is a pickup object
            if (objectHit.tag.Equals("pickup") && player.GetComponent<PlayerController>().inProximity(objectHit))
            {
                selected = objectHit;

                Color objColor = objectHit.GetComponent<Renderer>().material.color;
                if (!objColor.Equals(selectColor) && grabbing == null)
                    colorObject(objectHit);
            }
            else
            {
                selected = null;
            }

            clearColors();

        }

        // Select object at click
        if (Input.GetMouseButtonDown(0))
        {
            if (grabbing == null)
                grabbing = selected;
            else // if object is already grabbed, let go.
            {
                Vector3 newpos = camera.transform.position + camera.transform.forward * 2;

                    newpos.y = 0f;
                    grabbing.transform.position = newpos;
                    grabbing.GetComponent<Collider>().enabled = true;
                    grabbing.GetComponent<Rigidbody>().Sleep();
                    grabbing = null;
            }
        }

        // if object is grabbed, translate/rotate accordingly
        if (grabbing != null)
        {
            Vector3 newpos = camera.transform.position;
            grabbing.transform.position = newpos + camera.transform.forward;
            grabbing.GetComponent<Collider>().enabled = false;
        }
    }

    // HIghlight object
    void colorObject(GameObject obj)
    {
        prevItems.Add(obj);
        prevColors.Add(obj.GetComponent<Renderer>().material.color);
        obj.GetComponent<Renderer>().material.color = selectColor;
    }

    // clear previous highlights.
    void clearColors()
    {
        if (prevItems.Count > 1 || selected == null)
        {
            for (int i = 0; i < prevItems.Count; i++)
            {
                GameObject prev = (GameObject)prevItems[i];
                Color color = (Color)prevColors[i];

                prev.GetComponent<Renderer>().material.color = color;

                prevItems.RemoveAt(i);
                prevColors.RemoveAt(i);
            }
        }
    }
}
