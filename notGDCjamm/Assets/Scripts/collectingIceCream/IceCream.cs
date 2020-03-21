using UnityEngine;

public class IceCream : MonoBehaviour, ICollectible
{

    [SerializeField] private float bonusTime;

    public void Collect(GameObject grabber)
    {
        if(!grabber.TryGetComponent<EnemyController>(out EnemyController c)) GlobalTimer.timer.Countdown += bonusTime;
        Destroy(gameObject);
    }
}