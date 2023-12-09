using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject playerExplosion;
    [SerializeField] private int scoreValue;

    private GameController gameController;

    private void Start()
    {
        FindGameController();
    }

    private void FindGameController()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.LogError("Cannot find 'GameController' script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ShouldIgnoreCollision(other))
        {
            return;
        }

        HandleExplosion();
        HandlePlayerExplosion(other);
        HandleScoreAndDestroy(other.gameObject);
        Destroy(gameObject);
    }

    private bool ShouldIgnoreCollision(Collider other)
    {
        return other.CompareTag("Boundary") || other.CompareTag("Enemy");
    }

    private void HandleExplosion()
    {
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

    private void HandlePlayerExplosion(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController?.GameOver();
        }
    }

    private void HandleScoreAndDestroy(GameObject otherGameObject)
    {
        gameController?.AddScore(scoreValue);
        Destroy(otherGameObject);
    }
}
