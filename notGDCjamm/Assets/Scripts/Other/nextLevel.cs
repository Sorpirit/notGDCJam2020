using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class nextLevel : MonoBehaviour
{
    public Animator animator;
    public GameObject MainCanvasUI;
    public GameObject finCam;
    public GameObject Player;
    public AudioManager au;

    private void Start()
    {
        finCam.SetActive(false);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player)
        {
            Player.SetActive(false);
            finCam.SetActive(true);
            animator.SetTrigger("finCam");
            MainCanvasUI.SetActive(false);
            GlobalTimer.timer.IsTimerRunning = false;
            StartCoroutine(wait());
        }
    }
    IEnumerator wait ()
    {
        au.SetSound(false, "music");
        yield return new WaitForSeconds(1);
        au.PlaySound("finish");
    }
}
