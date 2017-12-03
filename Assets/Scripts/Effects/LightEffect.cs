using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(AudioSource))]
public class LightEffect : ThrownEffect
{

    private bool thrown = false;

    public float radius = 5.0f;
    public float force = 2.0f;
    private AudioSource source;
    public AudioClip throwClip;

    private Rigidbody body;

    public override void Throw()
    {
        thrown = true;
        source.PlayOneShot(throwClip, 1.0f);
        body.AddForce(transform.forward * bonus);
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }
}
