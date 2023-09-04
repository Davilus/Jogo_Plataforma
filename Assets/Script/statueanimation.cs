using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statueanimation : MonoBehaviour
{
    private Animator anim;
    private bool ativo = false;
    private enum CPState { idle, activation, active}
    CPState state;

    void Start()
    {
        state = CPState.idle;
        anim = GetComponent<Animator>();
        anim.SetInteger("state", (int)state);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CPState state;
        if (collision.gameObject.name == "Player")
        {
            state = CPState.activation;
            anim.SetInteger("state", (int)state);
            ativo = true;
        }
        if (ativo)
        {
            Invoke("Activated", 0.6f);
        }
    }

    private void Activated()
    {
        state = CPState.active;
        anim.SetInteger("state", (int)state);
    }
}
