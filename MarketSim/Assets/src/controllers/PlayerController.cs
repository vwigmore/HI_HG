using UnityEngine;
using System.Collections;

/**
 * Player Controller Script
 * Controls the player character in the simulation.
 * Simulation uses mouse and keyboard for the time being.
 * 
 * Author: Wing Nguyen 
 **/
public class PlayerController : MonoBehaviour {

    private CharacterController pc;
    private Vector3 moveDirection;

    public float mouseSensitivity = 3.0f;

    public float moveSpeed = 3.0f;
    public float jumpSpeed = 5.0f;
    public float gravity = 20f;

    private float playerHeight;
    private float crouchHeight;

    private float cam_vert_rot = 0;
    private float cam_cap = 90;

    float mouseAxisY;
    float mouseAxisX;

    float vAxis;
    float hAxis;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        pc = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;

        playerHeight = pc.height;
        crouchHeight = playerHeight / 2;
	}
	
	// Update is called once per frame
	void Update () {
        /* Controls exiting editor application by pressing ESC */
        if (Input.GetKey(KeyCode.Escape))
            UnityEditor.EditorApplication.isPlaying = false;

        /* Update movement of player*/
        updateMove();
        updateCrouch();

        GameObject left = GameObject.Find("left_foot");
        GameObject right = GameObject.Find("right_foot");

        // Debug.Log("left: " + left.transform.position + "\t right:" + right.transform.position);
	}

    void updateMove()
    {
        /* Player Camera Controls */
        mouseAxisY = Input.GetAxis("Mouse Y");
        cam_vert_rot -= mouseAxisY;

        // cap vertical rotation of camera
        cam_vert_rot = Mathf.Min(Mathf.Max(-cam_cap, cam_vert_rot), cam_cap);

        Camera.main.transform.localRotation = Quaternion.Euler(cam_vert_rot, 0, 0);
        // Debug.Log(cam_vert_rot);

        mouseAxisX = Input.GetAxis("Mouse X");
        Vector3 rotAngles = new Vector3(0, mouseAxisX * mouseSensitivity, 0);
        transform.Rotate(rotAngles);

        /* Player Movement */

        // Get axis -> between -1 and 1 
        // Discrete if KB, continuous if using joystick.
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        // move the player
        // http://docs.unity3d.com/ScriptReference/CharacterController.Move.html

        // prevents unlimited jumps
        if (pc.isGrounded)
        {
            // movement vector, consists of axes to move to.
            moveDirection = new Vector3(hAxis, 0, vAxis);
            // Transforms direction from local space to world space.
            moveDirection = transform.TransformDirection(moveDirection);
            // Multiplies vector with speed (axes are between -1 and 1, not much).
            moveDirection *= moveSpeed;
            if (Input.GetButton("Jump"))

                moveDirection.y = jumpSpeed;

            // Capping move speed: moveDirection hypotenuse 
            if (moveDirection.x < -moveSpeed)
                moveDirection.x = -moveSpeed;
            else if (moveDirection.x > moveSpeed)
                moveDirection.x = moveSpeed;

            if (moveDirection.z < -moveSpeed)
                moveDirection.z = -moveSpeed;
            else if (moveDirection.z > moveSpeed)
                moveDirection.z = moveSpeed;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        pc.Move(moveDirection * Time.deltaTime);
    }

    void updateCrouch()
    {
        float factor = 10 * Time.deltaTime;

        // crouch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (pc.height > crouchHeight)
            {
                Camera.main.transform.localPosition =
                    Camera.main.transform.localPosition - new Vector3(0, factor, 0);
                pc.height -= factor;
            }
            pc.center = new Vector3(0, -0.5f, 0);
        }

        //standup
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (pc.height < playerHeight)
            {
                Camera.main.transform.localPosition =
                    Camera.main.transform.localPosition + new Vector3(0, factor, 0);
                pc.height += factor;
            }
            pc.center = new Vector3(0, 0, 0);
        }
    }

    public bool inProximity(GameObject obj)
    {
        return (Vector3.Distance(pc.transform.position, obj.transform.position) <= 3f);
    }
}
