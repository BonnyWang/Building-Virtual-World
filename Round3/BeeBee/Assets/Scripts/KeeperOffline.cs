using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Firebase;
using Firebase.Database;


public class KeeperOffline : MonoBehaviour
{
    KeeperInfo keeperInfo;
    // if this is pulling data from the database
    [SerializeField] bool selected;
    DatabaseReference DBRef;

    public Animator animator;
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    //public Transform[] bees;

    HealthBar healthBar;
    public StaminaWheel staminaWheel;
    public GameObject stamina;
    public GameObject hpBar;

    int beeCollected;
    bool canAttack = true;
    public float cooldown = 0.1f;
    //public float attackDamage = 20f;

    public bool isRolling = false;
    public float attackStaminaCost = 50f;
    public float rollStaminaCost = 50f;

    public int defaultLayer;
    public int noBeeCollisionLayer;

    public Collider2D netCollider;

    Vector3 prevPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<HealthBar>();
        netCollider.enabled = false; // Net collider initially disabled

        initialization();
        StartListener(transform);

    }

    // Update is called once per frame
    void Update()
    {
        if(selected){
            movementInput.x = Input.GetAxis("Horizontal");
            movementInput.y = Input.GetAxis("Vertical");


            if (Input.GetKeyDown(KeyCode.K) && !isRolling)
            {
                StartRoll();
            }

            if (Input.GetKeyDown(KeyCode.J) && canAttack)
            {
                attack();
                //StartCoroutine(Cooldown());
            }
        }

        Flip();



    }

    private void FixedUpdate()
    {
        if(selected){
            updatePosition();
            if (!isRolling)
            {
                // Regular movement logic
                Vector2 movement = movementInput * speed;
                rb.velocity = movement;
            }

        
            // Update animator based on actual movement
            animator.SetFloat("Move", rb.velocity.magnitude);
            DBRef.Child("Instance").Child("Move").SetValueAsync(rb.velocity.magnitude);
        }else{
            UpdateFacingDirection();
        }

        
    }

    void attack()
    {
        if (staminaWheel.stamina >= attackStaminaCost)
        {
            SoundManager.instance.PlayKeeperAttackSound();
            Debug.Log("Attack");
            animator.SetTrigger("Attack");
            canAttack = false;
            staminaWheel.UseStamina(attackStaminaCost);
            // Activate the net's collider during the attack
            netCollider.enabled = true;

            DBRef.Child("Instance").Child("Attack").SetValueAsync(true);
            StartCoroutine(resetTrigger("Attack"));


        }
        else
        {
            Debug.Log("Not enough stamina to Attack");
        }

        //bees = GameObject.Find("BeeGroup").GetComponentsInChildren<Transform>();
        //foreach (transform bee in bees)
        //{
        //    if (vector3.magnitude(bee.position - net.position) < 1.1f)
        //    {
        //        soundmanager.instance.increasebgmstage();
        //        soundmanager.instance.playdiesound();
        //        // bee damages
        //        bee.getcomponentinchildren<healthbar>().takedamage(10f);
        //        bee.getcomponent<bee>().updatehealth(bee.getcomponentinchildren<healthbar>().currenthealth);
        //    }
        //}
    }

    private void StartRoll()
    {
        if (staminaWheel.stamina >= rollStaminaCost)
        {
            animator.SetTrigger("Roll");
            isRolling = true;
            staminaWheel.UseStamina(rollStaminaCost);
            gameObject.layer = noBeeCollisionLayer; // Change to a layer that doesn't collide with Bees
        }
        else
        {
            Debug.Log("Not enough stamina to roll");
        }


    }
    public void ResetRolling()
    {
        isRolling = false;
        Debug.Log("ResetRolling called");
        gameObject.layer = defaultLayer; // Change back to the default layer

    }

    public void ResetAttack()
    {
        canAttack = true;
        Debug.Log("ResetAttack called");
        netCollider.enabled = false;

    }

    public void updateHealth(float health)
    {
        // Update health locally
        Debug.Log("Updating health to: " + health);

        healthBar.currentHealth = health;
        DBRef.Child("Instance").Child("health").SetValueAsync(health);
        healthBar.UpdateHealthBar();
    }
    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.CompareTag("Bee"))
    //    {
    //        Debug.Log("Attack Succeed!");
    //        HealthBar healthBar = collider.gameObject.GetComponentInChildren<HealthBar>();
    //        if (healthBar != null)
    //        {
    //            healthBar.TakeDamage(attackDamage);
    //        }
    //    }
    //}


    private void Flip()
    {
        if (movementInput.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            stamina.transform.eulerAngles = new Vector3(0, 0, 0);
            hpBar.transform.eulerAngles = new Vector3(0, 180, 0);

        }
        if (movementInput.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            stamina.transform.eulerAngles = new Vector3(0, 0, 0);
            hpBar.transform.eulerAngles = new Vector3(0, 180, 0);

        }
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

void StartListener(UnityEngine.Transform transform) {
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

                if(keeperInfo.Roll){
                  Debug.Log("Roll Pulled");
                  StartRoll();
                }

                movementInput.x = keeperInfo.moveInputX;

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

    IEnumerator resetTrigger(string trigger){
      yield return new WaitForSeconds(0.1f);
      DBRef.Child("Instance").Child(trigger).SetValueAsync(false);
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
