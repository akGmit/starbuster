using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Script controlling player movement boundries.
 * Adoptped from Labs.
 */
public class Boundries : MonoBehaviour
{
    public Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth, objectHeight;

    void Start()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(
                            new Vector3(Screen.width,
                                        Screen.height,
                                        mainCamera.transform.position.z));

        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x,
                                (screenBounds.x * -1) + objectWidth,
                                screenBounds.x - objectWidth);
        transform.position = viewPos;
    }
}
