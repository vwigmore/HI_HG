using Assets.src.model;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Tests for the Player class
/// </summary>
[TestFixture]
public class PlayerTest
{
    #region Fields

    /// <summary>
    /// Character controller for the player
    /// </summary>
    CharacterController pc;

    /// <summary>
    /// The model
    /// </summary>
    GameObject model;

    /// <summary>
    /// The hip
    /// </summary>
    GameObject hip;

    /// <summary>
    /// The left foot
    /// </summary>
    GameObject leftFoot;

    /// <summary>
    /// The right foot
    /// </summary>
    GameObject rightFoot;

    /// <summary>
    /// Coordinates for vector position1
    /// </summary>
    float x1 = -0.1f, y1 = 0.0f, z1 = -17.4f;

    /// <summary>
    /// Coordinates for vector position2
    /// </summary>
    float x2 = -0.2f, y2 = 0.0f, z2 = -17.1f;

    /// <summary>
    /// Coordinates for vector position3
    /// </summary>
    float x3 = 0.1f, y3 = 0.0f, z3 = -17.1f;

    /// <summary>
    /// Coordinates for vector capVector1
    /// </summary>
    float x4 = 1.0f, y4 = 2.0f, z4 = 3.0f;

    /// <summary>
    /// Coordinates for vector capVector2
    /// </summary>
    float x5 = 10.0f, y5 = 4.0f, z5 = 8.0f;

    /// <summary>
    /// The player
    /// </summary>
    Player player;

    #endregion Fields

    #region Methods

    /// <summary>
    /// Setups this instance.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        model = GameObject.Find("playermodel");
        hip = GameObject.Find("hip_center");
        leftFoot = GameObject.Find("left_foot");
        rightFoot = GameObject.Find("right_foot");
        CharacterController c = new CharacterController();
        model.AddComponent<CharacterController>();
       pc = model.GetComponent<CharacterController>();
       //pc.height = 0.4f;
       pc.stepOffset = 0.0f;
       Debug.Log("height: " + pc.height + " radius:" + pc.radius + " off: "+pc.stepOffset );


       Debug.Log("gr: " + model.GetComponent<CharacterController>().ToString());
        player = new Player(pc, model, hip, leftFoot, rightFoot);

    }

    /// <summary>
    /// Tears down.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        player = null;
    }

    /// <summary>
    /// Test for the constructor. 
    /// </summary>
    [Test]
    public void TestNull()
    {
        Assert.IsNotNull(player);
    }

    /// <summary>
    /// Test for the UpdateCrouch method
    /// </summary>
    [Test]
    public void UpdateCrouchTestModel()
    {

        Vector3 position1 = new Vector3(x1, y1, z1);
        player.UpdateCrouch();
        Assert.AreEqual(player.model.transform.position.ToString(), position1.ToString());
    }

    /// <summary>
    /// Test for the UpdateCrouch method
    /// </summary>
    [Test]
    public void UpdateCrouchTestLeftFoot()
    {
       
        Vector3 position2 = new Vector3(x2, y2, z2);
        player.UpdateCrouch();
        Assert.AreEqual(player.leftFoot.transform.position.ToString(), position2.ToString());
    }

    /// <summary>
    /// Test for the UpdateCrouch method
    /// </summary>
    [Test]
    public void UpdateCrouchTestrightFoot()
    {

        Vector3 position3 = new Vector3(x3, y3, z3);
        player.UpdateCrouch();
        Assert.AreEqual(player.rightFoot.transform.position.ToString(), position3.ToString());
    }

    /// <summary>
    /// Test for the CapVector method
    /// </summary>
    [Test]
    public void capVectorTest1()
    {
        Vector3 capVector1 = new Vector3(x4, y4, z4);
        Vector3 vec1 = new Vector3(1,2,3);
        player.capVector(vec1, 3, 6);
        Assert.AreEqual(vec1.ToString(), capVector1.ToString());
    }

    /// <summary>
    /// Test for the CapVector method
    /// </summary>
    [Test]
    public void capVectorTest2()
    {
        Vector3 capVector2 = new Vector3(x5, y5, z5);
        Vector3 vec2 = new Vector3(10, 4, 8);
        player.capVector(vec2, 4, 8);
        Assert.AreEqual(vec2.ToString(), capVector2.ToString());
    }

    /// <summary>
    /// Test for the UpdateMovement method
    /// </summary>
    [Test]
    public void UpdateMovementTest()
    {

        Vector3 newPos = new Vector3(x1, y1, z1);
        player.updateMovement();
        Assert.AreEqual(player.pc.transform.position.ToString(), newPos.ToString());
    }

    /// <summary>
    /// Test for the UpdateRotation method
    /// </summary>
    [Test]
    public void UpdateRotationTest()
    {

        Vector3 newPos = new Vector3(x1, y1, z1);
        player.updateRotation();
        Assert.AreEqual(player.pc.transform.position.ToString(), newPos.ToString());
    }
}
#endregion Methods
