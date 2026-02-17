using UnityEngine;

public class HammerHitDetector : MonoBehaviour

{

    [SerializeField] private float minHitSpeed = 1.2f;
    [SerializeField] private int pointsPerHit = 1;
    private Rigidbody hammerRb;
    private void Awake()
    {
        hammerRb = GetComponentInParent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.State != GameManager.GameState.Running) return;
        if (!other.TryGetComponent<WormTarget>(out var worm)) return;
        float speed = hammerRb != null ? hammerRb.linearVelocity.magnitude : 0f;
        if (speed < minHitSpeed) return;
        worm.TryHit(pointsPerHit);
    }
}