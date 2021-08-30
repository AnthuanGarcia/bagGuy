using Cinemachine;
using UnityEngine;

public class FollowBackground : MonoBehaviour
{
    public bool playerPosition;
    CinemachineVirtualCamera vCam;
    //Transform[] positions;
    void Awake()
    {
        vCam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        
        if(playerPosition)
            transform.position = vCam.transform.position;

        //positions = GetComponentsInChildren<Transform>();

        //transform.SetParent(vCam.transform);
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(
            vCam.transform.position.x,
            vCam.transform.position.y
        );
    }

}
