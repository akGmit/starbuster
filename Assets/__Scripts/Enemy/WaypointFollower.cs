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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetNextTarget();
    }

    private void GetNextTarget()
    {
        if (HasMorePoints())
        {
            currentTarget = waypoints.First();
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
        rb.position = Vector2.MoveTowards(rb.position, currentTarget, speed * Time.deltaTime);
 
        if (Vector2.Distance(rb.position, currentTarget) < 0.01)
        {
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

