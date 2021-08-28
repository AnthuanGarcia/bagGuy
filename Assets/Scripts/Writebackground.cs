using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Writebackground : MonoBehaviour
{
    public Transform fixedPoint;
    CinemachineVirtualCamera vCam;
    Transform player;
    GameObject[] blocks;
    GameObject cursorText;
    Text mainText, cursorTxt;
    string word = "Who  am?";
    const string passStr = "me";
    bool write = true;
    bool wait = true;
    int originalLen;
    int pressed = 0;
    KeyCode[] letters = new KeyCode[26];
    float time = 0;
    bool changeCursor;

    
    void Awake()
    {
        vCam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        vCam.Follow = fixedPoint;
    }

    void Start()
    {
        PlayerMovement.canMove = false;

        mainText = GetComponent<Text>();

        cursorText = GameObject.FindGameObjectWithTag("CursorTxt");
        cursorTxt = cursorText.GetComponent<Text>();

        blocks = GameObject.FindGameObjectsWithTag("AntiFall");
        player = GameObject.FindGameObjectWithTag("Player").transform;

        originalLen = mainText.text.Length;

        foreach(KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            byte[] c = Encoding.ASCII.GetBytes(key.ToString());
            byte asciiVal = c.Length > 1 ? (byte)0 : c[0];

            if(asciiVal >= 65 && asciiVal <= 90)
                letters[asciiVal - 65] = key;
        }
    }

    void Update()
    {
        if(write)
        {
            if(wait && pressed != 2)
            {
                foreach(KeyCode key in letters)
                {
                    if(Input.GetKeyDown(key))
                    {
                        mainText.text += key.ToString().ToLower();
                        pressed++;
                        player.position = new Vector2(player.position.x - 0.25f, player.position.y);
                        cursorTxt.text = cursorTxt.text.Insert(0, " ");
                        break;
                    }
                }
            }
        }
        else
        {
            StopCoroutine(delay());
            foreach(GameObject block in blocks) block.SetActive(false);
            gameObject.SetActive(false);
            cursorText.SetActive(false);
            vCam.Follow = player;
            PlayerMovement.canMove = true;
        }

        if(pressed == 2)
        {
            StartCoroutine(delay());
            pressed = 0;
        }

        if(mainText.text.Length >= originalLen + 2 && wait)
        {
            string pass = mainText.text.Substring(originalLen, 2);
            write = pass != passStr;
            mainText.text = word;
            player.position = new Vector2(player.position.x + 0.5f, player.position.y);
            cursorTxt.text = "|";
        }

        time += Time.deltaTime;

        if(time > 0.4f)
        {
            time = 0f;
            changeCursor = !changeCursor;
            cursorTxt.color = changeCursor ? Color.white : Color.black;
        }
    }

    private IEnumerator delay()
    {
        wait = false;
        yield return new WaitForSeconds(0.4f);
        wait = true;
    }
}
