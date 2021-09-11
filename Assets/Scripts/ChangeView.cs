using UnityEngine;
using Cinemachine;

public class ChangeView : MonoBehaviour
{
    public CinemachineVirtualCamera exitView;

    void Update()
    {
        if(Input.anyKeyDown)
        {
            exitView.Priority = 8;
            gameObject.SetActive(false);
        }
    }
}
