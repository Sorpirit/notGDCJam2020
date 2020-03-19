using UnityEngine;

public class catAnime : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            animator.SetInteger("Side", 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetInteger("Side", 2);
        }
        else
        {
            animator.SetInteger("Side", 0);
        }
    }
}
