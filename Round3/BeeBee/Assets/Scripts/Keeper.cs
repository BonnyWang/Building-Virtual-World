using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;


public class Keeper : MonoBehaviour
{
    KeeperInfo keeperInfo;

    // if this is pulling data from the database
    bool selected;
    bool pulling;

    DatabaseReference DBRef;

    bool AttakInput;
    public Animator animator;

    public float speed = 5.0f;
    private Vector2 movement;

    public Transform[] bees;

    Transform Net;
    HealthBar healthBar;
    
    int beeCollected;
    Vector3 prevPosition;

    bool canAttack = true;
    float cooldown = 1.1f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        healthBar = GetComponentInChildren<HealthBar>();
        Net = transform.GetChild(0);
        initialization();
        StartListener(transform);

        // SoundManager.instance.IncreaseBgmStage();

    }

    // Update is called once per frame
    void Update()
    {
      

    }

    IEnumerator resetTrigger(string trigger){
      yield return new WaitForSeconds(0.1f);
      DBRef.Child("Instance").Child(trigger).SetValueAsync(false);
    }

    private void FixedUpdate()
    {
        if (selected)
        {
            // Only push data from the database
            updatePosition();
            moving();

            AttakInput = Input.GetButtonDown("Jump");
            if (AttakInput && canAttack)
            {
                attack();
                StartCoroutine(Cooldown());
            }
        }

        UpdateFacingDirection();
    }

    void attack(){
      
      SoundManager.instance.PlayKeeperAttackSound();
      Debug.Log("Attack");
      animator.SetTrigger("Attack");
      DBRef.Child("Instance").Child("Attack").SetValueAsync(true);
      StartCoroutine(resetTrigger("Attack"));


      bees = GameObject.Find("Bees").GetComponentsInChildren<Transform>();
      foreach(Transform bee in bees){
        if(Vector3.Magnitude(bee.position - Net.position) < 1.1f){
          SoundManager.instance.IncreaseBgmStage();
          SoundManager.instance.PlayDieSound();
          // Bee damages
          bee.GetComponentInChildren<HealthBar>().TakeDamage(10f);
          bee.GetComponent<Bee>().updateHealth(bee.GetComponentInChildren<HealthBar>().currentHealth);
        }
      }


    }

    // Reset the net collider
    void moving(){
      movement.x = Input.GetAxis("Horizontal");
      movement.y = Input.GetAxis("Vertical");

      animator.SetFloat("Move", Mathf.Max(Mathf.Abs(movement.y), Mathf.Abs(movement.x)));
      transform.Translate(movement * speed * Time.fixedDeltaTime);
      DBRef.Child("Instance").Child("Move").SetValueAsync(animator.GetFloat("Move"));
    }

    void initialization(){
        if(GlobalData.playerRole == -1){
            // GetComponent<BeekeeperMovement>().enabled = true;
            selected = true;
        }else{
            // GetComponent<BeekeeperMovement>().enabled = false;
            selected = false;
        }

        if(DBRef == null){
            DBRef = FirebaseDatabase.DefaultInstance.GetReference("Beekeeper");
        }

        updateHealth(healthBar.maxHealth);

    }

    void StartListener(Transform transform) {
        DBRef.ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
          if (e2.DatabaseError != null) {
            Debug.LogError(e2.DatabaseError.Message);
            return;
          }
          if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0) {
              keeperInfo = KeeperInfo.CreateFromJSON(e2.Snapshot.Child("Instance").GetRawJsonValue());

              if(!selected){
                // Debug.Log(e2.Snapshot.Child("Instance").GetRawJsonValue());
                transform.position = keeperInfo.getPosition();
                animator.SetFloat("Move", keeperInfo.Move);
                if(keeperInfo.Attack){
                  Debug.Log("Attack Pulled");
                  attack();
                }

                healthBar.currentHealth = keeperInfo.health;
                healthBar.UpdateHealthBar();
                
              }else{
                healthBar.currentHealth = keeperInfo.health;
                healthBar.UpdateHealthBar();

                

              }
          }
          
        };
    }

    void updatePosition(){
        DBRef.Child("Instance").Child("x").SetValueAsync(transform.position.x);
        DBRef.Child("Instance").Child("y").SetValueAsync(transform.position.y);
        DBRef.Child("Instance").Child("z").SetValueAsync(transform.position.z);
    }

    void updateAnimator(){
        DBRef.Child("Instance").Child("").SetValueAsync(animator.GetFloat("MoveY"));
    }

    public void updateHealth(float health){
      DBRef.Child("Instance").Child("health").SetValueAsync(health);
    }


    IEnumerator Cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    private void UpdateFacingDirection()
    {
        if((transform.position.x - prevPosition.x) == 0){
            return;
        }

        bool direction = (transform.position.x - prevPosition.x) > 0;

        prevPosition = transform.position;

        // Determine the direction the bee should face
        if (direction)
        {
            // Mouse is to the right of the bee
            transform.localScale = new Vector3(-8, 8, 8); // Face right
        }
        else
        {
            // Mouse is to the left of the bee
            transform.localScale = new Vector3(8, 8, 8); // Face left
        }
    }
}
