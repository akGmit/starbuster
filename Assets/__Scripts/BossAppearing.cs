using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppearing : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, transform.rotation.z + 3); 
       
    }
}
