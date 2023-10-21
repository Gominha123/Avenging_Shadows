using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DodgeRoll : MonoBehaviour
{
    public KeyCode DodgeRollKey = KeyCode.C;

    public PlayerHealth hp;
    private Rigidbody rb;

    public Animator anim;

    public float invDdelay;
    public float invDuration;
    
    public float DodgeCd;
    private float actCd;

    public float pushAmt;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool Roll = Input.GetMouseButtonDown(1);
        Debug.Log("Roll");
        if(actCd <= 0 ) 
        {
            //anim.ResetTrigger("Roll");
            if(Roll )
            {
                Dodge();
            }
        }
        else
        {
            actCd -= Time.deltaTime;
        }

    }

    private void Dodge()
    {
        actCd = DodgeCd;


        rb.AddForce(transform.forward * pushAmt, ForceMode.Force);

        anim.SetTrigger("Roll");
    }
}
