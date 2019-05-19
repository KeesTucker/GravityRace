using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }
    public void SceneTrans(string sceneName)
    {
        StartCoroutine(LoadSceneAFterTransition(sceneName));
    }
    private IEnumerator LoadSceneAFterTransition(string sceneName)
    {
        //show animate out animation
        animator.SetBool("animateOut", true);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(sceneName);
    }
}
