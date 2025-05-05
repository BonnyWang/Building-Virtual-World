using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class Bee : MonoBehaviour
{
    public int id;
    BeeInfo beeInfo;

    // if this is pulling data from the database
    bool selected;
    bool pulling;

    public static DatabaseReference DBRef;

    public float damageAmount = 20f;

    Animator animator;

    HealthBar healthBar;

    void Start()
    {
        // animator = GetComponentInChildren<Animator>();
        // healthBar = GetComponentInChildren<HealthBar>();
        // initialization();
        // updateHealth(healthBar.maxHealth);
        // StartListener(transform);

    }

    // Update is called once per frame
    private void FixedUpdate() {
      if(selected){
        // Only pull data from the database
        updatePosition();
      }
    }

    void initialization(){
        if(id == GlobalData.playerRole){
            GetComponent<BeeMovement>().enabled = true;
            selected = true;
        }else{
            GetComponent<BeeMovement>().enabled = false;
            selected = false;
        }

        if(DBRef == null){
            DBRef = FirebaseDatabase.DefaultInstance.GetReference("Bees");
        }

        updatePosition();
    }

    void StartListener(Transform transform) {
        DBRef.ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
          if (e2.DatabaseError != null) {
            Debug.LogError(e2.DatabaseError.Message);
            return;
          }
          if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0) {
              beeInfo = BeeInfo.CreateFromJSON(e2.Snapshot.Child("B" + id.ToString()).GetRawJsonValue());

              if(!selected){
                // Debug.Log(beeInfo.getPosition());
                transform.position = beeInfo.getPosition();
                if(beeInfo.Attack){
                  animator.SetTrigger("Attack");
                }

                healthBar.currentHealth = beeInfo.health;
                healthBar.UpdateHealthBar();
              // }
              }else{
                healthBar.currentHealth = beeInfo.health;
                healthBar.UpdateHealthBar();
              }
          }
          
        };
    }


    void updatePosition(){
        DBRef.Child("B" + id.ToString()).Child("x").SetValueAsync(transform.position.x);
        DBRef.Child("B" + id.ToString()).Child("y").SetValueAsync(transform.position.y);
        DBRef.Child("B" + id.ToString()).Child("z").SetValueAsync(transform.position.z);
    }

    IEnumerator resetTrigger(string trigger){
      yield return new WaitForSeconds(0.5f);
      DBRef.Child("B" + id.ToString()).Child("Attack").SetValueAsync(false);
    }

    public void updateHealth(float health){
      DBRef.Child("B" + id.ToString()).Child("health").SetValueAsync(health);
    }

     private void OnTriggerEnter2D(Collider2D collider)
    {
      Debug.Log("Bee Triggered");

        if (collider.gameObject.CompareTag("Keeper"))
        {
            Debug.Log("Beekeeper gets Attacked!");
            animator.SetTrigger("Attack");
            SoundManager.instance.PlayBeeAttackSound();
            DBRef.Child("B" + id.ToString()).Child("Attack").SetValueAsync(true);
            StartCoroutine(resetTrigger("Attack"));
            // When the bee attacks the beekeeper
            HealthBar healthBar = collider.gameObject.GetComponentInChildren<HealthBar>();
            if (healthBar != null)
            {
                collider.gameObject.GetComponentInChildren<Animator>().SetTrigger("Hurt");
                // SoundManager.instance.PlayBeekeeperHurtSound();
                Keeper keeper;
                keeper = collider.gameObject.GetComponent<Keeper>();
                healthBar.TakeDamage(damageAmount);
                keeper.updateHealth(healthBar.currentHealth);

                
            }
        }

    }

}
