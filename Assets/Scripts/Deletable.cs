using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(AudioSource)), RequireComponent(typeof(Collider))]
public class Deletable : MonoBehaviour
{
    private Rigidbody rb;
    public ScoreCounter sc;
    public int score;
    private AudioSource source;
    public AudioClip clip;
    public AudioClip stoneHit;
    private bool deleted = false;
    public LevelInfo info;
    private Collider collider;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if (score == 1)
        {
            Debug.Log("Score hit");
            source.PlayOneShot(stoneHit, 0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.transform.position.y < 0 && !deleted)
        {
            deleted = true;

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
        if (info != null)
            info.targetsDestroyed++;
    }

}
