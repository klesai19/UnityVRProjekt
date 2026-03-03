using UnityEngine;

public class WormController : MonoBehaviour
{

    [Header("Movement")]

    [SerializeField] private Transform wormVisual;
    [SerializeField] private float upHeight = 0.35f;
    [SerializeField] private float speed = 2.0f; 
    [SerializeField] private float waitMin = 0.3f;
    [SerializeField] private float waitMax = 1.0f;
    [SerializeField] private float hitPauseDuration = 5f;
    private float randomStartDelay;
    private bool isHitPaused = false;
    private float hitPauseTimer = 0f;
    private Vector3 baseLocalPos;

    private bool goingUp;

    private float waitTimer;

    private void Awake()

    {

        if (wormVisual == null) wormVisual = transform;

        baseLocalPos = wormVisual.localPosition;
        goingUp = false;
        waitTimer = 0f;
        randomStartDelay = Random.Range(0f, 2.5f);
        waitTimer = randomStartDelay;

    }

    private void Update()

    {

        if (GameManager.Instance == null) return;
        var state = GameManager.Instance.State;
        if (state == GameManager.GameState.Idle)
        {
            wormVisual.localPosition = baseLocalPos;
            return;
        }
        if (state == GameManager.GameState.Ended)
        {
            return;
        }

        if (isHitPaused)
        {
            hitPauseTimer -= Time.deltaTime;
            wormVisual.localPosition = baseLocalPos;
            if (hitPauseTimer<=0f)
            {
                isHitPaused = false;
                goingUp = false;
                waitTimer = 0f;
            }
            return;
        }
        RunMovement();
    }
    private void RunMovement()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        float targetY = baseLocalPos.y + (goingUp ? upHeight : 0f);
        Vector3 target = new Vector3(baseLocalPos.x, targetY, baseLocalPos.z);
        wormVisual.localPosition = Vector3.MoveTowards(
            wormVisual.localPosition,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(wormVisual.localPosition, target) < 0.001f)

        {
            goingUp = !goingUp;
            waitTimer = Random.Range(waitMin, waitMax)+ Random.Range(0.3f,1.2f);

        }

    }

    public void OnHit()
    {
        isHitPaused = true;
        hitPauseTimer = hitPauseDuration;
        wormVisual.localPosition = baseLocalPos;
    }

}
 