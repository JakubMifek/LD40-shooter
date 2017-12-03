using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Deletable : MonoBehaviour
{
    private Rigidbody rb;
    public ScoreCounter sc;
    public int score;
    private AudioSource source;
    public AudioClip clip;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.transform.position.y < 0)
        {
            if (sc != null)
                sc.Score += score;

            if (source != null && clip != null)
                StartCoroutine(PlaySoundAndDestroy());
            else Destroy(gameObject);
        }
    }

    IEnumerator PlaySoundAndDestroy()
    {
        source.PlayOneShot(clip, 0.1f);
        yield return new WaitForSeconds(clip.length);
        Destroy(gameObject);
    }

}
