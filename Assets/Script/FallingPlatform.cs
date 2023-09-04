using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    private Vector3 respawnPoint;
    [SerializeField] private float maxSpeed;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        respawnPoint = transform.position;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Fall", 1f);
            Invoke("Respawn", 2.5f);
        }
    }

    private void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }



    private void Respawn()
    {
        transform.position = respawnPoint;
        rb.bodyType = RigidbodyType2D.Static;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }


}
