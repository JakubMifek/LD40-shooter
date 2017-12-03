using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SetSize : MonoBehaviour
{

    public Data data;
    private ParticleSystem ps;

    // Use this for initialization
    void Start()
    {
        if (data == null)
            return;

        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var main = ps.main;
        main.startSizeMultiplier = data.Size;
    }
}
