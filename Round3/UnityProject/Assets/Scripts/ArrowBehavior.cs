using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    public float speed = 5.0f;
    public float arrowDamage = 20f;
    private Vector3 targetPosition;
    private Vector3 direction;

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        direction = (targetPosition - transform.position).normalized;
        UpdateRotation();
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        // Optionally, you can destroy the arrow if it goes too far
        if (Vector3.Distance(transform.position, targetPosition) > 10f) // 10 is an example max distance
        {
            Destroy(gameObject);
        }
    }

    private void UpdateRotation()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - 90);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Keeper"))
        {
            // Apply damage to beekeeper
            HealthBar healthBar = collider.gameObject.GetComponentInChildren<HealthBar>();
            if (healthBar != null)
            {
                //gameObject.GetComponentInChildren<Animator>().SetTrigger("Hurt");
                KeeperOffline keeper;
                keeper = collider.gameObject.GetComponent<KeeperOffline>();
                healthBar.TakeDamage(arrowDamage);
                keeper.updateHealth(healthBar.currentHealth);


            }

            // Destroy the arrow
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Boundary"))
        {
            // Destroy the arrow if it hits a boundary
            Destroy(gameObject);
        }
    }

}
