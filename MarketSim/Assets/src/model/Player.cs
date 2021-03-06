﻿using UnityEngine;

/// <summary>
/// Class player manages all player functions.
/// </summary>
public class Player
{
    #region Fields

    /// <summary>
    /// The mouse sensitivity
    /// </summary>
    public float mouseSensitivity = 3.0f;

    /// <summary>
    /// The move speed.
    /// </summary>
    public float moveSpeed = 3.0f;

    /// <summary>
    /// The jump speed.
    /// </summary>
    public float jumpSpeed = 5.0f;

    /// <summary>
    /// The gravity.
    /// </summary>
    public float gravity = 20f;

    /// <summary>
    /// Character controller for the player.
    /// </summary>
    public CharacterController pc;

    /// <summary>
    /// The hip.
    /// </summary>
    public GameObject hip, leftFoot, rightFoot;

    /// <summary>
    /// The model.
    /// </summary>
    public GameObject model;

    /// <summary>
    /// The move direction.
    /// </summary>
    private Vector3 moveDirection;

    /// <summary>
    /// The player height.
    /// </summary>
    private float playerHeight;

    /// <summary>
    /// The crouch height.
    /// </summary>
    private float crouchHeight;

    /// <summary>
    /// The cam_vert_rot.
    /// </summary>
    private float cam_vert_rot = 0;

    /// <summary>
    /// The cam_cap.
    /// </summary>
    private float cam_cap = 90;

    /// <summary>
    /// The mouse axis y.
    /// </summary>
    private float mouseAxisY;

    /// <summary>
    /// The mouse axis x.
    /// </summary>
    private float mouseAxisX;

    /// <summary>
    /// The v axis.
    /// </summary>
    private float vAxis;

    /// <summary>
    /// The h axis.
    /// </summary>
    private float hAxis;

    /// <summary>
    /// The last delta height.
    /// </summary>
    private float lastDeltaHeight, initialDeltaHeight;

    #endregion Fields

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="model">The model.</param>
    /// <param name="hip">The hip.</param>
    /// <param name="leftFoot">The left foot.</param>
    /// <param name="rightFoot">The right foot.</param>
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

    /// <summary>
    /// Updates the player position when crouching.
    /// </summary>
    public void UpdateCrouch()
    {
        float minFootY = Mathf.Min(this.leftFoot.transform.position.y, this.rightFoot.transform.position.y);
        float deltaHeight = hip.transform.position.y - minFootY;
        Vector3 crouchDir = this.model.transform.position;
        if (deltaHeight > lastDeltaHeight)
        {
            CrouchDirectionSmallerThanNull(minFootY, deltaHeight, crouchDir);
        }
        else if (deltaHeight < lastDeltaHeight)
        {
            CrouchDirectionGreaterThanNull(minFootY, deltaHeight, crouchDir);
        }
        this.model.transform.position = crouchDir;
        lastDeltaHeight = deltaHeight;
    }

    /// <summary>
    /// Sets the player position to normal level when beneath
    /// </summary>
    /// <param name="min">The minimum level.</param>
    /// <param name="height">The height.</param>
    /// <param name="direction">The direction.</param>
    public void CrouchDirectionSmallerThanNull(float min, float height, Vector3 direction)
    {
        if (min + (height - lastDeltaHeight) <= 0)
            direction.y += height - lastDeltaHeight;
    }

    /// <summary>
    /// Sets the player position to normal level when above.
    /// </summary>
    /// <param name="min">The minimum.</param>
    /// <param name="height">The height.</param>
    /// <param name="direction">The direction.</param>
    public void CrouchDirectionGreaterThanNull(float min, float height, Vector3 direction)
    {
        if (min - (lastDeltaHeight - height) >= 0)
            direction.y -= lastDeltaHeight - height;
    }

    /// <summary>
    /// Updates the rotation.
    /// </summary>
    public void updateRotation()
    {
        if (Input.GetKey(KeyCode.Q))
            pc.transform.Rotate(new Vector3(0, -50 * Time.deltaTime, 0));
        else if (Input.GetKey(KeyCode.E))
            pc.transform.Rotate(new Vector3(0, 50 * Time.deltaTime, 0));

        mouseAxisY = Input.GetAxis("Mouse Y");
        cam_vert_rot -= mouseAxisY;

        cam_vert_rot = Mathf.Min(Mathf.Max(-cam_cap, cam_vert_rot), cam_cap);

        if (Manager.MKBOnly)
            Camera.main.transform.localRotation = Quaternion.Euler(cam_vert_rot, 0, 0);

        mouseAxisX = Input.GetAxis("Mouse X");
        Vector3 rotAngles = new Vector3(0, mouseAxisX * mouseSensitivity, 0);
        pc.transform.Rotate(rotAngles);
    }

    /// <summary>
    /// Caps the vector.
    /// </summary>
    /// <param name="vec">The vec.</param>
    /// <param name="max">The maximum.</param>
    /// <param name="min">The minimum.</param>
    /// <returns></returns>
    public Vector3 capVector(Vector3 vec, float max, float min)
    {
        vec.x = Mathf.Min(Mathf.Max(vec.x, min), max);
        vec.z = Mathf.Min(Mathf.Max(vec.z, min), max);
        return vec;
    }

    /// <summary>
    /// Updates the movement.
    /// </summary>
    public void updateMovement()
    {
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        if (pc.isGrounded)
        {
            updateMovementHelp();
        }
        else
        {
            this.moveDirection.y -= gravity * Time.deltaTime;
        }

        pc.Move(moveDirection * Time.deltaTime);
    }

    /// <summary>
    /// Helper method for updateMovement.
    /// </summary>
    public void updateMovementHelp()
    {
        // movement vector, consists of axes to move to.
        this.moveDirection = new Vector3(hAxis, 0, vAxis);

        if (Input.GetButton("Jump"))
            moveDirection.y = jumpSpeed;

        // Transforms direction from local space to world space.
        this.moveDirection = pc.transform.TransformDirection(moveDirection);

        // Multiplies vector with speed (axes are between -1 and 1, not much).
        this.moveDirection *= moveSpeed;

        // Capping move speed: moveDirection hypotenuse
        capVector(moveDirection, moveSpeed, -moveSpeed);
    }

    #endregion Methods
}