using Assets.src.model;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;

[TestFixture]
public class ItemHolderTest
{
    #region Fields

    private ItemHolder itemH;

    #endregion Fields

    #region Methods

    [SetUp]
    public void Setup()
    {
        GameObject basket = new GameObject("basket");
        basket.AddComponent<Rigidbody>();
        basket.AddComponent<BoxCollider>();
        itemH = new ItemHolder(basket, 2, 4);
    }

    [TearDown]
    public void TearDown()
    {
        itemH = null;
    }

    [Test]
    public void TestSizeOfOffsets()
    {
        itemH.InitOffsets();
        Assert.AreEqual(itemH.offsets.Length, 2 * 4);
    }

    #endregion Methods
}