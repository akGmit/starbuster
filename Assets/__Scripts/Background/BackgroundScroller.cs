using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * use a quad to set up a scrolling background
 * see unity documentation https://docs.unity3d.com/Manual/PrimitiveObjects.html?_ga=2.18934193.804407618.1572951697-41484169.1568376042
 * 
 * to scroll,
 * need scroll speed, material, quad
 */


public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1.0f;
    private Vector2 offset; // use in update
    private Material myMaterial;

    void Start()
    {
        // get the material and set the offset value
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, scrollSpeed); 
    }

    void Update()
    {
        // set the x offset
        myMaterial.mainTextureOffset += 
                            offset * Time.deltaTime;
    }
}
