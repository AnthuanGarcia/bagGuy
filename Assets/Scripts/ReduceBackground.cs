using UnityEngine;
using UnityEngine.Events;

public class ReduceBackground : MonoBehaviour
{
    public UnityEvent OnResizeBackgroundEvent;

    void Awake()
    {
        if(OnResizeBackgroundEvent == null)
            OnResizeBackgroundEvent = new UnityEvent();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            OnResizeBackgroundEvent.Invoke();
        }
    }
}
