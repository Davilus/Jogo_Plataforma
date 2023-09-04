using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{

    public void GoToMenu()
    {
        Invoke("LoadMenuu", 1f);
    }

    public void LoadMenuu()
    {
        SceneManager.LoadScene("Start Screen");
    }
}
