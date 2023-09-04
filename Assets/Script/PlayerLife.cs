using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 respawnPoint;

    private int deaths = 0;
    [SerializeField] private TextMeshProUGUI deathText;

    [SerializeField] private FloatSO deathSO;
    [SerializeField] private FloatSO deathSONotUsed1;
    [SerializeField] private FloatSO deathSONotUsed2;

    public bool morto = false;

    private bool imortal = false;

    [SerializeField] private AudioSource deathSoundEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!imortal)
        {
            if (collision.gameObject.CompareTag("Trap") && !morto)
            {
                Die();
                Invoke("Respawn", 1f);
                deaths++;
                deathText.text = "x " + deaths;
                deathSO.Value++;
            }
        }
    }

    private void Update()
    {
        //TIRAR QUANDO LANÇAR O JOGO
        if (Input.GetKeyDown(KeyCode.K))
        {
            imortal = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            imortal = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            respawnPoint = transform.position;
        }
    }

    public void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        morto = true;
    }

    private void Respawn()
    {
        transform.position = respawnPoint;
        rb.bodyType = RigidbodyType2D.Dynamic;
        morto = false;
    }
}
