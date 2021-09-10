using UnityEngine;
using Cinemachine;

public class ChangeView : MonoBehaviour
{
    public CinemachineVirtualCamera exitView;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            exitView.Priority = 8;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
