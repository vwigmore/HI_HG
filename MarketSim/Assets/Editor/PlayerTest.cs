using Assets.src.model;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;

[TestFixture]
public class PlayerTest
{
    #region Fields

    CharacterController pc;
    GameObject model;
    GameObject hip;
    GameObject leftFoot;
    GameObject rightFoot;

    float x = -0.1f;
    float y = 0.0f;
    float z = -17.4f;

    Player player;

    #endregion Fields

    #region Methods

    [SetUp]
    public void Setup()
    {
        model = GameObject.Find("playermodel");
        hip = GameObject.Find("hip_center");
        leftFoot = GameObject.Find("left_foot");
        rightFoot = GameObject.Find("right_foot");
        player = new Player(pc, model, hip, leftFoot, rightFoot);

    }

    [TearDown]
    public void TearDown()
    {
        player = null;
    }

    [Test]
    public void TestNull()
    {
        Assert.IsNotNull(player);
    }

    [Test]
    public void UpdateCrouchTest()
    {

        Vector3 position = new Vector3(x, y, z);
        player.UpdateCrouch();
        Assert.AreEqual(player.model.transform.position.ToString(), position.ToString());
    }
}
#endregion Methods
