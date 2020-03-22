using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class nextLevel : MonoBehaviour
{
    public Animator animator;
    public Animator animator2;
    public GameObject MainCanvasUI;
    public GameObject finCam;
    public GameObject Player;
    public AudioManager au;
    public TextMeshProUGUI tex;

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
        else
        {
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Player.GetComponent<CarControls>().BlockMovment = true;
            animator2.SetTrigger("outOfIce");
            GlobalTimer.timer.IsTimerRunning = false;
            tex.text = "your enemy has delivered the ice cream";
        }
    }
    IEnumerator wait ()
    {
        au.SetSound(false, "music");
        yield return new WaitForSeconds(1);
        au.PlaySound("finish");
    }
}
