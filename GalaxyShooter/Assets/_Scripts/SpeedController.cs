using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody rb;

    private void Start()
    {
        InitializeComponents();
        Move();
    }

    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Move()
    {
        rb.velocity = transform.forward * speed;
    }
}
