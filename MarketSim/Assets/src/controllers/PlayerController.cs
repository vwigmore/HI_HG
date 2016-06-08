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

    private CharacterController pc;
    private Vector3 moveDirection;
    private float playerHeight;
    private float crouchHeight;

    private float cam_vert_rot = 0;
    private float cam_cap = 90;

    private float mouseAxisY;
    private float mouseAxisX;

    private float vAxis;

    /// <summary>
    /// The h axis
    /// </summary>
    private float hAxis;

    private GameObject head, hip, leftFoot, rightFoot;

    private float lastDeltaHeight, initialDeltaHeight;

    private GameObject model;

    private Player player;

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

    public void rotateLeft()
    {
        pc.transform.Rotate(new Vector3(0, -50 * Time.deltaTime, 0));
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
        model = GameObject.Find("KinectPointMan");
        moveDirection = Vector3.zero;

        playerHeight = pc.height;
        crouchHeight = playerHeight / 2;

        head = GameObject.Find("03_Head");
        hip = GameObject.Find("30_Hip_Left");
        leftFoot = GameObject.Find("33_Foot_Left");
        rightFoot = GameObject.Find("43_Foot_Right");

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

        UpdateCamPos();
    }

    /// <summary>
    /// Updates the cam position.
    /// </summary>
    private void UpdateCamPos()
    {
        if (Manager.MKBOnly)
        {
            Vector3 newpos = Camera.main.transform.position;
            newpos.y = 1.7f;
            Camera.main.transform.position = newpos;
        }
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