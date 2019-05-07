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
}
