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
    protected GameObject basket;
    /// <summary>
    /// The handle
    /// </summary>
    protected GameObject handle;
    /// <summary>
    /// The handle model
    /// </summary>
    protected GameObject handleModel;
    /// <summary>
    /// The basket model
    /// </summary>
    protected GameObject basketModel;

    #endregion Fields

    #region Methods
    // Use this for initialization
    /// <summary>
    /// Starts this instance
    /// </summary>
    void Start()
    {
        this.basketModel = GameObject.Find("single_basket_no_handle");
        this.handleModel = GameObject.Find("single_basket_handle");

    }

    /// <summary>
    /// Update position of basket and handle
    /// </summary>
    void Update()
    {
        Vector3 newPos = basketModel.transform.position;
        float height = handleModel.GetComponent<MeshCollider>().bounds.size.y;
        newPos.y += height;
        handleModel.transform.position = newPos;
        
    }
    #endregion Methods 
    
}
