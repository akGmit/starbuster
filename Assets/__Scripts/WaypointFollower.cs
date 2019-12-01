using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public class WaypointFollower : MonoBehaviour
{
    // == fields ==
    private float speed;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private IList<Vector3> waypoints = new List<Vector3>();
    private Vector3 currentTarget;
    private Rigidbody2D rb;

    // == public method ==
    public void AddPointToFollow(Vector3 point)
    {
        waypoints.Add(point);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // set the current target to the next point on the list
        GetNextTarget();
    }

    private void GetNextTarget()
    {
        if (HasMorePoints())
        {
            currentTarget = waypoints.First();// using system.Linq;
            
        }
    }

    private bool HasMorePoints()
    {
        return waypoints.Count != 0;
    }

    private void FixedUpdate()
    {
        if (HasMorePoints())
        {
            MoveToNextPoint();
        }
    }

    private void MoveToNextPoint()
    {
        // set the rb moving towards the target point.
        rb.position = Vector2.MoveTowards(rb.position,
                                          currentTarget,
                                          speed);
        // have to figure when the rb arrives at target
       
        if (Vector2.Distance(rb.position, currentTarget) < 0.1)
        {
            // update rb.position to the target
            rb.position = new Vector2(currentTarget.x, currentTarget.y);
            waypoints.Remove(currentTarget);
            
           
            if (HasMorePoints())
            {
                GetNextTarget();
            }
            else
            { 
                    Destroy(gameObject);
            }          
        }
    }
}

