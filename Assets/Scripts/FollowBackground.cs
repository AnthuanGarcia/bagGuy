using Cinemachine;
using UnityEngine;

public class FollowBackground : MonoBehaviour
{
    CinemachineVirtualCamera vCam;

    void Awake()
    {
        vCam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        transform.SetParent(vCam.transform);
    }

    /*void FixedUpdate()
    {
        transform.position = new Vector2(
            vCam.transform.position.x,
            vCam.transform.position.y
        );
    }*/
}
