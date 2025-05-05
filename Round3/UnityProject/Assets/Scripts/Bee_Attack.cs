using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee_Attack : MonoBehaviour
{
    public Animator animator;
    public float damageAmount = 20f;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Keeper"))
        {
            Debug.Log("Beekeeper gets Attacked!");
            animator.SetTrigger("Attack");
            
            // When the bee attacks the beekeeper
            HealthBar healthBar = collider.gameObject.GetComponentInChildren<HealthBar>();
            if (healthBar != null)
            {
                Keeper keeper;
                keeper = collider.gameObject.GetComponent<Keeper>();
                healthBar.TakeDamage(damageAmount);
                keeper.updateHealth(healthBar.currentHealth);
            }
        }

        if (collider.gameObject.CompareTag("Net"))
        {
            Debug.Log("Bee gets Caught!");
        }
    }

}
