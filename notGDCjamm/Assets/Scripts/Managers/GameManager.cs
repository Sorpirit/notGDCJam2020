using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Sprite> iceImages;
    public List<Sprite> countDown;
    public Image Cimage;
    public Image image;
    public GameObject Player;
    public GameObject Cpanel;
    public GameObject PanelButtonUI;
    public Animator animator;
    public AudioManager au;


    private CarControls playerCar;

    private void Awake()
    {
        playerCar = Player.GetComponent<CarControls>();
    }

    private void Start()
    {
        PanelButtonUI.SetActive(false);
        playerCar.BlockMovment = true;
        StartCoroutine(wait());
        if (PlayerPrefs.GetInt("music") == 1)
        {
            au.SetSound(true, "music");
        }
        else if (PlayerPrefs.GetInt("music") == 0)
        {
            au.SetSound(false, "music");
        }
        if (PlayerPrefs.GetInt("effects") == 1)
        {
            au.SetSound(true, "countdown");
            au.SetSound(true, "select");
            au.SetSound(true, "collect");
            au.SetSound(true, "finish");
        }
        else if (PlayerPrefs.GetInt("effects") == 0)
        {
            au.SetSound(false, "countdown");
            au.SetSound(false, "select");
            au.SetSound(false, "collect");
            au.SetSound(false, "finish");
        }
    }
    private void Update()
    {
        float x = GlobalTimer.timer.Countdown/4;

        float a = (int)(100 * (x - (int)x));

        float b = (a / 100) * 8;

        image.sprite = iceImages[(int)b];
    }
    public void Pause()
    {
        animator.SetTrigger("openP");
        au.PlaySound("select");
        GlobalTimer.timer.IsTimerRunning = false;
        playerCar.BlockMovment = true;
    }
    public void Unpause()
    {
        au.PlaySound("select");
        animator.SetTrigger("closeP");
        GlobalTimer.timer.IsTimerRunning = true;
        playerCar.BlockMovment = false;
    }
    IEnumerator wait()
    {
        Cimage.gameObject.SetActive(false);
        PanelButtonUI.SetActive(false);
        playerCar.BlockMovment = true;
        yield return new WaitForSeconds(.5f);
        PanelButtonUI.SetActive(false);
        Cimage.gameObject.SetActive(true);
        Cimage.sprite = countDown[0];
        au.PlaySound("countdown");
        yield return new WaitForSeconds(1);
        Cimage.sprite = countDown[1];
        yield return new WaitForSeconds(1);
        Cimage.sprite = countDown[2];
        yield return new WaitForSeconds(1);
        Cimage.sprite = countDown[3];
        playerCar.BlockMovment = false;
        GlobalTimer.timer.IsTimerRunning = true;
        yield return new WaitForSeconds(.5f);
        au.PlaySound("music");
    }

}
