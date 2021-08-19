using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool cycle = false;
    int currentWayPointIdx = 0;

    void FixedUpdate()
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
}
