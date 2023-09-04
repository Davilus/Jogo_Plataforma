using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    private Vector3 respawnPoint;
    [SerializeField] private float respawnTimer;
    [SerializeField] private float maxSpeed;
    private bool ativo = false;
    [SerializeField] private BoxCollider2D boxColliderMHurtbox;
    [SerializeField] private BoxCollider2D boxColliderMID;
    [SerializeField] private BoxCollider2D boxColliderGHurtbox;
    [SerializeField] private BoxCollider2D boxColliderGID;

    [SerializeField] private Rigidbody2D rb;

    private Animator anim;
    private enum SpikeState { idle, breaking, falling }
    SpikeState state;

    private void Start()
    {
        state = SpikeState.idle;
        anim = GetComponent<Animator>();
        respawnPoint = transform.position;
        anim.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ativo)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Fall();
                ativo = true;
            }
        }
    }

    private void Update()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = (Vector2)Vector3.ClampMagnitude((Vector3)rb.velocity, maxSpeed);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Trap"))
        {
            boxColliderMHurtbox.enabled = false;
            boxColliderGHurtbox.enabled = false;
            state = SpikeState.breaking;
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetInteger("state", (int)state);
            Invoke("Respawn", respawnTimer);
            ativo = false;
        }
    }
    private void Fall()
    {
        boxColliderMID.enabled = false;
        boxColliderGID.enabled = false;
        state = SpikeState.falling;
        anim.SetInteger("state", (int)state);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Respawn()
    {
        boxColliderMHurtbox.enabled = true;
        boxColliderMID.enabled = true;
        boxColliderGHurtbox.enabled = true;
        boxColliderGID.enabled = true;
        state = SpikeState.idle;
        transform.position = respawnPoint;
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetInteger("state", (int)state);
    }
}
