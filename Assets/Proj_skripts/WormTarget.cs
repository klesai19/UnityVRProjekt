using UnityEngine;

public class WormTarget : MonoBehaviour

{

    [Header("Hit Rules")]

    [SerializeField] private float minUpOffsetToBeHittable = 0.15f;
    [SerializeField] private float hitCooldown = 0.35f;
    private float cooldownTimer;
    private Vector3 baseLocalPos;
    private void Start()
    {
        baseLocalPos = transform.localPosition;
    }
    private void Update()
    {
        if (cooldownTimer > 0f) cooldownTimer -= Time.deltaTime;
    }

    public void TryHit(int points)
    {
        if (cooldownTimer > 0f) return;
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.State != GameManager.GameState.Running) return;
        float upOffset = transform.localPosition.y - baseLocalPos.y;
        if (upOffset < minUpOffsetToBeHittable) return;
        cooldownTimer = hitCooldown;
        WormHitEvents.Raise(points);
        if (TryGetComponent<WormController>(out var controller))
        {
            controller.OnHit();
        }
        if (SoundManager.Instance!=null)
        {
           SoundManager.Instance.PlayWormHit(); 
        }
    }
}

