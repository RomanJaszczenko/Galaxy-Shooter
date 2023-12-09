using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float dodge;
    [SerializeField] private float smoothing;
    [SerializeField] private float tilt;

    [Header("Timing Settings")]
    [SerializeField] private Vector2 startWait;
    [SerializeField] private Vector2 maneuverTime;
    [SerializeField] private Vector2 maneuverWait;

    [Header("Boundary")]
    [SerializeField] private Boundary boundary;

    private GameController gameController;
    private Transform playerTransform;
    private float currentSpeed, targetManeuver;
    private Rigidbody rb;

    private void Start()
    {
        InitializeComponents();
        StartCoroutine(Evade());
    }

    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerTransform = (!gameController.gameOver) ? GameObject.FindGameObjectWithTag("Player").transform : null;
        currentSpeed = rb.velocity.z;
    }

    private IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            CalculateTargetManeuver();
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            ResetTargetManeuver();
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    private void CalculateTargetManeuver()
    {
        if (gameController.hardMode)
        {
            targetManeuver = (playerTransform != null) ? playerTransform.position.x : transform.position.x;
        }
        else
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
        }
    }

    private void ResetTargetManeuver()
    {
        targetManeuver = 0;
    }

    private void FixedUpdate()
    {
        MoveShip();
        ApplyBoundary();
        ApplyRotation();
    }

    private void MoveShip()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
    }

    private void ApplyBoundary()
    {
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
    }

    private void ApplyRotation()
    {
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
