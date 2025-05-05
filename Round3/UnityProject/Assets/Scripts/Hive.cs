using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    public float forceAmount = 10f; // Adjust this value based on the desired initial speed

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Generate a random direction
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // Apply the force
        rb.AddForce(randomDirection * forceAmount, ForceMode2D.Impulse);

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Keeper"))
        {
            Destroy(gameObject);
        }
    }

}
