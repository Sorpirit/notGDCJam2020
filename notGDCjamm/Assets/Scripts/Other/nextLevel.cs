using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextLevel : MonoBehaviour
{
    public Animator animator;
    public GameObject MainPanel;
    public GameObject finCam;
    public GameObject Player;

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
            MainPanel.SetActive(false);
            GlobalTimer.timer.IsTimerRunning = false;
        }
    }
}
