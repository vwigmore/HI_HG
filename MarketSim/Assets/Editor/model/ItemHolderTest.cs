using Assets.src.model;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Test for the ItemHolder
/// </summary>
[TestFixture]
public class ItemHolderTest
{
    #region Fields

    /// <summary>
    /// Number of rows in basket
    /// </summary>
    private readonly int row = 4;

    /// <summary>
    /// Number of columns in basket
    /// </summary>
    private readonly int col = 2;

    /// <summary>
    /// The item h
    /// </summary>
    private ItemHolder itemH;

    #endregion Fields

    #region Methods

    /// <summary>
    /// Setups this instance.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        GameObject basket = new GameObject("basket");
        basket.AddComponent<Rigidbody>();
        basket.AddComponent<BoxCollider>();
        itemH = new ItemHolder(basket, col, row);
    }

    /// <summary>
    /// Tears down.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        itemH = null;
    }

    /// <summary>
    /// Tests the size of offsets.
    /// </summary>
    [Test]
    public void TestSizeOfOffsets()
    {
        itemH.InitOffsets();
        Assert.AreEqual(itemH.offsets.Length, col * row);
    }

    #endregion Methods
}