using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    public int selected;
    [SerializeField]
    public Image[] stars;
    public void Retry()
    {
        FindObjectOfType<SceneTransition>().SceneTrans(SceneManager.GetActiveScene().name);
    }
    public void Levels()
    {
        FindObjectOfType<SceneTransition>().SceneTrans("Levels");
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
            FindObjectOfType<SceneTransition>().SceneTrans("Level" + (level + 1).ToString());
        }
    }
    public void SelectLevel()
    {
        FindObjectOfType<SceneTransition>().SceneTrans("Level" + (selected).ToString());
    }
}
