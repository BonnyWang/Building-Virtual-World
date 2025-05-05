using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    [SerializeField] float magnitude = 1f;
    [SerializeField] bool triggerBySpace = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerBySpace){
            if(Input.GetKeyDown(KeyCode.Space)){
                StartCoroutine(Shake(duration, magnitude));
            }
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f,1f) * magnitude;
            transform.position += new Vector3(x,y,0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
    }
}
