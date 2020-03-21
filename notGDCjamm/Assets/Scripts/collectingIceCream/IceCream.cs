using UnityEngine;

public class IceCream : MonoBehaviour, ICollectible
{

    [SerializeField] private float bonusTime;

    public void Collect()
    {
        GlobalTimer.timer.Countdown += bonusTime;
        Destroy(gameObject);
    }
}