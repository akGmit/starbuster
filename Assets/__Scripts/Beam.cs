using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private float timeActive = 5f;
    private float timeActivated;
    // Start is called before the first frame update
    void Start()
    {
        timeActivated = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (Time.time - timeActivated > timeActive)
        {
            Destroy(gameObject);
        }
    }
}
