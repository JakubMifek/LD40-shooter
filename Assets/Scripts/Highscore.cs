using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

public static class StringExtensions
{

    public static int[] ToIntArray(this string rep)
    {
        return Array.ConvertAll(rep.Split(';'), int.Parse);
    }

    public static string ToStringRep(this int[] array)
    {
        if (array.Length == 0)
            return "";

        StringBuilder sb = new StringBuilder();
        sb.Append(array[0]);
        for (int i = 1; i < array.Length; i++)
            sb.Append(";").Append(array[i]);

        return sb.ToString();
    }
}

public class HighScore : MonoBehaviour
{
    public int[] scores;

    public int highscore;
    public int minscore;
    private int localScore;
    public int LocalScore { get { return started ? localScore : PlayerPrefs.GetInt("lastScore", 0); } set { } }
    private bool started;

    void Start()
    {
        scores = PlayerPrefs.GetString("highscore", "0;0;0;0;0;0;0;0;0;0").ToIntArray();
        highscore = scores.Max();
        minscore = scores.Min();
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        Debug.Log("Setting localscore");
        localScore = PlayerPrefs.GetInt("lastScore", 0);
        started = true;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("lastScore", 0);
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        Debug.Log("scene change");
        if (PlayerPrefs.GetInt("scoreable", 0) == 1)
        {
            Debug.Log("scoreable == 1");
            if (arg1.name == "Menu")
            {
                Debug.Log("menu");
                AddFinalScore(localScore);
                PlayerPrefs.SetInt("lastScore", 0);
            }
            else
            {
                PlayerPrefs.SetInt("lastScore", localScore);
            }
        }
    }

    public void AddScore(int score)
    {
        localScore += score;
        Debug.Log("Score: " + localScore);
    }

    public void Clear()
    {
        localScore = 0;
    }

    public void Reset()
    {
        for (int i = 0; i < scores.Length; i++)
            scores[i] = 0;

        PlayerPrefs.SetString("highscore", scores.ToStringRep());
    }

    public void AddFinalScore(int score)
    {
        Debug.Log("scoring");
        if (score > minscore)
        {
            minscore = score;
            scores[0] = minscore;

            for (int i = 1; i < scores.Length; i++)
            {
                if (scores[i] >= scores[i - 1])
                    break;

                scores[i] ^= scores[i - 1];
                scores[i - 1] ^= scores[i];
                scores[i] ^= scores[i - 1];
            }

            PlayerPrefs.SetString("highscore", scores.ToStringRep());
        }
    }
}