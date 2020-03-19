using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextLevel : MonoBehaviour
{
    public Animator animator;

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
            finCam.SetActive(true);
            animator.SetTrigger("finCam");
            Player.SetActive(false);
        }
    }
}
