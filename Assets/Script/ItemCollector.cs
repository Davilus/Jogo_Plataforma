using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    private int gg = 0;
    [SerializeField] private AudioSource collectionSoundEffect;
    [SerializeField] private FloatSO ggSO;
    [SerializeField] private FloatSO ggSONotUsed1;
    [SerializeField] private FloatSO ggSONotUsed2;
    [SerializeField] private GameObject GG1;
    [SerializeField] private GameObject GG2;
    [SerializeField] private GameObject GG3;
    [SerializeField] private GameObject GG4;
    [SerializeField] private GameObject GG5;
    [SerializeField] private GameObject GG6;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Green Grape"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            gg++;
            ggSO.Value++;
            GG1.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Green Grape 2"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            gg++;
            ggSO.Value++;
            GG2.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Green Grape 3"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            gg++;
            ggSO.Value++;
            GG3.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Green Grape 4"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            gg++;
            ggSO.Value++;
            GG4.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Green Grape 5"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            gg++;
            ggSO.Value++;
            GG5.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Green Grape 6"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            gg++;
            ggSO.Value++;
            GG6.SetActive(true);
        }
    }


}
