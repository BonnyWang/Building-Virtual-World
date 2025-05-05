using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHurt : MonoBehaviour
{
    private SpriteRenderer sp;
    public float hurtLength;
    private float hurtCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
