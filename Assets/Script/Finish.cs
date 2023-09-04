using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject startingSceneTransition;
    [SerializeField] private GameObject endingSceneTransition;

    private AudioSource finishSound;

    private bool levelCompleted = false;

    private void Start()
    {
        startingSceneTransition.SetActive(true);
        Invoke("DisableStartAnim", 5f);
    }

    void DisableStartAnim()
    {
        startingSceneTransition.SetActive(false);
    }

    void Update()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            finishSound.Play();
            levelCompleted = true;
            Invoke("EndingAnimation", 1f);
            Invoke("CompleteLevel", 2f);
        }
    }

    void EndingAnimation()
    {
        endingSceneTransition.SetActive(true);
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
