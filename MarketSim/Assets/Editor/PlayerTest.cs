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
        Vector3 position1 = new Vector3(x1, y1, z1);
        player.UpdateCrouch();
        Vector3 pos1 = player.leftFoot.transform.position;

        position1[0] = Mathf.Round(position1[0]);
        position1[1] = Mathf.Round(position1[1]);
        position1[2] = Mathf.Round(position1[2]);

        pos1[0] = Mathf.Round(pos1[0]);
        pos1[1] = Mathf.Round(pos1[1]);
        pos1[2] = Mathf.Round(pos1[2]);

        Assert.True(pos1.Equals(position1));
    }

    /// <summary>
    /// Test for the UpdateCrouch method
    /// </summary>
    [Test]
    public void UpdateCrouchTestLeftFoot()
    {
        Vector3 position2 = new Vector3(x1, y1, z1);
        player.UpdateCrouch();
        Vector3 pos2 = player.leftFoot.transform.position;

        position2[0] = Mathf.Round(position2[0]);
        position2[1] = Mathf.Round(position2[1]);
        position2[2] = Mathf.Round(position2[2]);

        pos2[0] = Mathf.Round(pos2[0]);
        pos2[1] = Mathf.Round(pos2[1]);
        pos2[2] = Mathf.Round(pos2[2]);

        Assert.True(pos2.Equals(position2));
    }

    /// <summary>
    /// Test for the UpdateCrouch method
    /// </summary>
    [Test]
    public void UpdateCrouchTestrightFoot()
    {
        Vector3 position3 = new Vector3(x1, y1, z1);
        player.UpdateCrouch();
        Vector3 pos3 = player.rightFoot.transform.position;

        position3[0] = Mathf.Round(position3[0]);
        position3[1] = Mathf.Round(position3[1]);
        position3[2] = Mathf.Round(position3[2]);

        pos3[0] = Mathf.Round(pos3[0]);
        pos3[1] = Mathf.Round(pos3[1]);
        pos3[2] = Mathf.Round(pos3[2]);

        Assert.True(pos3.Equals(position3));
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
        Vector3 newPos = new Vector3(x4, y4, z4);
        player.updateMovement();
        Vector3 playerVector1 = player.pc.transform.position;

        playerVector1[0] = Mathf.Round(playerVector1[0]);
        playerVector1[1] = Mathf.Round(playerVector1[1]);
        playerVector1[2] = Mathf.Round(playerVector1[2]);

        newPos[0] = Mathf.Round(newPos[0]);
        newPos[1] = Mathf.Round(newPos[1]);
        newPos[2] = Mathf.Round(newPos[2]);

        Assert.IsTrue(playerVector1.Equals(newPos));
    }

    /// <summary>
    /// Test for the UpdateRotation method
    /// </summary>
    [Test]
    public void UpdateRotationTest()
    {
        Vector3 newPos = new Vector3(x4, y4, z4);
        player.updateRotation();
        Vector3 playerVector2 = player.pc.transform.position;

        playerVector2[0] = Mathf.Round(playerVector2[0]);
        playerVector2[1] = Mathf.Round(playerVector2[1]);
        playerVector2[2] = Mathf.Round(playerVector2[2]);

        newPos[0] = Mathf.Round(newPos[0]);
        newPos[1] = Mathf.Round(newPos[1]);
        newPos[2] = Mathf.Round(newPos[2]);

        Assert.IsTrue(playerVector2.Equals(newPos));
    }
}

#endregion Methods