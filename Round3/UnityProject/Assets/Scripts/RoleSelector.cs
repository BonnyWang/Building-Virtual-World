using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoleSelector : MonoBehaviour
{
    const int KEEPER = -1;
    const int BRIAN = 0;
    const int BEN = 1;
    const int BILLY = 2;

    DatabaseReference DBRef;
    DatabaseReference KeeperRef;
    DatabaseReference BeeRef;

    [SerializeField] Button[] buttons;
    private void Start() {
        GlobalData.playerRole = -1;

        DBRef = FirebaseDatabase.DefaultInstance.RootReference;
        KeeperRef = FirebaseDatabase.DefaultInstance.GetReference("Beekeeper");
        BeeRef = FirebaseDatabase.DefaultInstance.GetReference("Bees");

        // Reset everyone to not ready when first open the game
        KeeperRef.Child("Instance").Child("Ready").SetValueAsync(false);
        for (int i = 0; i < 4; i++){
            BeeRef.Child("B"+ i.ToString()).Child("Ready").SetValueAsync(false);
        }

        DBRef.ValueChanged += checkAllPlayerReady;


    }
    public void setRole(int role){
        GlobalData.playerRole = role;
        

        if(role == KEEPER){
            KeeperRef.Child("Instance").Child("Ready").SetValueAsync(true);
        }else{
            BeeRef.Child("B"+ role.ToString()).Child("Ready").SetValueAsync(true);
        }

        waitOtherPlayer(role);
        // SceneManager.LoadScene("Main");
    }

    // dectivate all buttons
    public void waitOtherPlayer(int role){
        for (int i = 0; i < buttons.Length; i++){
            if(EventSystem.current.currentSelectedGameObject != buttons[i].gameObject){        
                buttons[i].interactable = false;
            }
        }
    }

    void checkAllPlayerReady(object sender2, ValueChangedEventArgs e2){
        if (e2.DatabaseError != null) {
            Debug.LogError(e2.DatabaseError.Message);
            return;
        }
        if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0) {
            KeeperInfo keeperInfo = KeeperInfo.CreateFromJSON(e2.Snapshot.Child("Beekeeper").Child("Instance").GetRawJsonValue());

            // ...whatever
            if(keeperInfo.Ready){
                for (int i = 0; i < 3; i++){
                    BeeInfo beeInfo = BeeInfo.CreateFromJSON(e2.Snapshot.Child("Bees").Child("B"+ i.ToString()).GetRawJsonValue());
                    if(!beeInfo.Ready){
                        return;
                    }
                }
                // SceneManager.LoadScene("Main");
                StartCoroutine(loadMainScne(GlobalData.playerRole + 1f));
            }
        }
    }

    IEnumerator loadMainScne(float waitTime){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Main");
    }
}
