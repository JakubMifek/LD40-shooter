using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Explosion : ThrownEffect
{
    private bool thrown = false;

    public float radius = 5.0f;
    public float force = 2.0f;

    private Collider col;

    public override void Throw()
    {
        thrown = true;
    }

    private void Start()
    {
        col = GetComponent<Collider>();
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

        Destroy(gameObject);
    }
}
