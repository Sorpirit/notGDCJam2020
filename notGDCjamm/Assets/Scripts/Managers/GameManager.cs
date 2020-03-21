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

    private void Start()
    {
        PanelButtonUI.SetActive(false);
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(wait());
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
        GlobalTimer.timer.IsTimerRunning = false;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
    public void Unpause()
    {
        animator.SetTrigger("closeP");
        GlobalTimer.timer.IsTimerRunning = true;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
    IEnumerator wait()
    {
        Cimage.gameObject.SetActive(false);
        PanelButtonUI.SetActive(false);
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(.5f);
        PanelButtonUI.SetActive(false);
        Cimage.gameObject.SetActive(true);
        Cimage.sprite = countDown[0];
        yield return new WaitForSeconds(1);
        Cimage.sprite = countDown[1];
        yield return new WaitForSeconds(1);
        Cimage.sprite = countDown[2];
        yield return new WaitForSeconds(1);
        Cimage.sprite = countDown[3];
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GlobalTimer.timer.IsTimerRunning = true;
    }

}
