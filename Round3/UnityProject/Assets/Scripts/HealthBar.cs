using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HealthBar : MonoBehaviour
{
    public KeeperChild keeperChild;
    public BeeHurt beeHurt;

    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthFill; // Drag the child health image here

    KeeperOffline keeper;
    BeeMovement bee;

    public static int beeCollected;
    public GameObject beeGO;

    bool Died;

    private void Start()
    {
        currentHealth = maxHealth;
        if(transform.parent.GetComponent<KeeperOffline>() != null){
            keeper = transform.parent.GetComponent<KeeperOffline>();
        }

        if(transform.parent.GetComponent<BeeMovement>() != null){
            bee = transform.parent.GetComponent<BeeMovement>();
        }

        //healthFill = GetComponentsInChildren<Image>()[1];

        beeCollected = 0;

        UpdateHealthBar();

        Died = false;
    }

    public void TakeDamage(float damageAmount)
    {
        keeperChild.HurtShader();
        Debug.Log(beeHurt);
        if (beeHurt != null)
        {
            beeHurt.HurtShader();
        }
        

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        if (keeper != null)
        {
            keeper.updateHealth(currentHealth);

            if (currentHealth <= 0)
            {
                keeper.animator.SetTrigger("Die");
                SoundManager.instance.PlayDieSound();
                Global.BeeWin = true;
                StartCoroutine(loadResult());

            }
        }

    }

    IEnumerator loadResult(){
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("WinScreen");
    }

    public void UpdateHealthBar()
    {
        if(bee != null){
            if(currentHealth <= 0){
                beeGO.SetActive(false);

                SoundManager.instance.IncreaseBgmStage();

                if(!Died){
                    Died = true;
                    beeCollected++;
                }
            }
        }


        if(beeCollected >= 3){
            Global.BeeWin = false;
            StartCoroutine(loadResult());
        }

        healthFill.fillAmount = currentHealth / maxHealth;
    }
}
