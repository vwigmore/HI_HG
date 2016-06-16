using Assets.src.model;
using ManusMachina;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class GestureControllerTest
{
    #region Fields

    private Glove glove;

    #endregion Fields

    #region Methods

    [SetUp]
    public void Setup()
    {
        glove = new Glove(GLOVE_HAND.GLOVE_LEFT);
    }

    [TearDown]
    public void TearDown()
    {
        glove = null;
    }

    [Test]
    public void DetermineGestureTest()
    {
        IMoveGesture gest = GestureController.GetGesture(glove);
        Assert.IsInstanceOf<GestureOpen>(gest);
    }

    #endregion Methods
}