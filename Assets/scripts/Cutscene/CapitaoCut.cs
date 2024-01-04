using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Playables;

public class CapitaoCut : MonoBehaviour
{
    EnemyHealth enemyHealth;

    public PlayableDirector playableDirector;

    public GameObject player;

    public GameObject playerPos;

    public GameObject capitaoPos;

    private bool cutStart;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        playableDirector = GameObject.FindGameObjectWithTag("CutsceneDirector").GetComponent<PlayableDirector>();
        player = GameObject.FindGameObjectWithTag("Player");

        cutStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.health <= 0 && !cutStart) 
        {
            cutStart = true;
            player.transform.position = playerPos.transform.position;
            player.transform.Rotate(new Vector3(0, 180f, 0)); 
            this.transform.position = capitaoPos.transform.position;
            transform.rotation = capitaoPos.transform.rotation;
            playableDirector.Play();
        }
    }
}
