using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;
using Firebase;
using Firebase.Database;

public class BeeMovement : MonoBehaviour
{
    public int id;
    [SerializeField] bool selected;
    DatabaseReference DBRef;
    BeeInfo beeInfo;
    private Transform beeChild;
    public float moveSpeed = 7.5f;

    public float damageAmount = 20f;
    public GameObject HP;

    Animator animator;

    HealthBar healthBar;

    [Header("Queen")]
    public float healthRegainRate = 5f; // Rate of health regained per second
    public float healthRegainRadius = 3f; // Radius within which health is regained
    private bool isWithinHealingAura = false;
    public ParticleSystem healingEffect;

    GameObject indicator;


    void Start()
    {
        beeChild = transform.GetChild(0);
        animator = GetComponentInChildren<Animator>();
        healthBar = GetComponentInChildren<HealthBar>();

        if(healingEffect!=null){
            healingEffect.Stop();
        }

        indicator = transform.GetChild(transform.childCount - 1).gameObject;
        indicator.SetActive(false);
        
        initialization();



    }
    void Update()
    {
        if(selected){
            MoveTowardsMouse();
            indicator.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(TobiiAPI.GetGazePoint().Screen).x, Camera.main.ScreenToWorldPoint(TobiiAPI.GetGazePoint().Screen).y, 0);
            
        }

        Flip();
        if (isWithinHealingAura){
                RegainHealth();
        }


        //UpdateFacingDirection();
    }

    private void MoveTowardsMouse()
    {
        //Eye Tracker Control
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(TobiiAPI.GetGazePoint().Screen);

        //Mouse Control
        // Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(TobiiAPI.GetGazePoint().Screen);

        mouseWorldPosition.z = 0;
        Vector3 directionToMove = (mouseWorldPosition - transform.position).normalized;

        if ((mouseWorldPosition - transform.position).magnitude > 0.1f)
        {

            // Update the bee's position
            transform.position += directionToMove * moveSpeed * Time.deltaTime;
        }


        updatePosition();

    }
    private void Flip()
    {
        if ((Camera.main.ScreenToWorldPoint(TobiiAPI.GetGazePoint().Screen) - transform.position).x > 0.1f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            HP.transform.eulerAngles = new Vector3(0, 0, 0);

        }
        if ((Camera.main.ScreenToWorldPoint(TobiiAPI.GetGazePoint().Screen) - transform.position).x < 0.1f)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            HP.transform.eulerAngles = new Vector3(0, 0, 0);

        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Bee Triggered");

        if (collider.gameObject.CompareTag("Keeper"))
        {
            Debug.Log("Beekeeper gets Attacked!");
            animator.SetTrigger("Attack");
            SoundManager.instance.PlayBeeAttackSound();
            StartCoroutine(resetTrigger("Attack"));
            // When the bee attacks the beekeeper
            HealthBar healthBar = collider.gameObject.GetComponentInChildren<HealthBar>();
            if (healthBar != null)
            {
                //gameObject.GetComponentInChildren<Animator>().SetTrigger("Hurt");
                KeeperOffline keeper;
                keeper = collider.gameObject.GetComponent<KeeperOffline>();
                healthBar.TakeDamage(damageAmount);
                keeper.updateHealth(healthBar.currentHealth);


            }

        }

        if (collider.CompareTag("Queen"))
        {
            StartRegainingHealth();
        }


        IEnumerator resetTrigger(string trigger)
        {
            yield return new WaitForSeconds(0.5f);
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Queen"))
        {
            StopRegainingHealth();
        }
    }
    void StartRegainingHealth()
    {
        isWithinHealingAura = true;
        healingEffect.Play();

    }
    void StopRegainingHealth()
    {
        // Stop regaining health
        isWithinHealingAura = false;
        healingEffect.Stop();

    }


    void RegainHealth()
    {

        // Increase health and update the health bar

        if (transform.Find("Queen") == null)
        {
            healthBar.currentHealth = Mathf.Min(healthBar.currentHealth + healthRegainRate * Time.deltaTime, healthBar.maxHealth);
            updateHealth(healthBar.currentHealth);
            healthBar.UpdateHealthBar();
            Debug.Log("Queen Nearby");
        }
    }


    void initialization(){
        if(id == GlobalData.playerRole){
            GetComponent<BeeMovement>().enabled = true;
            selected = true;

            indicator.SetActive(true);

        }else{
            GetComponent<BeeMovement>().enabled = false;
            selected = false;
            // GetComponentInChildren<Collider2D>().enabled = false;
        }

        if(DBRef == null){
            DBRef = FirebaseDatabase.DefaultInstance.GetReference("Bees");
        }

        updateHealth(healthBar.maxHealth);

        StartListener(transform);
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
                //   animator.SetTrigger("Attack");
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

    public void updateHealth(float health){
      DBRef.Child("B" + id.ToString()).Child("health").SetValueAsync(health);
    }
}
