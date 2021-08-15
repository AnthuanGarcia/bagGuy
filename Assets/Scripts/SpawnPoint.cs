using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    GameObject prefabToSpawn;

    void Awake()
    {
        prefabToSpawn = GameObject.FindGameObjectWithTag("Player");
    }

    public void SpawnToPosition()
    {
        if(prefabToSpawn != null)
            prefabToSpawn.transform.position = transform.position; 

    }
}
