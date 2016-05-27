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

    float x1 = -0.1f, y1 = 0.0f, z1 = -17.4f;
    float x2 = -0.2f, y2 = 0.0f, z2 = -17.1f;
    float x3 = 0.1f, y3 = 0.0f, z3 = -17.1f;
    float x4 = 1.0f, y4 = 2.0f, z4 = 3.0f;
    float x5 = 10.0f, y5 = 4.0f, z5 = 8.0f;

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
    public void UpdateCrouchTestModel()
    {

        Vector3 position1 = new Vector3(x1, y1, z1);
        player.UpdateCrouch();
        Assert.AreEqual(player.model.transform.position.ToString(), position1.ToString());
    }

    [Test]
    public void UpdateCrouchTestLeftFoot()
    {
       
        Vector3 position2 = new Vector3(x2, y2, z2);
        player.UpdateCrouch();
        Assert.AreEqual(player.leftFoot.transform.position.ToString(), position2.ToString());
    }

    [Test]
    public void UpdateCrouchTestrightFoot()
    {

        Vector3 position3 = new Vector3(x3, y3, z3);
        player.UpdateCrouch();
        Assert.AreEqual(player.rightFoot.transform.position.ToString(), position3.ToString());
    }
   
    [Test]
    public void capVectorTest1()
    {
        Vector3 expected1 = new Vector3(x4, y4, z4);
        Vector3 vec1 = new Vector3(1,2,3);
        player.capVector(vec1, 3, 6);
        Assert.AreEqual(vec1.ToString(), expected1.ToString());
    }

    [Test]
    public void capVectorTest2()
    {
        Vector3 expected2 = new Vector3(x5, y5, z5);
        Vector3 vec2 = new Vector3(10, 4, 8);
        player.capVector(vec2, 4, 8);
        Assert.AreEqual(vec2.ToString(), expected2.ToString());
    }
}
#endregion Methods
