using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private Stack<SpawnPoint> spawnPointsStack;
    private IList<SpawnPoint> spawnPointsList;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnPointsList = GameObject.Find("Spawners").GetComponentsInChildren<SpawnPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
