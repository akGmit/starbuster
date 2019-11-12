using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // == fields ==
    [SerializeField]    // adds this field to the Unity editor
    private float playerSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()    // initialisation
    {
    }

    // == private methods ==
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // need to ensure that deltaX is frame rate independent
        // use Time.deltaTime as a multiplier
        // also add a speed multiplier for the player to get a good feel
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        
        // add the delta to the current position
        var newXPos = transform.position.x + deltaX;
        
        // update the current position
        transform.position = new Vector2(newXPos, transform.position.y);
    }


}
