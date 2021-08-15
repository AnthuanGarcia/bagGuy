using UnityEngine;
using Cinemachine;

public class TriggerZoom : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    bool twice = true;

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(twice)
            {
                cam.Priority = 9;
                twice = !twice;
            }
            else
            {
                cam.Priority = 11;
                twice = !twice;
            }
        }
    }
}
