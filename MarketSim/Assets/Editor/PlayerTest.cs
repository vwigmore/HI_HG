using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Tests for the Player class
/// </summary>
[TestFixture]
public class PlayerTest
{
    #region Fields

    /// <summary>
    /// Coordinates for vector position1
    /// </summary>
    private readonly float x1 = -4.0f, y1 = 0.0f, z1 = -13.0f;

    /// <summary>
    /// Coordinates for vector capVector1
    /// </summary>
    private readonly float x2 = 1.0f, y2 = 2.0f, z2 = 3.0f;

    /// <summary>
    /// Coordinates for vector capVector2
    /// </summary>
    private readonly float x3 = 10.0f, y3 = 4.0f, z3 = 8.0f;

    /// <summary>
    /// Coordinates for vector newPos
    /// </summary>
    private readonly float x4 = -4.2f, y4 = 1.0f, z4 = -12.8f;

    /// <summary>
    /// Character controller for the player
    /// </summary>
    private CharacterController pc;

    /// <summary>
    /// The model
    /// </summary>
    private GameObject model;

    /// <summary>
    /// The hip
    /// </summary>
    private GameObject hip;

    /// <summary>
    /// The left foot
    /// </summary>
    private GameObject leftFoot;

    /// <summary>
    /// The right foot
    /// </summary>
    private GameObject rightFoot;

    /// <summary>
    /// The player
    /// </summary>
    private Player player;

    Vector3 pos2;
    Vector3 pos3;

    #endregion Fields

    #region Methods

    /// <summary>
    /// Setups this instance.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        model = GameObject.Find("KinectPointMan");
        hip = GameObject.Find("00_Hip_Center");
        leftFoot = GameObject.Find("33_Foot_Left");
        rightFoot = GameObject.Find("43_Foot_Right");

        pc = model.transform.parent.gameObject.GetComponent<CharacterController>();
        player = new Player(pc, model, hip, leftFoot, rightFoot);

        Vector3 pos2 = player.leftFoot.transform.position;
        Vector3 pos3 = player.rightFoot.transform.position;
        Vector3 initialPcPosition = player.pc.transform.position;
    }

    /// <summary>
    /// Tears down.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        player = null;
        model = null;
        hip = null;
        leftFoot = null;
        rightFoot = null;
        pc = null;
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
 
        player.UpdateCrouch();

        Assert.IsTrue(player.pc.transform.position.z < pos3.z);
        Assert.IsTrue(player.pc.transform.position.x < pos3.x);
        Assert.IsTrue(player.pc.transform.position.z < pos2.z);
        Assert.IsTrue(player.pc.transform.position.x < pos2.x);
    }

    /// <summary>
    /// Test for the UpdateCrouch method
    /// </summary>
    [Test]
    public void UpdateCrouchTestLeftFoot()
    {
              
        player.UpdateCrouch();
   
        Assert.IsTrue(player.leftFoot.transform.position.z < pos2.z);
        Assert.IsTrue(player.leftFoot.transform.position.x < pos3.x);

    }

    /// <summary>
    /// Test for the UpdateCrouch method
    /// </summary>
    [Test]
    public void UpdateCrouchTestrightFoot()
    {
      
        player.UpdateCrouch();
        Assert.IsTrue(player.rightFoot.transform.position.z < pos3.z);
        Assert.IsTrue(player.rightFoot.transform.position.x < pos3.x);
    }

    /// <summary>
    /// Test for the CapVector method
    /// </summary>
    [Test]
    public void capVectorTest1()
    {
        Vector3 capVector1 = new Vector3(x2, y2, z2);
        Vector3 vec1 = new Vector3(1, 2, 3);
        player.capVector(vec1, 3, 6);
        Assert.True(vec1.Equals(capVector1));
    }

    /// <summary>
    /// Test for the CapVector method
    /// </summary>
    [Test]
    public void capVectorTest2()
    {
        Vector3 capVector2 = new Vector3(x3, y3, z3);
        Vector3 vec2 = new Vector3(10, 4, 8);
        player.capVector(vec2, 4, 8);

        Assert.True(vec2.Equals(capVector2));
    }

    /// <summary>
    /// Test for the UpdateMovement method
    /// </summary>
    [Test]
    public void UpdateMovementTest()
    {

        player.updateMovement();

        Assert.IsTrue(player.pc.transform.position.z < pos3.z);
        Assert.IsTrue(player.pc.transform.position.x < pos3.x);
        Assert.IsTrue(player.pc.transform.position.z < pos2.z);
        Assert.IsTrue(player.pc.transform.position.x < pos2.x);
    }

    /// <summary>
    /// Test for the UpdateRotation method
    /// </summary>
    [Test]
    public void UpdateRotationTest()
    {

        player.updateRotation();
  
        Assert.IsTrue(player.pc.transform.position.z < pos3.z);
        Assert.IsTrue(player.pc.transform.position.x < pos3.x);
        Assert.IsTrue(player.pc.transform.position.z < pos2.z);
        Assert.IsTrue(player.pc.transform.position.x < pos2.x);
    }
}

#endregion Methods