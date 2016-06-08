using System.Collections;
using UnityEngine;

/**
 * Player Controller Script
 * Controls the player character in the simulation.
 * Simulation uses mouse and keyboard for the time being.
 *
 * Author: Wing Nguyen
 **/

/// <summary>
/// 
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// The player controller 
    /// </summary>
    private CharacterController pc;

    /// <summary>
    /// The move direction
    /// </summary>
    private Vector3 moveDirection;

    /// <summary>
    /// The player height
    /// </summary>
    private float playerHeight;

    /// <summary>
    /// The crouch height
    /// </summary>
    private float crouchHeight;

    /// <summary>
    /// The cam_vert_rot
    /// </summary>
    private float cam_vert_rot = 0;

    /// <summary>
    /// The cam_cap
    /// </summary>
    private float cam_cap = 90;

    /// <summary>
    /// The mouse axis y
    /// </summary>
    private float mouseAxisY;

    /// <summary>
    /// The mouse axis x
    /// </summary>
    private float mouseAxisX;

    /// <summary>
    /// The v axis
    /// </summary>
    private float vAxis;

    /// <summary>
    /// The h axis
    /// </summary>
    private float hAxis;

    /// <summary>
    /// The head
    /// </summary>
    private GameObject head, hip, leftFoot, rightFoot;

    /// <summary>
    /// The last delta height
    /// </summary>
    private float lastDeltaHeight, initialDeltaHeight;

    /// <summary>
    /// The model
    /// </summary>
    private GameObject model;

    /// <summary>
    /// The player
    /// </summary>
    private Player player;

    #endregion Fields

    #region Methods

    /// <summary>
    /// moves the player forward.
    /// </summary>
    public void walkForward()
    {
        pc.Move(pc.transform.forward * Time.deltaTime);
    }

    /// <summary>
    ///  moves the player backwards.
    /// </summary>
    public void walkBackwards()
    {
        pc.Move(-pc.transform.forward * Time.deltaTime);
    }

    /// <summary>
    /// Rotates the player to the right.
    /// </summary>
    public void rotateRight()
    {
        pc.transform.Rotate(new Vector3(0, 50 * Time.deltaTime, 0));
    }

    /// <summary>
    /// Rotates the player to the left.
    /// </summary>
    public void rotateLeft()
    {
        pc.transform.Rotate(new Vector3(0, -50 * Time.deltaTime, 0));
    }

    /// <summary>
    /// Check if the player is in proximity.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
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
        model = GameObject.Find("KinectPointMan");
        moveDirection = Vector3.zero;

        playerHeight = pc.height;
        crouchHeight = playerHeight / 2;

        head = GameObject.Find("03_Head");
        hip = GameObject.Find("30_Hip_Left");
        leftFoot = GameObject.Find("33_Foot_Left");
        rightFoot = GameObject.Find("43_Foot_Right");

        UpdateCamPos();

        player = new Player(pc, model, hip, leftFoot, rightFoot);

        initialDeltaHeight = hip.transform.position.y - leftFoot.transform.position.y;
        lastDeltaHeight = initialDeltaHeight;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
#if UNITY_EDITOR
        /* Controls exiting editor application by pressing ESC */
        if (Input.GetKey(KeyCode.Escape))
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        /* Update movement of player*/
        UpdateMove();
    }

    /// <summary>
    /// Updates the cam position.
    /// </summary>
    private void UpdateCamPos()
    {
        ///
    }

    /// <summary>
    /// Updates the movement of the player.
    /// </summary>
    private void UpdateMove()
    {
        player.updateRotation();
        player.updateMovement();
    }

    #endregion Methods
}