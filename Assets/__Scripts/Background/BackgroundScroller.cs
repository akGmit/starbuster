using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for scrolling background functionality. 
/// </summary>

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1.0f;
    private Vector2 offset;
    private Material myMaterial;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, scrollSpeed); 
    }

    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
