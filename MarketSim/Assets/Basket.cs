﻿using UnityEngine;
using System.Collections;

public class Basket : MonoBehaviour
{
    #region Fields

    protected GameObject basket;

    protected GameObject handle;

    protected GameObject handleModel;

    protected GameObject basketModel;

    #endregion Fields

    #region Methods
    // Use this for initialization
    void Start()
    {
        ///this.handle = Resources.Load<GameObject>("single_basket_handle");
        //this.basket = Resources.Load<GameObject>("single_basket_no_handle");
        this.basketModel = GameObject.Find("single_basket_no_handle");
        this.handleModel = GameObject.Find("single_basket_handle");

        basketModel.transform.position = handleModel.transform.position;
    }

    /// <summary>
    /// Update position.
    /// </summary>
    void Update()
    {
      
        basketModel.GetComponent<Collider>().enabled = false;
        Vector3 newPos = basketModel.transform.position;
        float height = handleModel.GetComponent<BoxCollider>().bounds.size.y;
        newPos.y += height;
        handleModel.transform.position = newPos;
        
    }
    #endregion Methods 
    
}
