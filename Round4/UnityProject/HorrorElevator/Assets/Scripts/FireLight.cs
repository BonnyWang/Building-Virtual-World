using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    [SerializeField] private GameObject lightObj;
    [SerializeField] private float fireRate = 16f;
    
    private Light fireLight;
    
    // Start is called before the first frame update
    void Start()
    {
        fireLight = lightObj.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        fireLight.intensity = Time.smoothDeltaTime * fireRate * Random.Range(8f, 10f);
    }


}
