using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class WeaponUpgradeScript : MonoBehaviour
{
    public Material[] mat;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.enabled = true;
        rend.sharedMaterial = mat[0];
        Upgrade();
    }

    void Upgrade()
    {
        rend.sharedMaterial = mat[1];
        StartCoroutine(DoAfterFiveSeconds());
    }

    void StopUpgrade()
    {
        //Debug.Log("here");                                tirar de coment quando for para por a funcionar não sei porque está aqui
        rend.sharedMaterial = mat[0];
    }

    IEnumerator DoAfterFiveSeconds()
    {
        yield return new WaitForSeconds(5);

        StopUpgrade();

    }
}
