using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(AudioSource)), RequireComponent(typeof(Collider))]
public class LightEffect : ThrownEffect
{
    public AudioClip throwClip;
    public AudioClip hitClip;
    public GameObject mess;

    private Rigidbody body;
    private Collider col;
    private AudioSource source;

    private bool thrown;

    public override void Throw()
    {
        source.PlayOneShot(throwClip, 1.0f);
        body.AddForce(transform.forward * bonus);
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

        //thrown = false;
        //StartCoroutine(PlaySoundAndDestroy());
        PlaySoundAndDestroy();
    }

    private void PlaySoundAndDestroy()
    {
        source.PlayOneShot(hitClip, 1.0f);
        if (transform.position.y > 13)
            Instantiate(mess, transform.position, Quaternion.identity);

        //yield return new WaitForSeconds(hitClip.length);
        //Destroy(gameObject);
    }
}
