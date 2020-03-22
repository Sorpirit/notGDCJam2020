using UnityEngine;

public class CarCollector : MonoBehaviour {
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<ICollectible>(out ICollectible collectible))
        {
            collectible.Collect(gameObject);
        }
    }

}