using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(AudioSource))]
public class Explosion : ThrownEffect
{
    private bool thrown = false;

    public float radius = 5.0f;
    public float force = 2.0f;
    public GameObject explosion;
    private AudioSource source;
    public AudioClip explosionClip;
    public AudioClip throwClip;

    private Collider col;

    public override void Throw()
    {
        thrown = true;
        source.PlayOneShot(throwClip, 1.0f);
    }

    private void Start()
    {
        col = GetComponent<Collider>();
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!thrown)
            return;

        var colliders = Physics.OverlapSphere(transform.position, radius * bonus);

        foreach (var c in colliders)
        {
            var rb = c.GetComponent<Rigidbody>();
            if (rb == null)
                continue;

            rb.AddExplosionForce(force * bonus, transform.position, radius * bonus, .5f, ForceMode.Impulse);
        }

        var data = explosion.GetComponent<Data>();
        if (data != null)
            data.Size = bonus;

        var expl = Instantiate(explosion, transform.position, Quaternion.identity);
        thrown = false;
        StartCoroutine(PlaySoundAndDestroy());
    }

    private IEnumerator PlaySoundAndDestroy()
    {
        source.PlayOneShot(explosionClip, 1.0f);
        yield return new WaitForSeconds(explosionClip.length);
        Destroy(gameObject);
    }
}
