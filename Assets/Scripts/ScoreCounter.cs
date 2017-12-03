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
            _score = value;

            mesh.text = "Score: " + _score;
        }
    }

    // Use this for initialization
    void Start()
    {
        mesh = GetComponent<TextMesh>();
    }

    private void OnDestroy()
    {
        score.AddScore(Score);
    }
}
