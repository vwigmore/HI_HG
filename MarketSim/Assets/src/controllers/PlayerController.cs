﻿using UnityEngine;

/// <summary>
/// Controls the player character in the simulation.
/// Simulation uses mouse and keyboard for the time being.
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// The charactercontroller.
    /// </summary>
    private CharacterController pc;

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
    /// The parts of the body: head, hip, left foot and right foot.
    /// </summary>
    private GameObject hip, leftFoot, rightFoot;

    /// <summary>
    /// The model.
    /// </summary>
    private GameObject model;

    /// <summary>
    /// The player.
    /// </summary>
    private Player player;

    #endregion Fields

    #region Methods

    /// <summary>
    /// Allows the player to walk forward.
    /// </summary>
    public void walkForward()
    {
        pc.Move(pc.transform.forward * Time.deltaTime);
    }

    /// <summary>
    /// Allows the player to walk backward.
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
    /// Ckecks if the user is in proximity of the object.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    private bool inProximity(GameObject obj)
    {
        return (Vector3.Distance(pc.transform.position, obj.transform.position) <= 3f);
    }

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pc = GetComponent<CharacterController>();
        model = GameObject.Find("KinectPointMan");
        hip = GameObject.Find("30_Hip_Left");
        leftFoot = GameObject.Find("33_Foot_Left");
        rightFoot = GameObject.Find("43_Foot_Right");
        player = new Player(pc, model, hip, leftFoot, rightFoot);
    }

    /// <summary>
    /// Update is called once per frame.
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
        ChangePosition();
    }

    /// <summary>
    /// Changes the  camera position.
    /// </summary>
    public void ChangePosition()
    {
        if (Manager.MKBOnly)
        {
            Vector3 newpos = Camera.main.transform.position;
            newpos.y = 1.85f;
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