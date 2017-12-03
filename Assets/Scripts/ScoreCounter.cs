using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class ScoreCounter : MonoBehaviour
{
    private TextMesh mesh;

    private int _score;
    public int Score
    {
        get { return _score; }
        set
        {
            var last = _score;
            _score = value;

            HighScore.AddPoints(_score - last);
            mesh.text = "Score: " + _score;
        }
    }

    // Use this for initialization
    void Start()
    {
        mesh = GetComponent<TextMesh>();
    }
}
