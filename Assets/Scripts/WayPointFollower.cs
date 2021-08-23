using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool cycle = false;
    [SerializeField] private bool playerIsNeed = false;
    int currentWayPointIdx = 0;
    bool move = false;

    void Start()
    {
        currentWayPointIdx = cycle ? 1 : 0;
    }

    void FixedUpdate()
    {
        if(!playerIsNeed)
        {
            if(Vector2.Distance(
                wayPoints[currentWayPointIdx].position,
                transform.position) < .1f)
            {
                currentWayPointIdx++;

                if(currentWayPointIdx > wayPoints.Length - 1)
                {
                    if(cycle) 
                    {
                        transform.position = wayPoints[0].position;
                        currentWayPointIdx = 1;
                    }
                    else
                        currentWayPointIdx = 0;
                }
            }

            transform.position = Vector2.MoveTowards(
                transform.position,
                wayPoints[currentWayPointIdx].position,
                Time.fixedDeltaTime * speed
            );
        } 
        else if (move)
        {
            
            transform.position = Vector2.MoveTowards(
                transform.position,
                wayPoints[currentWayPointIdx].position,
                Time.fixedDeltaTime * speed
            );

            if(Vector2.Distance(
                wayPoints[currentWayPointIdx].position,
                transform.position) < .1f)
            {
                currentWayPointIdx++;

                if(currentWayPointIdx > wayPoints.Length - 1)
                    move = false;
            }
        }
    }

    public void StartPath()
    {
        move = true;
    }

    public void ResetPath()
    {
        transform.position = wayPoints[0].position;
        currentWayPointIdx = 0;
        move = false;
    }
}
