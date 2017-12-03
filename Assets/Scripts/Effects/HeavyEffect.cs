using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(AudioSource)), RequireComponent(typeof(Collider))]
public class HeavyEffect : ThrownEffect
{
    public AudioClip throwClip;
    public AudioClip hitClip;

    private Rigidbody body;
    private Collider col;
    private AudioSource source;

    private bool thrown;

    public override void Throw()
    {
        source.PlayOneShot(throwClip, 1.0f);
        body.mass += bonus;
        thrown = true;
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!thrown)
            return;

        thrown = false;
        StartCoroutine(PlaySoundAndDestroy());
    }

    private IEnumerator PlaySoundAndDestroy()
    {
        source.PlayOneShot(hitClip, 1.0f);
        yield return new WaitForSeconds(hitClip.length);
        //Destroy(gameObject);
    }
}
