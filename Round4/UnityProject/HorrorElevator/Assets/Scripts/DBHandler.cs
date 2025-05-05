using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
public class DBHandler : MonoBehaviour
{
    public DatabaseReference GhostRef;
    public DatabaseReference PlayerRef;
    public static DBHandler instance;
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialization(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }

        GhostRef = FirebaseDatabase.DefaultInstance.GetReference("Ghost");
        PlayerRef = FirebaseDatabase.DefaultInstance.GetReference("Player");
        // DBref.Child("Position").Child("x").SetValueAsync(1000);
        
    }

    public void setPlayerRotation(float x, float y, float z){
        PlayerRef.Child("instance").Child("RX").SetValueAsync(x);
        PlayerRef.Child("instance").Child("RY").SetValueAsync(y);
        PlayerRef.Child("instance").Child("RZ").SetValueAsync(z);
    }

    public void setPlayerState(string name, bool state, bool isTrigger){
        PlayerRef.Child("instance").Child(name).SetValueAsync(state);
        if(isTrigger){
            Invoke("resetStateTrigger", 1f);
        }
    }

    void resetStateTrigger(){
        PlayerRef.Child("instance").Child("Scared").SetValueAsync(false);
    }

    public void setPlayerShake(int magnitude){
        PlayerRef.Child("instance").Child("Shake").SetValueAsync(magnitude);
    }
}
