using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthKill : MonoBehaviour
{
    public StealthKilled _enemy;
    public Transform _killPosition;
    public float time;
    public Animator anim;

    public StealthKilled Enemy
    {
        get { return _enemy; }
        set { _enemy = value; }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _enemy != null) 
        {
            this.transform.SetPositionAndRotation(_killPosition.position, _killPosition.rotation);

            if(this.transform.position == _killPosition.position && this.transform.rotation == _killPosition.rotation) 
            {
                Debug.Log("Stealth kill");

                anim.SetTrigger("Kill");

                StartCoroutine(EndKillStealth());
            }
        }
    }

    IEnumerator EndKillStealth()
    {
        yield return new WaitForSeconds(time);

        _enemy = null;
        _killPosition = null;
    }
}
