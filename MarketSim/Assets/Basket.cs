using UnityEngine;
using System.Collections;

/// <summary>
/// Translate the handle to the basket
/// </summary>
/// <seealso cref="UnityEngine.MonoBehaviour" />
public class Basket : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// The basket
    /// </summary>
    private GameObject basket;

    /// <summary>
    /// The handle
    /// </summary>
    private GameObject handle;

    /// <summary>
    /// The handle model
    /// </summary>
    private GameObject handleModel;

    /// <summary>
    /// The basket model
    /// </summary>
    private GameObject basketModel;

    #endregion Fields

    #region Methods

    // Use this for initialization
    /// <summary>
    /// Starts this instance
    /// </summary>
    public void Start()
    {
        this.basketModel = GameObject.Find("single_basket_no_handle");
        this.handleModel = GameObject.Find("single_basket_handle");

    }

    /// <summary>
    /// Update position of basket and handle
    /// </summary>
    public void Update()
    {
        Vector3 newPos = basketModel.transform.position;
        float height = this.handleModel.GetComponent<MeshCollider>().bounds.size.y;
        newPos.y += height;
        this.handleModel.transform.position = newPos;
        
    }
    #endregion Methods 
    
}
