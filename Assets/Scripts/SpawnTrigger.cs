using UnityEngine;
using UnityEngine.Events;

public class SpawnTrigger : MonoBehaviour
{
    public SpawnPoint point;
    public bool restart = false;
    public UnityEvent restartPlatform, restartEntryPoint;

    void Awake()
    {
        if(restartPlatform == null)
            restartPlatform = new UnityEvent();

        if(restartEntryPoint == null)
            restartEntryPoint = new UnityEvent();
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            if(restart)
            {
                restartPlatform.Invoke();
                restartEntryPoint.Invoke();
            }

            point.SpawnToPosition();
        }
    }
}
