using UnityEngine;

public class WormController : MonoBehaviour
{

    [Header("Movement")]

    [SerializeField] private Transform wormVisual;

    [SerializeField] private float upHeight = 0.35f;

    [SerializeField] private float speed = 2.0f; 

    [SerializeField] private float waitMin = 0.3f;

    [SerializeField] private float waitMax = 1.0f;

    private Vector3 baseLocalPos;

    private bool goingUp;

    private float waitTimer;

    private void Awake()

    {

        if (wormVisual == null) wormVisual = transform;

        baseLocalPos = wormVisual.localPosition;

        goingUp = false;

        waitTimer = 0f;

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
            waitTimer = Random.Range(waitMin, waitMax);

        }

    }

}
 