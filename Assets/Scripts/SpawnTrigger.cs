using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public SpawnPoint point;
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
            point.SpawnToPosition();
    }
}
