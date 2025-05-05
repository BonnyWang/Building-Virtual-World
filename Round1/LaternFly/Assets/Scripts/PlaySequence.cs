using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySequence : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator play(){
        yield return null;

        // Sequentially play all audio clips
        foreach(AudioClip clip in audioClips){
            audioSource.PlayOneShot(clip);
            // Wait for the clip to finish playing
            while(audioSource.isPlaying){
                yield return null;
            }
        }
    }
}
