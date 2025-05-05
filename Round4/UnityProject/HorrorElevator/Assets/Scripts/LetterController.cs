using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterController : MonoBehaviour
{    
    [SerializeField] private GameObject letter;
    [SerializeField] private int waitLength;
    [SerializeField] private Clue letterClue;

    // Start is called before the first frame update
    void Start()
    {
        letterClue = letter.GetComponent<Clue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LetterReadThrough()
    {
        SoundManager.instance.PlayLetterPickedUpSound();

        Debug.Log("Start Wait");
        yield return new WaitForSeconds(waitLength);

        Debug.Log("End Wait");
        letterClue.dissolvePaper();

        letterClue.removePS();

        yield return new WaitForSeconds(5);

        

    }
}
