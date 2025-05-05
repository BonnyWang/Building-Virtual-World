using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public float attackDamage = 20f;
    Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Bee"))
        {
            Debug.Log("Attack Succeed!");
            SoundManager.instance.IncreaseBgmStage();
            SoundManager.instance.PlayDieSound();
            HealthBar healthBar = collider.gameObject.GetComponentInChildren<HealthBar>();
            if (healthBar != null)
            {
                healthBar.TakeDamage(attackDamage);
                collider.gameObject.GetComponent<BeeMovement>().updateHealth(healthBar.currentHealth - attackDamage);
            }

            //Vector2 difference = collider.transform.position - transform.position;
            //collider.transform.position = new Vector2(collider.transform.position.x + difference.x, collider.transform.position.y + difference.y);

        }
    }
}
