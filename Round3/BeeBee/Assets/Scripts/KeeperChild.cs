using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperChild : MonoBehaviour
{
    public Animator animator;
    public float rollDistanceCoefficient = 2f;

    [Header("Hurt")]
    private SpriteRenderer sp;
    public float hurtLength;
    private float hurtCounter;

    void Update()
    {
        if (hurtCounter > 0)
        {
            hurtCounter -= Time.deltaTime;
            if (hurtCounter <= 0)
            {
                // Reset the shader effect
                SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer sp in spriteRenderers)
                {
                    sp.material.SetFloat("_FlashAmount", 0);
                }
            }
        }
    }

    public void CallParentResetRolling()
    {
        Debug.Log("CallParentResetRolling called");
        transform.parent.GetComponent<KeeperOffline>().ResetRolling();

    }
    public void CallParentResetAttack()
    {
        Debug.Log("CallParentResetAttack called");
        transform.parent.GetComponent<KeeperOffline>().ResetAttack();

    }
    void OnAnimatorMove()
    {
        if (animator.applyRootMotion && transform.parent.GetComponent<KeeperOffline>().isRolling)
        {
            // Calculate the desired velocity from root motion
            Vector2 rootMotionVelocity = animator.deltaPosition * rollDistanceCoefficient / Time.deltaTime;

            // Apply this velocity to the Rigidbody2D
            Rigidbody2D rb = transform.parent.GetComponent<Rigidbody2D>();
            rb.velocity = rootMotionVelocity;
        }
    }
    public void HurtShader()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        // Apply the shader effect to each SpriteRenderer
        foreach (SpriteRenderer sp in spriteRenderers)
        {
            sp.material.SetFloat("_FlashAmount", 1);
        }
        hurtCounter = hurtLength;
    }
}
