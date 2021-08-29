using UnityEngine;
using Cinemachine;

public class TriggerCam : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();    
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            vCam.Priority = 8;
            //vCam.m_Lens.OrthographicSize = 1.5f;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            vCam.Priority = 11;
            //vCam.m_Lens.OrthographicSize = 1.5f;
        }
    }
}
