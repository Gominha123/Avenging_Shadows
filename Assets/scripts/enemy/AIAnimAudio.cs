using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class AIAnimAudio : MonoBehaviour
{
    public AISimples Navgador;
    public AudioClip Attacking, Waiting, Patrolling, Following, SearchingLostTarget;

    private Animator _animator;
    private Rigidbody rb;
    private AudioSource _audioSource;
    private bool _isAudioPlaying;
    private bool isAttacking = false;
    private bool attackAnimationComplete = false;

    private Dictionary<AISimples.stateOfAi, AudioClip> audioClips = new Dictionary<AISimples.stateOfAi, AudioClip>();

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        rb = GetComponent<Rigidbody>();

        // Inicializa o dicion�rio de clipes de �udio
        audioClips[AISimples.stateOfAi.patrolling] = Patrolling;
        audioClips[AISimples.stateOfAi.following] = Following;
        audioClips[AISimples.stateOfAi.attacking] = Attacking;
        audioClips[AISimples.stateOfAi.searchingLostTarget] = SearchingLostTarget;
        audioClips[AISimples.stateOfAi.waiting] = Waiting;
    }

    void Update()
    {
        UpdateAudio();
        UpdateAnimations();
    }

    void UpdateAnimations()
    {
        AISimples.stateOfAi currentState = Navgador.GetCurrentState();

        if (currentState == AISimples.stateOfAi.attacking)
        {
            // Se estiver dentro do alcance de ataque, apenas execute a anima��o de ataque
            isAttacking = true;
        }
        else
        {
            // Fora do alcance de ataque, permita outras anima��es
            isAttacking = false;
            attackAnimationComplete = false; // Redefina a flag de conclus�o da anima��o de ataque
        }

        _animator.SetBool("Attack", isAttacking);
        _animator.SetBool("Searching", currentState == AISimples.stateOfAi.searchingLostTarget);
        _animator.SetBool("Patrolling", currentState == AISimples.stateOfAi.patrolling);
        _animator.SetBool("Following", currentState == AISimples.stateOfAi.following);
        _animator.SetBool("Waiting", currentState == AISimples.stateOfAi.waiting);
    }

    void UpdateAudio()
    {
        AISimples.stateOfAi currentState = Navgador.GetCurrentState();

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




