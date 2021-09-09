using UnityEngine;
using UnityEngine.Events;

public class SpawnTrigger : MonoBehaviour
{
    public SpawnPoint point;
    public bool restart = false;
    public UnityEvent restartPlatform, restartEntryPoint, restartTileBackground;

    void Awake()
    {
        if(restartPlatform == null)
            restartPlatform = new UnityEvent();

        if(restartEntryPoint == null)
            restartEntryPoint = new UnityEvent();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            AudioManager.sharedInstance.Play("death");
            
            if (restart)
            {
                restartPlatform.Invoke();
                restartEntryPoint.Invoke();
            }

            if(restartTileBackground != null)
                restartTileBackground.Invoke();

            point.SpawnToPosition();

        }
    }
}
