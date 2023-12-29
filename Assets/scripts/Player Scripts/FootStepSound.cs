using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footStepsSounds;
    [SerializeField] private AudioSource audioSource;

    private int pos;

    public void PlayFootStepSound()
    {
        pos = (int)Mathf.Floor(Random.Range(0, footStepsSounds.Count));
        audioSource.PlayOneShot(footStepsSounds[pos]);    
    }
}
