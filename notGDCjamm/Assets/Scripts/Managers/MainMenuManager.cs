using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Animator animator;

    public void GoToPlay(int SceneInt)
    {
        StartCoroutine(wait(SceneInt));
    }
    IEnumerator wait(int i)
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(.30f);
        SceneManager.LoadScene(sceneBuildIndex: i+1);
    }
    public void openSettings()
    {
        animator.SetTrigger("openSettings");
    }
    public void closeSettings()
    {
        animator.SetTrigger("closeSettings");
    }
    public void openCredits()
    {
        animator.SetTrigger("openCredits");
    }
    public void closeCredits()
    {
        animator.SetTrigger("closeCredits");
    }
}
