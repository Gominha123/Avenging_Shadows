using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponUpgradeScript : MonoBehaviour
{
    public Material[] mat;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        mat[0] = rend.sharedMaterial;
        Debug.Log(mat[0]);
    }

    void Upgrade()
    {
        
    }
}
