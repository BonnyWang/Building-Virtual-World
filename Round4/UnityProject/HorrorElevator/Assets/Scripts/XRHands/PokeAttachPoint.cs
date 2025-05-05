using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PokeAttachPoint : MonoBehaviour
{
    [SerializeField] private Transform pokeAttachPoint;

    private XRPokeInteractor pokeInteractor;

    // Start is called before the first frame update
    private void Start()
    {
        pokeInteractor = transform.parent.parent.GetComponentInChildren<XRPokeInteractor>();
        SetPokeAttachPoint();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void SetPokeAttachPoint()
    {
        if (pokeAttachPoint == null) { Debug.LogWarning("Attach Point is null"); return; }
        if (pokeInteractor == null) { Debug.LogWarning("Poke Interactor is null"); return; }
        pokeInteractor.attachTransform = pokeAttachPoint;
    }
}
