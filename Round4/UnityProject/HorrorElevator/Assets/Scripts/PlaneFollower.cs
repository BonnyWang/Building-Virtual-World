using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaneFollower : MonoBehaviour
{
    public Transform target;
    [SerializeField] Material creditMaterial;
    void Start()
    {
        StartCoroutine(changeMaterial());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, 0.01f);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.parent.rotation *Quaternion.Euler(90, -90,90), 0.01f);
    }

    IEnumerator changeMaterial(){
        yield return new WaitForSeconds(5f);
        GetComponent<MeshRenderer>().material = creditMaterial;
    }
}
