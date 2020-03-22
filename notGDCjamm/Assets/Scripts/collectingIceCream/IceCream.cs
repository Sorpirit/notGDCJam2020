using UnityEngine;

public class IceCream : MonoBehaviour, ICollectible
{

    [SerializeField] private float bonusTime;

    public void Collect()
    {
        GlobalTimer.timer.Countdown += bonusTime;
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().PlaySound("collect");
        Destroy(gameObject);
    }
}