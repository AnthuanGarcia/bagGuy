using UnityEngine;
using Cinemachine;

public class TriggerCam : MonoBehaviour
{
    public CinemachineVirtualCamera newView;
    //private CinemachineVirtualCamera main_vCam;

    // Start is called before the first frame update
    /*void Start()
    {
        main_vCam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();    
    }*/

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            newView.Priority = 12;
            //vCam.m_Lens.OrthographicSize = 1.5f;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            newView.Priority = 8;
            //vCam.m_Lens.OrthographicSize = 1.5f;
        }
    }
}
