using Assets.src.model;
using NUnit.Framework;
using System.Collections;
using UnityEngine;

[TestFixture]
public class UnitTest1
{
    #region Fields

    private ManusGrab ms;

    #endregion Fields

    #region Methods

    [SetUp]
    public void Setup()
    {
        ms = null;
    }

    [TearDown]
    public void TearDown()
    {
        ms = null;
    }

    [Test]
    public void TestMethod()
    {
        Assert.AreEqual(6, 5 + 1);
    }

    #endregion Methods
}