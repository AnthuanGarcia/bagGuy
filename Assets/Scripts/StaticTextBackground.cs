using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class StaticTextBackground : MonoBehaviour
{
    public Transform m_FixedPoint;
    public CinemachineVirtualCamera m_InitCam;
    public string m_Word = "Who  am?";
    public float m_Time = 1.0f;


    CinemachineVirtualCamera mainCam;
    Transform player;
    GameObject[] blocks;
    Text mainText;
    float elapsedTime = 0f;
    bool once = true;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.canMove = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        mainCam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();

        mainText = GetComponent<Text>();
        mainText.text = m_Word;

        blocks = GameObject.FindGameObjectsWithTag("AntiFall");
    }

    // Update is called once per frame
    void Update()
    {
        if(elapsedTime > m_Time && once)
        {
            foreach(GameObject block in blocks) block.SetActive(false);
            m_InitCam.Priority = 8;
            PlayerMovement.canMove = true;
            once = false;
            gameObject.SetActive(false);
            mainCam.Follow = player;
        }

        elapsedTime += Time.deltaTime;
    }
}
