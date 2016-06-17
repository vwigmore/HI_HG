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

    /// <summary>
    /// The position of the left foot
    /// </summary>
    private Vector3 posLeft;

    /// <summary>
    /// The position of the right foot
    /// </summary>
    private Vector3 posRight;

    /// <summary>
    /// The initial pc position
    /// </summary>
    private Vector3 initialPcPosition;

    #endregion Fields

    #region Methods

    /// <summary>
    /// setup for the instances.
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

        Vector3 posLeft = leftFoot.transform.position;
        Vector3 posRight = rightFoot.transform.position;
        Vector3 initialPcPosition = pc.transform.position;
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

        Assert.AreNotEqual(player.pc.transform.position.x, initialPcPosition.x);
    }

    /// <summary>
    /// Test for the UpdateCrouch method
    /// </summary>
    [Test]
    public void UpdateCrouchTestLeftFoot()
    {
        player.UpdateCrouch();

        Assert.AreNotEqual(player.leftFoot.transform.position.z, posLeft.z);
        Assert.AreNotEqual(player.leftFoot.transform.position.x, posLeft.x);
    }

    /// <summary>
    /// Test for the UpdateCrouch method
    /// </summary>
    [Test]
    public void UpdateCrouchTestrightFoot()
    {
        player.UpdateCrouch();
        Assert.AreNotEqual(player.rightFoot.transform.position.z, posRight.z);
        Assert.AreNotEqual(player.rightFoot.transform.position.x, posRight.x);
    }

    /// <summary>
    /// Test for the CapVector method
    /// </summary>
    [Test]
    public void capVectorTest1()
    {
        Vector3 vec1 = new Vector3(1, 2, 3);
        Vector3 expected = new Vector3(1, 2, 3);
        player.capVector(vec1, 3, 6);
        Assert.True(vec1.Equals(expected));
    }

    /// <summary>
    /// Test for the UpdateMovement method
    /// </summary>
    [Test]
    public void UpdateMovementTest()
    {
        player.updateMovement();

        Assert.AreNotEqual(player.pc.transform.position.z, initialPcPosition.z);
        Assert.AreNotEqual(player.pc.transform.position.x, initialPcPosition.x);
    }

    /// <summary>
    /// Test for the UpdateRotation method
    /// </summary>
    [Test]
    public void UpdateRotationTest()
    {
        player.updateRotation();

        Assert.AreNotEqual(player.pc.transform.position.z, initialPcPosition.z);
        Assert.AreNotEqual(player.pc.transform.position.x, initialPcPosition.x);
    }
}

#endregion Methods