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

    private GameObject hip, leftFoot, rightFoot;
    private float lastDeltaHeight, initialDeltaHeight;

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

    public bool inProximity(GameObject obj)
    {
        return (Vector3.Distance(pc.transform.position, obj.transform.position) <= 3f);
    }

    // Use this for initialization
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pc = GetComponent<CharacterController>();
        model = GameObject.Find("playermodel");
        moveDirection = Vector3.zero;

        playerHeight = pc.height;
        crouchHeight = playerHeight / 2;

        hip = GameObject.Find("hip_center");
        leftFoot = GameObject.Find("left_foot");
        rightFoot = GameObject.Find("right_foot");

        initialDeltaHeight = hip.transform.position.y - leftFoot.transform.position.y;
        lastDeltaHeight = initialDeltaHeight;
    }

    // Update is called once per frame
    private void Update()
    {
        /* Controls exiting editor application by pressing ESC */
        if (Input.GetKey(KeyCode.Escape))
            UnityEditor.EditorApplication.isPlaying = false;

        /* Update movement of player*/
        UpdateMove();
        updateCrouch();
    }

    private void UpdateMove()
    {
        updateRotation();
        updateMovement();
    }

    private void updateRotation()
    {
        mouseAxisY = Input.GetAxis("Mouse Y");
        cam_vert_rot -= mouseAxisY;

        cam_vert_rot = Mathf.Min(Mathf.Max(-cam_cap, cam_vert_rot), cam_cap);

        Camera.main.transform.localRotation = Quaternion.Euler(cam_vert_rot, 0, 0);

        mouseAxisX = Input.GetAxis("Mouse X");
        Vector3 rotAngles = new Vector3(0, mouseAxisX * mouseSensitivity, 0);
        transform.Rotate(rotAngles);
    }

    private void updateMovement()
    {
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        if (pc.isGrounded)
        {
            moveDirection = new Vector3(hAxis, 0, vAxis);

            moveDirection = transform.TransformDirection(moveDirection);

            moveDirection *= moveSpeed;

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

    private void updateCrouch()
    {
        float minFootY = Mathf.Min(leftFoot.transform.position.y, rightFoot.transform.position.y);

        float deltaHeight = hip.transform.position.y - minFootY;

        Vector3 crouchDir = model.transform.position;

        if (deltaHeight > lastDeltaHeight)
        {
            if (minFootY + (deltaHeight - lastDeltaHeight) <= 0)
                crouchDir.y += deltaHeight - lastDeltaHeight;
        }
        else if (deltaHeight < lastDeltaHeight)
        {
            if (minFootY - (lastDeltaHeight - deltaHeight) >= 0)
                crouchDir.y -= lastDeltaHeight - deltaHeight;
        }
        model.transform.position = crouchDir;

        lastDeltaHeight = deltaHeight;
    }

    #endregion Methods
}