using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXScript : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem = null;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = transform.parent.rotation;
        transform.position = transform.parent.position;
    }

    void Upgrade()
    {
        particleSystem.Play();
    }
}
