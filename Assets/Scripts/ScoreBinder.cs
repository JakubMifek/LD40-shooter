using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class ScoreBinder : MonoBehaviour
{

    public int index;
    public HighScore hs;

    private TextMesh mesh;

    private void Start()
    {
        mesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        mesh.text = hs.scores[index].ToString();
    }
}
