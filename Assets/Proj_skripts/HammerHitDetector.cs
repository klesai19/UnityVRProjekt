using UnityEngine;

public class HammerHitDetector : MonoBehaviour

{

    [SerializeField] private float minHitSpeed = 1.2f;   // wie “hart” schlagen muss

    [SerializeField] private int pointsPerHit = 1;

    private Rigidbody hammerRb;

    private void Awake()

    {

        // Rigidbody ist am Parent (Hammer)

        hammerRb = GetComponentInParent<Rigidbody>();

    }

    private void OnTriggerEnter(Collider other)

    {

        if (GameManager.Instance == null) return;

        if (GameManager.Instance.State != GameManager.GameState.Running) return;

        if (!other.TryGetComponent<WormTarget>(out var worm)) return;

        // Schlag nur zählen, wenn der Hammer wirklich in Bewegung ist

        float speed = hammerRb != null ? hammerRb.linearVelocity.magnitude : 0f;

        if (speed < minHitSpeed) return;

        worm.TryHit(pointsPerHit);

    }

}