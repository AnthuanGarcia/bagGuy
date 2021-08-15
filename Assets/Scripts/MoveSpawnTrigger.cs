using UnityEngine;

public class MoveSpawnTrigger : MonoBehaviour
{
    public Transform spawnPoint;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
            spawnPoint.position = transform.position;
    }
}
