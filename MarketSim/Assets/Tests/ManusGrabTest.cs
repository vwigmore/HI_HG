using Assets.src.model;
using NSubstitute;
using NUnit.Framework;
using System.Collections;
using UnityEngine;

[TestFixture]
public class ManusGrabTest
{
    #region Fields

    private ManusGrab ms;
    private ManusGrab gameobj;

    #endregion Fields

    #region Methods

    [SetUp]
    public void Setup()
    {
        ms = null;
        gameobj = Substitute.For<ManusGrab>();
    }

    [TearDown]
    public void TearDown()
    {
        ms = null;
        gameobj = null;
    }

    [Test]
    public void TestMethod()
    {
        Assert.AreEqual(6, 5 + 1);
    }

    #endregion Methods
}