using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject endingSceneTransition;
    [SerializeField] private GameObject cafe;
    [SerializeField] private GameObject player;

    public void PlayGame()
    {
        Invoke("CafeAnimation", .5f);
        Invoke("PlayerAnimation", .8f);
        Invoke("EndingAnimation", 5f);
        Invoke("LoadScene", 6f);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void EndingAnimation()
    {
        endingSceneTransition.SetActive(true);
    }
    void CafeAnimation()
    {
        cafe.SetActive(true);
    }
    void PlayerAnimation()
    {
        player.SetActive(true);
    }
}
