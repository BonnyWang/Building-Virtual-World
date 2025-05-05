using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class testMovePosition : MonoBehaviour
{
    float x;
    BeeInfo beeInfo;

    public int id;

    bool parent;

    bool pulling;
    void Start()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        
        StartListener(transform);
        pulling = false;
        parent = false;
    }

    public void setAsParent(){
      parent = true;
    }

    private void FixedUpdate() {
      if(!pulling && parent){
        updatePosition();
      }
      if(parent){
        Moving();
      }
    }

    void Moving(){
      // Get Input axies
      transform.position = transform.position + new Vector3(Input.GetAxis("Horizontal"),0,0);
    }

    void StartListener(Transform transform) {
      FirebaseDatabase.DefaultInstance
        .GetReference("Bees")
        .ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
          if (e2.DatabaseError != null) {
            Debug.LogError(e2.DatabaseError.Message);
            return;
          }
          if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0) {
              if(!parent){
                beeInfo = BeeInfo.CreateFromJSON(e2.Snapshot.Child("B" + id.ToString()).GetRawJsonValue());
                Debug.Log(beeInfo.getPosition());
                transform.position = beeInfo.getPosition();
                pulling = true;
                StartCoroutine(freezePulling());
              // }
              }
          }
          
        };
    }

    void updatePosition(){
      FirebaseDatabase.DefaultInstance
        .GetReference("Bees").Child("B" + id.ToString()).Child("x").SetValueAsync(transform.position.x);
    }

    IEnumerator freezePulling(){
      yield return new WaitForSeconds(1f);
      pulling = false;
    }


}
