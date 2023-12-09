using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float tileSizeZ;

    private Vector3 startPosition;

    private void Start()
    {
        CacheStartPosition();
    }

    private void CacheStartPosition()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        ScrollBackground();
    }

    private void ScrollBackground()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
