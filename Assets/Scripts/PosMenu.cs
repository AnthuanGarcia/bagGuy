using UnityEngine;
using UnityEngine.SceneManagement;

public class PosMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuUi;
    public string themeToStop;
    public bool restartTiles = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void Paused()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.sharedInstance.Stop(themeToStop);
        if(restartTiles) ComposeBakcground.tilesBackground = 0;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
