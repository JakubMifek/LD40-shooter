using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class ScoreBinder : MonoBehaviour
{

    public int index;

    private TextMesh mesh;

    private void Start()
    {
        mesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        mesh.text = HighScore.scores[index].ToString();
    }
}
