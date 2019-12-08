using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    [SerializeField]
    private Boss bossPrefab;
    
    public void SpawnBoss() => Instantiate(bossPrefab, transform);
    

    void Start()
    {
        
    }
}
