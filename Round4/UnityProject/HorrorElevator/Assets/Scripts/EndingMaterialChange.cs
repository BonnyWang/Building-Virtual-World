using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingMaterialChange : MonoBehaviour
{
    [SerializeField] private GameObject endingPlane;
    [SerializeField] Material goodLuck;
    [SerializeField] Material credit;

    [SerializeField] bool test;

    private Material endingMaterial;
    // Start is called before the first frame update
    void Start()
    {
        endingPlane.GetComponent<MeshRenderer>().material = credit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MaterialSetup()
    {
        if (GameManager.Instance.success != true || !test)
        {
            endingPlane.GetComponent<MeshRenderer>().material = goodLuck;
        }

        else
        {
            endingPlane.GetComponent<MeshRenderer>().material = credit;
        }
    }
}
