using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    //public int startingHealth = 100;
    //public int currentHealth;

    [Header("Health Slider")]
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    PlayerMovement2 playermovement;
    PlayerHealth playerHealth;
    public bool isDead;
    bool damaged;
    Scene currentScene;
    public string sceneName;
    internal bool isdead;

    public HealthSlider healthbar;

    public void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName == "Level_Open_Remaster")
        {
            InvokeRepeating("GetHp", 30, 30);
        }
        healthbar.SetMaxHealth(health);
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
    }

    public void TakeDamage(int damage)
    {
        damaged = true;

        health -= damage;

        healthbar.SetHealth(health);

       // healthSlider.value = health;

        if (health <= 0 && !isDead)
            Death();
    }

    void Death()
    {
        isDead = true;
    }
    public void GetHp()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerHealth.health += 25;
    }
}