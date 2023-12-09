using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    [SerializeField] private float tumble;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetRandomAngularVelocity();
    }

    private void SetRandomAngularVelocity()
    {
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
