using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyProjectile : MonoBehaviour
{
    public int damage;
    public float despawnTimer;

    private Rigidbody rb;
    private float timer;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= despawnTimer)
            Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

            player.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
