using UnityEngine;
using UnityEngine.Events;

public class StickyPlatform : MonoBehaviour
{
    [SerializeField] private bool entryPoint = false;
    public UnityEvent start;

    void Awake()
    {
        if(start == null)
            start = new UnityEvent();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            col.gameObject.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            col.gameObject.transform.SetParent(null);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player") && entryPoint)
        {
            start.Invoke();
            entryPoint = false;
        }
    }

    public void restartEntryPoint()
    {
        entryPoint = true;
    }
}
