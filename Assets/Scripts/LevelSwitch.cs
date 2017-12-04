using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSwitch : MonoBehaviour {

    public string level;

    private void Start()
    {
        var but = GetComponent<Button>();
    }

    public void Switch()
    {
        if (level != "Ranked")
            PlayerPrefs.SetInt("scoreable", 0);
        else
            PlayerPrefs.SetInt("scoreable", 1);

        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }
}
