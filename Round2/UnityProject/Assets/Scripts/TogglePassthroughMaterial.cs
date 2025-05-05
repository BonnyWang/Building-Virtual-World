using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TogglePassthroughMaterial : MonoBehaviour
{
    private Renderer objectRenderer;
    private bool isInverted = false;

    private void Start() {
        objectRenderer = GetComponent<Renderer>();
    }
    public void TogglePassthrough() {
        isInverted = !isInverted;
        UpdateMaterial();
    }
    void UpdateMaterial() {
        if (isInverted) {
            objectRenderer.material.SetFloat("_InvertedAlpha", 1);
        } else {
            objectRenderer.material.SetFloat("_InvertedAlpha", 0);
        }
    }
    
}
