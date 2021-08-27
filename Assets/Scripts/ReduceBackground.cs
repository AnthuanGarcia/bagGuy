using UnityEngine;
using UnityEngine.Events;

public class ReduceBackground : MonoBehaviour
{
    public float width = 6f;
    public float height = 4f;
    public bool reduce = false;

	[System.Serializable]
    public class ResizeBakgroundEvent : UnityEvent<float, float, bool> { }
    public ResizeBakgroundEvent OnResizeBackgroundEvent;

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(OnResizeBackgroundEvent != null)
                OnResizeBackgroundEvent.Invoke(width, height, reduce);
        }
    }
}
