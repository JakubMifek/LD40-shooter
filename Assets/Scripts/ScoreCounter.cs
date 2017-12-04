using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class ScoreCounter : MonoBehaviour
{
    private TextMesh mesh;

    public HighScore score;

    private int _score;
    public int Score
    {
        get { return _score; }
        set
        {
            var last = _score;
            _score = value;

            score.AddScore(_score - last);
            mesh.text = "Score: " + _score;
        }
    }

    // Use this for initialization
    void Start()
    {
        mesh = GetComponent<TextMesh>();
        Debug.Log("Setting score: " + score.LocalScore);
        Score = score.LocalScore;
    }
}
