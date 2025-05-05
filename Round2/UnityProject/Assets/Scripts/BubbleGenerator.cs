using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
    public GameObject bubble;
    public int color;    
    [SerializeField] Transform genPosition;

    static SoundPlayer squeezeSound;
    

    void Start()
    {
        squeezeSound = GameObject.Find("Squeeze").GetComponent<SoundPlayer>();
        //InvokeRepeating("generate_bubble", 0f, 7f);//for testing, delete after
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generate_bubble(){
        float giant = Random.Range(0.0f, 1f);
        


        GameObject newBubble = Instantiate(bubble, genPosition.position, Quaternion.identity);
        newBubble.GetComponentInChildren<Bubble>().setColor(color);
        Transform armatureTransform = newBubble.transform.GetChild(0);
        Bubble bubbleScript = armatureTransform.GetComponent<Bubble>();
        StartCoroutine(MoveHorizontally(newBubble.GetComponentInChildren<Bubble>().transform, bubbleScript));

        //for testing, change to 0.05 afterwards
        if (giant < 0.0f)
        {
            StartCoroutine(ScaleBubble(armatureTransform, true));
            
            bubbleScript.health = 7;
        }
        else
        {
            StartCoroutine(ScaleBubble(armatureTransform));
        }
        
        Collider[] colliders =  newBubble.GetComponentsInChildren<Collider>();
        foreach(Collider col in colliders){
            col.enabled = false;
        }
        StartCoroutine(reactivate_collider(colliders));
        squeezeSound.play_random();
    }

    IEnumerator reactivate_collider(Collider[] cols){
        yield return new WaitForSeconds(0.25f);
        foreach(Collider col in cols){
            col.enabled = true;
        }
    }
    IEnumerator MoveHorizontally(Transform bubbleTransform, Bubble bubblescript)
    {
        Vector3 startPosition = bubbleTransform.position;
        float randomZ = Random.Range(0.15f, 0.60f);
        float randomY = Random.Range(-0.1f, 0.3f);
        Vector3 endPosition = startPosition + new Vector3(0.0f, randomY, -randomZ); // using random for more fun and stop collison
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(startPosition, endPosition);
        while (Time.time - startTime < 2)
        { // Run for 2 second,break whenever interacted is true;
            if (bubblescript.interacted == true)
            { break; }
            float distCovered = (Time.time - startTime);
            float fractionOfJourney = distCovered / 2; // Denominator is the time in seconds
            bubbleTransform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0.0f, 1.0f, fractionOfJourney));
            yield return null;
        }
        // bubbleTransform.position = endPosition;
    }

    //function for bubble size change when created
    IEnumerator ScaleBubble(Transform bubbleTransform, bool giant = false)
    {
        float startTime = Time.time;
        Vector3 startScale;
        Vector3 midScale;
        Vector3 endScale;
        float growDuration = 0.6f;  // Time to go from startScale to midScale
        float reduceDuration = 0.25f;
        if (giant)
        {
            startScale = new Vector3(0f, 0f, 0f);
            midScale = new Vector3(1.7f, 1.7f, 1.7f);
            endScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            startScale = new Vector3(0.1f, 0.1f, 0.1f);
            midScale = new Vector3(1.2f, 1.2f, 1.2f);
            endScale = new Vector3(1f, 1f, 1f);
        }
        if (giant)
        {
            growDuration = 1.2f;
            reduceDuration = 0.5f;
        }

        // Scale up
        while (Time.time - startTime <= growDuration)
        {
            float t = (Time.time - startTime) / growDuration;
            bubbleTransform.localScale = Vector3.Lerp(startScale, midScale, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        // Reset start time
        startTime = Time.time;

        // Scale down
        while (Time.time - startTime <= reduceDuration)
        {
            float t = (Time.time - startTime) / reduceDuration;
            bubbleTransform.localScale = Vector3.Lerp(midScale, endScale, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        bubbleTransform.localScale = endScale;
    }

}
