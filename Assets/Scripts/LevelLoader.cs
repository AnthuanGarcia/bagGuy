using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    public float timeTransition = 2f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int idx)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(timeTransition);

        SceneManager.LoadScene(idx);
    }
}
