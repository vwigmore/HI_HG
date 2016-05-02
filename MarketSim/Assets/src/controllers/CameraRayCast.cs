using UnityEngine;
using System.Collections;

public class CameraRayCast : MonoBehaviour {
    public Camera camera;
    private ArrayList prevItems;
    private ArrayList prevColors;

    private GameObject selected;
    private Color selectColor;
    private GameObject grabbing;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        prevItems = new ArrayList();
        prevColors = new ArrayList();

        selected = null;
        selectColor = Color.red;
        grabbing = null;
	}
	
	// Update is called once per frame
	void Update () {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //Transform objectHit = hit.transform;
                GameObject objectHit = hit.transform.gameObject;
                
                if (objectHit.tag.Equals("pickup"))
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

                if (selected != null && Input.GetMouseButton(0))
                {
                    Vector3 newpos = selected.transform.position + camera.transform.forward * 2;
                    selected.transform.position = newpos;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (grabbing == null)
                        grabbing = selected;
                    else
                    {
                        Vector3 newpos = camera.transform.position + camera.transform.forward * .3f;
                        grabbing.GetComponent<Rigidbody>().WakeUp();
                        grabbing.transform.position = newpos;                        
                        grabbing = null;
                    }
                }

                if (grabbing != null)
                {
                    Vector3 newpos = camera.transform.position;
                    grabbing.transform.position = newpos + camera.transform.forward;
                    grabbing.GetComponent<Rigidbody>().Sleep();
                    //grabbing.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }

                clearColors();
            }



	}

    void colorObject(GameObject obj)
    {
        prevItems.Add(obj);
        prevColors.Add(obj.GetComponent<Renderer>().material.color);
        obj.GetComponent<Renderer>().material.color = selectColor;
    }

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

    void grabObject(GameObject obj)
    {
        if (obj == null) return;

        Vector3 newpos = obj.transform.position + camera.transform.parent.transform.forward * 2;
        obj.transform.position = newpos;
        //grabbing = true;
    }
}
