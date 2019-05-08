using UnityEngine.SceneManagement;
using UnityEngine;

public class Navigation : MonoBehaviour
{
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
}
