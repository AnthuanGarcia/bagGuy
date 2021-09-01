using UnityEngine;
using Cinemachine;

public class TriggerCam : MonoBehaviour
{
    public CinemachineVirtualCamera newView;
    public bool followPlayer = false;
    public bool deactivateMainCamera = false;
    public bool resetCamConfig = false;

    Transform playerPos;
    BackScreenSize mainBack;
    CinemachineVirtualCamera mainVCam;
    //private CinemachineVirtualCamera main_vCam;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        mainVCam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();

        if(deactivateMainCamera)
            mainBack = GameObject.FindGameObjectWithTag("PartBackground").GetComponent<BackScreenSize>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {                
            newView.Priority = 12;
            newView.Follow = followPlayer ? playerPos : newView.Follow;

            if(resetCamConfig)
            {
                mainBack.follow = newView.transform;
                return;
            }

            if(deactivateMainCamera)
                mainBack.follow = playerPos;
            //vCam.m_Lens.OrthographicSize = 1.5f;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            newView.Priority = 8;
            if(playerPos != null) newView.Follow = null;

            if(deactivateMainCamera)
                mainBack.follow = mainVCam.transform;
            //vCam.m_Lens.OrthographicSize = 1.5f;
        }
    }
}
