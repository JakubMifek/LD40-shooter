using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSwitch : MonoBehaviour
{

    public string level;

    private void Start()
    {
        var but = GetComponent<Button>();
    }

    public void Switch()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Debug.Log("menu was on");
            if (level != "Ranked")
            {
                PlayerPrefs.SetInt("scoreable", 0);
            }
            else
            {
                PlayerPrefs.SetInt("scoreable", 1);
                level = "Level 0";
            }
        }

        SceneManager.LoadScene(level);
    }
}
