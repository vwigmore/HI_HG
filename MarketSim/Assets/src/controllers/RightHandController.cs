using UnityEngine;
using System.Collections;

public class RightHandController : MonoBehaviour
{

    private GameObject root;
    private GameObject hand;

    // Use this for initialization
    void Start()
    {
        this.hand = GameObject.Find("Manus_Handv2_Right");
        this.root = GameObject.Find("right_wrist");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newpos = this.root.transform.position;
        this.hand.transform.position = newpos;
        //Debug.Log(this.root.transform.rotation);
        this.hand.transform.rotation = this.root.transform.rotation;
        this.hand.transform.Rotate(Vector3.up, 90);
        this.hand.transform.Rotate(Vector3.forward, -90);
    }
}
