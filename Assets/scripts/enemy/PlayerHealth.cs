using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats")]
    public int hp;
    private float invAmt;
    //public int startingHealth = 100;
    //public int currentHealth;

    [Header("Health Slider")]
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new(1f, 0f, 0f, 0.1f);

    PlayerHealth playerHealth;
    public Animator anim;
    public bool isDead;
    bool damaged;
    Scene currentScene;
    public string sceneName;

    public HealthSlider healthbar;

    public InteractionPromptUI interactionPromptUI;

    public void Start()
    {
        //if (sceneName == "Level_Open_Remaster")
        //{
        //    InvokeRepeating("GetHp", 30, 30);
        //}
        healthbar.SetMaxHealth(hp);
    }

    void Update()
    {
        if (damaged && damageImage != null) // Check if damageImage is not null before accessing it
        {
            damageImage.color = flashColour;
        }
        else if (damageImage != null) // Check if damageImage is not null before accessing it
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        //Dodge
        if (invAmt > 0)
        {
            invAmt -= Time.deltaTime;
        }

        if (isDead && Input.GetKeyDown(KeyCode.L))
        {
            Revive();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(hp);
        }


    }

    public void Revive()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        SceneManager.LoadScene(currentScene.name);
        GameObject spawnPoint = GameObject.FindWithTag("Spawn");
        transform.position = spawnPoint.transform.position;
        Item currentKeyItem = spawnPoint.GetComponent<ItemController>().item;
        if (currentKeyItem != null)
        {
            Inventory.Instance.Remove(spawnPoint.GetComponent<ItemController>().item);
        }
        anim.SetTrigger("Revive");
        hp = 100;
        healthbar.SetHealth(hp);
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        damaged = true;

        if (invAmt <= 0)
        {
            hp -= damage;
        }

        healthbar.SetHealth(hp);

        // healthSlider.value = health;

        if (hp <= 0 && !isDead)
        {
            anim.SetTrigger("isDead");
            isDead = true;
            interactionPromptUI.SetUp("You Died, Press L to Restart");
            StartCoroutine(DoAfterFiveSeconds());

        }
    }

    public void GetHp()
    {
        int healthPickup = 25;
        playerHealth = GetComponent<PlayerHealth>();

        playerHealth.hp += healthPickup;

        if (playerHealth.hp > 100)
            playerHealth.hp = 100;

        healthbar.SetHealth(hp);
        //playerHealth.health += 25;
    }

    public void Invinsible(float delay, float invLength)
    {
        if (delay > 0)
        {
            StartCoroutine(StartInvinsible(delay, invLength));
        }
        else
        {
            invAmt = invLength;
        }
    }

    IEnumerator StartInvinsible(float delay, float invLength)
    {
        yield return new WaitForSeconds(delay);
        invAmt = invLength;
    }

    IEnumerator DoAfterFiveSeconds()
    {
        yield return new WaitForSeconds(5);

        interactionPromptUI.Close();

    }
}