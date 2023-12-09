using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    [SerializeField] private GameObject shot;
    [SerializeField] private Transform shotSpawn;
    [SerializeField] private float fireRate;
    [SerializeField] private float delay;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating(nameof(Fire), delay, fireRate);
    }

    private void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
