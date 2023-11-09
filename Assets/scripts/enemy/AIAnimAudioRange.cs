
using UnityEngine;
using System.Collections.Generic;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class AIAnimAudioRange : MonoBehaviour
{
    public AISimplesRange Navgador;
    public AudioClip Atack, Searching, Patrolling, Following;

    private Animator _animator;
    private AudioSource _audioSource;
    private bool _isAudioPlaying;

    private Dictionary<AISimplesRange.stateOfAi, AudioClip> audioClips = new Dictionary<AISimplesRange.stateOfAi, AudioClip>();

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        // Inicializa o dicionário de clipes de áudio
        audioClips[AISimplesRange.stateOfAi.patrolling] = Patrolling;
        audioClips[AISimplesRange.stateOfAi.following] = Following;
        audioClips[AISimplesRange.stateOfAi.attacking] = Atack;
        audioClips[AISimplesRange.stateOfAi.searchingLostTarget] = Searching;
    }

    void Update()
    {
        UpdateAudio();
        UpdateAnimations();
    }

    void UpdateAnimations()
    {
        AISimplesRange.stateOfAi currentState = Navgador.GetCurrentState();
        _animator.SetBool("Attack", currentState == AISimplesRange.stateOfAi.attacking);
        //_animator.SetBool("Searching", currentState == AISimplesRange.stateOfAi.searchingLostTarget);
        _animator.SetBool("Patrolling", currentState == AISimplesRange.stateOfAi.patrolling);
        _animator.SetBool("Following", currentState == AISimplesRange.stateOfAi.following);
        _animator.SetBool("Waiting", currentState == AISimplesRange.stateOfAi.waiting);
    }

    void UpdateAudio()
    {
        AISimplesRange.stateOfAi currentState = Navgador.GetCurrentState();

        if (audioClips.TryGetValue(currentState, out AudioClip clip))
        {
            if (!_isAudioPlaying || !_audioSource.isPlaying)
            {
                _audioSource.clip = clip;
                _audioSource.Play();
                _isAudioPlaying = true;
            }
        }
    }
}






