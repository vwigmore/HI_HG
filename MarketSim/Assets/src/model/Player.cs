using UnityEngine;
using System.Collections;

public class Player
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

    #region Constructors
    public Player(CharacterController player, GameObject model, GameObject hip, GameObject leftFoot, GameObject rightFoot)
    {
        pc = player;
        this.model = model;
        this.hip = hip;
        this.leftFoot = leftFoot;
        this.rightFoot = rightFoot;
    }
    #endregion Constructors

    #region Methods

    public void UpdateCrouch()
    {
        float minFootY = Mathf.Min(this.leftFoot.transform.position.y, this.rightFoot.transform.position.y);
        float deltaHeight = hip.transform.position.y - minFootY;
        Vector3 crouchDir = this.model.transform.position;
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

        this.model.transform.position = crouchDir;
        lastDeltaHeight = deltaHeight;
    }

    public void updateRotation()
    {
        mouseAxisY = Input.GetAxis("Mouse Y");
        cam_vert_rot -= mouseAxisY;

        cam_vert_rot = Mathf.Min(Mathf.Max(-cam_cap, cam_vert_rot), cam_cap);

        Camera.main.transform.localRotation = Quaternion.Euler(cam_vert_rot, 0, 0);

        mouseAxisX = Input.GetAxis("Mouse X");
        Vector3 rotAngles = new Vector3(0, mouseAxisX * mouseSensitivity, 0);
        pc.transform.Rotate(rotAngles);
    }


    public void updateMovement()
    {
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        if (pc.isGrounded)
        {
            // movement vector, consists of axes to move to.
            this.moveDirection = new Vector3(hAxis, 0, vAxis);

            // Transforms direction from local space to world space.
            this.moveDirection = pc.transform.TransformDirection(moveDirection);

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
    #endregion Methods
}
