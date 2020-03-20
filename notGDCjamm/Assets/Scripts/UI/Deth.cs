using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deth : MonoBehaviour
{
    public GameObject Player;

    public Animator animator;

    public GameObject Panel;

    private void OnEnable()
    {
        gameObject.GetComponent<GlobalTimer>().TimeIsOut += OutOfIce;
    }
    private void OutOfIce()
    {
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        animator.SetInteger("GameState", 1);
        yield return new WaitForSeconds(1.5f);
    }
    private void Start()
    {
        Panel.SetActive(false);
    }
}
