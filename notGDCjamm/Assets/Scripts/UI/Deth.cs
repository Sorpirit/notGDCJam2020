using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deth : MonoBehaviour
{
    public GameObject Player;

    public Animator animator;

    public GameObject Panel;

    bool done;

    private void OnEnable()
    {
        gameObject.GetComponent<GlobalTimer>().TimeIsOut += OutOfIce;
    }
    private void OutOfIce()
    {
        if(done)
        {
            Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            animator.SetTrigger("outOfIce");
            done = false;
        }
        //StartCoroutine(wait());
    }
    /*IEnumerator wait()
    {
        animator.SetInteger("GameState", 1);
        yield return new WaitForSeconds(1.5f);
    }*/
    private void Start()
    {
        done = true;
        Panel.SetActive(false);
    }
}
