using UnityEngine;

public class DestroyByBorder : MonoBehaviour
{

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
