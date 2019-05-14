﻿using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    public int selected;
    [SerializeField]
    public Image[] stars;
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }
    public void Next()
    {
        int level = GameObject.Find("LevelManager").GetComponent<LevelManager>().levelNumber;
        if (GameObject.Find("LevelManager").GetComponent<LevelManager>().lastLevel)
        {
            Levels();
        }
        else
        {
            SceneManager.LoadScene("Level" + (level + 1).ToString());
        }
    }
    public void SelectLevel()
    {
        SceneManager.LoadScene("Level" + (selected).ToString());
    }
}