using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Boundary
{
    public float xMin = -6.0f, xMax = 6.0f, zMin = -4.0f, zMax = 8.0f;
}

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float tilt = 5.0f;
    [SerializeField] private Boundary boundary;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject shot;
    [SerializeField] private Transform shotSpawn;
    [SerializeField] private float fireRate = 0.5f;

    private Rigidbody rb;
    private AudioSource audioSource;
    private float nextFireTime;
    private bool canMove = false;

    // Stored inclination angle
    private float currentTiltAngle = 0.0f;

    private void Start()
    {
        InitializeComponents();
        nextFireTime = Time.time;
    }

    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
            HandleShooting();
        }

        if (Input.GetMouseButton(0))
        {
            canMove = true;
            Debug.Log("click");
        }
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPosition = GetTouchPosition();

            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            targetPosition.y = 0.0f;
            targetPosition.z += 1f;

            rb.position = Vector3.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);

            ClampPosition();
            ApplyRotation();
        }
    }

    private Vector3 GetTouchPosition()
    {
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }
        else
        {
            return Input.mousePosition;
        }
    }

    private void ClampPosition()
    {
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
    }

    private void ApplyRotation()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        currentTiltAngle = moveHorizontal * -tilt;
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, currentTiltAngle);
    }

    private void HandleShooting()
    {
        if (CanFire())
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    private bool CanFire()
    {
        return Time.time > nextFireTime;
    }

    private void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
