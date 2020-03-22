using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Animator animator;
    public AudioManager au;
    public Image musicButton;
    bool bMusic;
    public Image efectsButton;
    bool bEffects;

    private void Start()
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            bMusic = true;
            musicButton.color = new Color32(0, 255, 255, 255);
            au.sounds[0].playOnAwake = true;
        }
        else if (PlayerPrefs.GetInt("music") == 0)
        {
            au.SetSound(false, "menuMusic");
            bMusic = false;
            musicButton.color = new Color32(255, 0, 0, 255);
            au.sounds[0].playOnAwake = false;
        }
        if (PlayerPrefs.GetInt("effects") == 1)
        {
            bEffects = true;
            efectsButton.color = new Color32(0, 255, 255, 255);
        }
        else if (PlayerPrefs.GetInt("effects") == 0)
        {
            au.SetSound(false, "menuConfirm");
            bEffects = false;
            efectsButton.color = new Color32(255, 0, 0, 255);
        }
    }
    public void GoToPlay(int SceneInt)
    {
        au.PlaySound("menuConfirm");
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
        au.PlaySound("menuConfirm");
    }
    public void closeSettings()
    {
        animator.SetTrigger("closeSettings");
        au.PlaySound("menuConfirm");
    }
    public void openCredits()
    {
        animator.SetTrigger("openCredits");
        au.PlaySound("menuConfirm");
    }
    public void closeCredits()
    {
        animator.SetTrigger("closeCredits");
        au.PlaySound("menuConfirm");
    }
    public void openSelect()
    {
        animator.SetTrigger("openSelect");
        au.PlaySound("menuConfirm");
    }
    public void closeSelect()
    {
        animator.SetTrigger("closeSelect");
        au.PlaySound("menuConfirm");
    }
    public void SetMusic()
    {
        if(bMusic)
        {
            au.SetSound(false, "menuMusic");
            PlayerPrefs.SetInt("music", 0);
            musicButton.color = new Color32(255, 0, 0, 255);
            bMusic = false;
        }
        else if(!bMusic)
        {
            au.SetSound(true, "menuMusic");
            PlayerPrefs.SetInt("music", 1);
            musicButton.color = new Color32(0, 255, 255, 255);
            bMusic = true;
        }
    }
    public void SetEffects()
    {
        if (bEffects)
        {
            au.SetSound(false, "menuConfirm");
            PlayerPrefs.SetInt("effects", 0);
            efectsButton.color = new Color32(255, 0, 0, 255);
            bEffects = false;
        }
        else if (!bEffects)
        {
            au.SetSound(true, "menuConfirm");
            PlayerPrefs.SetInt("effects", 1);
            efectsButton.color = new Color32(0, 255, 255, 255);
            bEffects = true;
        }
    }
}
