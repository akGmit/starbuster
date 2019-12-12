using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,
                            new Vector3(0.5f, 0.5f));
    }

}

