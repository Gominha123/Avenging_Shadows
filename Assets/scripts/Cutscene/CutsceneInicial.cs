using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneInicial : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DoAfterSixtyFourSeconds());
    }

    IEnumerator DoAfterSixtyFourSeconds()
    {
        yield return new WaitForSeconds(65);

        SceneManager.LoadScene("mapa da neve");

    }
}
