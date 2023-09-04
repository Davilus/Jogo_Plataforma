using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private AudioSource MushroomSound;
    private enum MushState { idle, active }
    MushState state;

    private float bounce = 30f;
    private void Start()
    {
        anim = GetComponent<Animator>();
        state = MushState.idle;
        anim.SetInteger("state", (int)state);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.rotation.y == 0)
            {
                MushroomSound.Play();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
                Active();
                Invoke("Idle", 0.7f);
            }
        }
    }

    private void Active()
    {
        state = MushState.active;
        anim.SetInteger("state", (int)state);
    }

    private void Idle()
    {
        state = MushState.idle;
        anim.SetInteger("state", (int)state);
    }
}
