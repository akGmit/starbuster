using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundries : MonoBehaviour
{
    public Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth, objectHeight;

    void Start()
    {
        // find the screen bounds and convert to unity 
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
