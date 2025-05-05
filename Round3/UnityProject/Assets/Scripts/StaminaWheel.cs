using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaWheel : MonoBehaviour
{
    public float stamina;
    public float maxStamina;
    public Slider staminaWheel;
    public Slider usageWheel;
    public CanvasGroup staminaWheelCanvasGroup; // Add a CanvasGroup component to the stamina wheel in the Editor

    public float staminaRegen = 20f;
    public float hideDelay = 2f; // Time after which the stamina wheel is hidden when full
    private float hideTimer;

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
        staminaWheelCanvasGroup.alpha = 0; // Make the stamina wheel invisible
        UpdateStaminaDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        // Regenerate stamina over time
        if (stamina < maxStamina)
        {
            stamina += staminaRegen * Time.deltaTime;
            UpdateStaminaDisplay();
        }

        // Hide the stamina wheel if full and timer elapsed
        if (stamina >= maxStamina)
        {
            hideTimer += Time.deltaTime;
            if (hideTimer >= hideDelay)
            {
                staminaWheelCanvasGroup.alpha = 0; // Make the stamina wheel invisible
            }
        }
        else
        {
            hideTimer = 0; // Reset timer if stamina is not full
            staminaWheelCanvasGroup.alpha = 1; // Make the stamina wheel visible
        }

    }
    public void UseStamina(float amount)
    {
        if (stamina >= amount)
        {
            stamina -= amount;
            UpdateStaminaDisplay();
        }
    }
    private void UpdateStaminaDisplay()
    {
        staminaWheel.value = stamina / maxStamina;
        usageWheel.value = stamina / maxStamina + 0.1f;
    }

}
