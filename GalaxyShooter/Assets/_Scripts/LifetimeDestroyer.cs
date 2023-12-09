using UnityEngine;

public class LifetimeDestroyer : MonoBehaviour
{

    [SerializeField] private float lifeTime;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
