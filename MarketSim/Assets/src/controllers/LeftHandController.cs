using UnityEngine;
using System.Collections;

public class LeftHandController : MonoBehaviour {

    private GameObject root;
    private GameObject hand;

	// Use this for initialization
	void Start () {
        this.hand = GameObject.Find("Manus_Handv2_Left");
        this.root = GameObject.Find("left_hand");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newpos = this.root.transform.position;
        this.hand.transform.position = newpos;
        Debug.Log(this.root.transform.rotation);
        this.hand.transform.rotation = this.root.transform.rotation;
        this.hand.transform.Rotate(Vector3.up, -90);
        this.hand.transform.Rotate(Vector3.forward, -90);
	}
}
