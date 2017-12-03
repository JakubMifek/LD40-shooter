using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text;

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
    public static int[] scores;

    public static int highscore;
    public static int minscore;
    
    void Start()
    {
        scores = PlayerPrefs.GetString("highscore", "0;0;0;0;0;0;0;0;0;0").ToIntArray();
        highscore = scores.Max();
        minscore = scores.Min();
    }

    public void AddScore(int score)
    {
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