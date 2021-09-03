using UnityEngine;
using UnityEngine.Events;

public class ReduceBackground : MonoBehaviour
{
    public float width = 6f;
    public float height = 4f;
    public bool reduce = false;
    public bool verifyTiles = false;

	[System.Serializable]
    public class ResizeBakgroundEvent : UnityEvent<float, float, bool> { }
    public ResizeBakgroundEvent OnResizeBackgroundEvent;

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(OnResizeBackgroundEvent != null && !verifyTiles)
                OnResizeBackgroundEvent.Invoke(width, height, reduce);
                
            else if(OnResizeBackgroundEvent != null && verifyTiles && ComposeBakcground.tilesBackground == 10)
                OnResizeBackgroundEvent.Invoke(width, height, reduce);
        }
    }
}
