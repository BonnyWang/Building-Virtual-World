using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SphereNearByDetect : MonoBehaviour
{
    [SerializeField] GameObject visualizer;
    [SerializeField] bool start;
    bool trigger;
    bool destroyed;
    // void Start()
    // {
    //     if(start){
    //         destroyNearBy();
    //     }
    //     trigger = false;
    // }

    void Update()
    {
        if(start && (!trigger)){
            trigger = true;
            destroyNearBy(transform.position, 0.2f);
        }
    }

    public void destroyNearBy(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            SphereNearByDetect nearbyBall = hitCollider.gameObject.GetComponent<SphereNearByDetect>();
            if((nearbyBall != null) && (nearbyBall.GetComponent<Bubble>() != null)){
                if((nearbyBall.GetComponent<Bubble>().color == GetComponent<Bubble>().color)&&(!nearbyBall.destroyed)){
                    StartCoroutine(lateDestroy(hitCollider.gameObject));
                }
            }
        }

        destroyed = true;
        Bubble.playPopSound();
        
        StartCoroutine(lateDestroy());
    }
    

    // public void destroyNearBy(){
    //     float[][] randomSphere = get_random_sphere(128);
    //     for(int i = 0; i < randomSphere[0].Length; i++){
            
    //         Vector3 pointPosition =  new Vector3(randomSphere[0][i], randomSphere[1][i], randomSphere[2][i]);
            
    //         Debug.DrawRay(pointPosition*0.1f + transform.position, pointPosition*0.2f, Color.red, 0.1f);
    //         Physics.Raycast(pointPosition*0.2f + transform.position, pointPosition, out RaycastHit hit, 0.1f);
            
    //         if(hit.collider != null){
    //            SphereNearByDetect nearbyBall = hit.collider.gameObject.GetComponent<SphereNearByDetect>();
    //             if((nearbyBall != null) && (nearbyBall.GetComponent<Bubble>() != null)){
    //                 if((nearbyBall.GetComponent<Bubble>().color == GetComponent<Bubble>().color)&&(!nearbyBall.destroyed)){
    //                     StartCoroutine(lateDestroy(hit.collider.gameObject));
    //                 }
    //             }

    //         }

    //     }


    //     // No matter if find nearby, destroy self

    //     destroyed = true;
    //     Bubble.playPopSound();
        
    //     StartCoroutine(lateDestroy());
    //     // }

    // }


    IEnumerator lateDestroy(GameObject hitObject = null){
        
        yield return new WaitForSeconds(0.15f);
        if(hitObject !=null){
            SphereNearByDetect nearbyBall = hitObject.GetComponent<SphereNearByDetect>();
            if(nearbyBall != null){
                nearbyBall.destroyNearBy(hitObject.transform.position, 0.2f);
            }
        }
        yield return new WaitForSeconds(0.15f);
        Destroy(transform.parent.gameObject);
    }


    float[][] get_random_sphere(int sample_num = 32){
        float[] z = generateRandomArray(sample_num, -1f, 1f);
        float[] theta = generateRandomArray(sample_num, 0, 2*Mathf.PI);
        
        float[] x = multiplyArray(sqrtArray(minusArray(1, power2Array(z))), cosineArray(theta));
        float[] y = multiplyArray(sqrtArray(minusArray(1, power2Array(z))), sineArray(theta));
        return new float[][]{x, y, z};
    }

    float[] multiplyArray(float[] input1, float[] input2){
        float[] output = new float[input1.Length];
        for (int i = 0; i < input1.Length; i++)
        {
            output[i] = input1[i] * input2[i];
        }
        return output;
    }

    float[] sqrtArray(float[] input){
        float[] output = new float[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            output[i] = MathF.Sqrt(input[i]);
        }
        return output;
    }

    float[] minusArray(float intput1, float[] input2){
        float[] output = new float[input2.Length];
        for (int i = 0; i < input2.Length; i++)
        {
            output[i] = intput1 - input2[i];
        }
        return output;
    }

    float[] power2Array(float[] input){
        float[] output = new float[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            output[i] = input[i] * input[i];
        }
        return output;
    }

    float[] cosineArray(float[] input){
        float[] output = new float[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            output[i] = MathF.Cos(input[i]);
        }
        return output;
    }

    float[] sineArray(float[] input){
        float[] output = new float[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            output[i] = MathF.Sin(input[i]);
        }
        return output;
    }

    float[] generateRandomArray(int size, float min, float max)
    {
        float[] arr = new float[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = UnityEngine.Random.Range(min, max);
        }
        return arr;
    }
}
