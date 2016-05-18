using System.Collections;
using UnityEngine;

/**
 * Player Controller Script
 * Controls the player character in the simulation.
 * Simulation uses mouse and keyboard for the time being.
 *
 * Author: Wing Nguyen
 **/

public class PlayerController : MonoBehaviour
{
    #region Fields

    public float mouseSensitivity = 3.0f;
    public float moveSpeed = 3.0f;
    public float jumpSpeed = 5.0f;
    public float gravity = 20f;
    private CharacterController pc;
    private Vector3 moveDirection;
    private float playerHeight;
    private float crouchHeight;

    private float cam_vert_rot = 0;
    private float cam_cap = 90;

    private float mouseAxisY;
    private float mouseAxisX;

    private float vAxis;
    private float hAxis;

    private GameObject leftFoot, rightFoot;
    private float crouchRange, deltaHeight;

    private GameObject model;

    #endregion Fields

    #region Methods

    public void walkForward()
    {
        pc.Move(pc.transform.forward * Time.deltaTime);
    }

    public void walkBackwards()
    {
        pc.Move(-pc.transform.forward * Time.deltaTime);
    }

    public void rotateRight()
    {
        pc.transform.Rotate(new Vector3(0, 50 * Time.deltaTime, 0));
    }

    private bool inProximity(GameObject obj)
    {
        return (Vector3.Distance(pc.transform.position, obj.transform.position) <= 3f);
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pc = GetComponent<CharacterController>();
        model = GameObject.Find("playermodel");
        moveDirection = Vector3.zero;

        playerHeight = pc.height;
        crouchHeight = playerHeight / 2;

        leftFoot = GameObject.Find("left_foot");
        rightFoot = GameObject.Find("right_foot");

        crouchRange = 0.4f;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        /* Controls exiting editor application by pressing ESC */
        if (Input.GetKey(KeyCode.Escape))
            UnityEditor.EditorApplication.isPlaying = false;

        /* Update movement of player*/
        UpdateMove();
        UpdateCrouch();
    }

    /// <summary>
    /// Updates the movement of the player.
    /// </summary>
    private void UpdateMove()
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
            this.moveDirection = new Vector3(hAxis, 0, vAxis);

            // Transforms direction from local space to world space.
            this.moveDirection = transform.TransformDirection(moveDirection);

            // Multiplies vector with speed (axes are between -1 and 1, not much).
            this.moveDirection *= moveSpeed;
            if (Input.GetButton("Jump"))

                this.moveDirection.y = jumpSpeed;

            // Capping move speed: moveDirection hypotenuse
            if (this.moveDirection.x < -moveSpeed)
                this.moveDirection.x = -moveSpeed;
            else if (this.moveDirection.x > moveSpeed)
                this.moveDirection.x = moveSpeed;

            if (this.moveDirection.z < -moveSpeed)
                this.moveDirection.z = -moveSpeed;
            else if (this.moveDirection.z > moveSpeed)
                this.moveDirection.z = moveSpeed;
        }
        else
        {
            this.moveDirection.y -= gravity * Time.deltaTime;
        }

        pc.Move(moveDirection * Time.deltaTime);
    }

    /// <summary>
    /// Updates transform for crouching.
    /// </summary>
    private void UpdateCrouch()
    {
        float minFootY = Mathf.Min(this.leftFoot.transform.position.y, this.rightFoot.transform.position.y);
        Vector3 crouchDir = this.model.transform.position;
        if (minFootY > this.crouchRange)
        {
            crouchDir.y = -0.2f;
        }
        else if (minFootY < 0f)
        {
            crouchDir.y = 0.2f;
        }

        this.model.transform.position = crouchDir;
    }

    #endregion Methods
}