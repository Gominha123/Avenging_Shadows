using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip SwordSlash, BFSwordSlash, SpearSlash;
    [SerializeField] private List<AudioClip> dodgeRollSounds;
    [SerializeField] private List<AudioClip> deathSounds;

    private int posDodge, posDeath;

    public void PlaySwordSlashSound()
    {
        audioSource.PlayOneShot(SwordSlash);
    }

    public void PlayBFSwordSlashSound()
    {
        audioSource.PlayOneShot(BFSwordSlash);
    }

    public void PlaySpearSlashSound()
    {
        audioSource.PlayOneShot(SpearSlash);
    }

    public void PlayDodgeRollSound()
    {
        posDodge = (int)Mathf.Floor(Random.Range(0, dodgeRollSounds.Count));
        audioSource.PlayOneShot(dodgeRollSounds[posDodge]);
    }

    public void PlayDeathSound()
    {
        posDeath = (int)Mathf.Floor(Random.Range(0, deathSounds.Count));
        audioSource.PlayOneShot(deathSounds[posDeath]);
    }

}
