using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(AudioSource))]
public class HeavyEffect : ThrownEffect
{
    private AudioSource source;
    public AudioClip throwClip;

    private Rigidbody body;

    public override void Throw()
    {
        source.PlayOneShot(throwClip, 1.0f);
        body.mass += bonus;
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }
}
