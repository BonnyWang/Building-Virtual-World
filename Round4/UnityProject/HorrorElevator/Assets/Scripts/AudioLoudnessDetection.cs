using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    private int sampleWindow = 64;
    private AudioClip microClip;

    // Start is called before the first frame update
    void Start()
    {
        MicrophoneToAudioClip();
    }

    // Update is called once per frame
    void Update()
    { 

        if(GetLoudnessFromMicrophone() > 2f){
            Debug.Log("Slowed down");
            Time.timeScale = 0.5f;
        }else{
            Time.timeScale = 1f;
        }
    }

    private void MicrophoneToAudioClip()
    {
        if(Microphone.devices.Length == 0)
        {
            // Debug.Log("No microphone device found");
            return;
        }
        string microName = Microphone.devices[0];
        microClip = Microphone.Start(microName, true, 20, AudioSettings.outputSampleRate);
    }

    // Get current voice loudness from microphone (the first one in the microphone device list)
    public float GetLoudnessFromMicrophone()
    {
        if(Microphone.devices.Length == 0)
        {
            // Debug.Log("No microphone device found");
            return 0;
        }
        return GetLoudnessAudioClip(Microphone.GetPosition(Microphone.devices[0]), microClip);
    }

    private float GetLoudnessAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
        {
            return 0;
        }

        float[] waveData =new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;

        for (int i = 0; i < waveData.Length; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness;
    }
}
