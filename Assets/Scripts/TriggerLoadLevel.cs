using UnityEngine;

public class TriggerLoadLevel : MonoBehaviour
{
    public LevelLoader loader;

    void Start()
    {
        if(loader == null)
            loader = gameObject.AddComponent<LevelLoader>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            loader.LoadNextLevel();
        }
    }
}
