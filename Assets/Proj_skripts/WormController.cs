using UnityEngine;

public class WormController : MonoBehaviour
{

    [Header("Movement")]

    [SerializeField] private Transform wormVisual;   // das Teil das hoch/runter fährt (kann auch this sein)

    [SerializeField] private float upHeight = 0.35f; // wie weit hoch

    [SerializeField] private float speed = 2.0f;     // wie schnell

    [SerializeField] private float waitMin = 0.3f;   // zufällige Pause oben/unten

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

        // A3: Idle/Ended => still stehen

        if (GameManager.Instance == null) return;

        var state = GameManager.Instance.State;

        if (state == GameManager.GameState.Idle)

        {

            // Score wird im GameManager beim StartRound auf 0 gesetzt

            // Wurm steht unten und bewegt sich NICHT

            wormVisual.localPosition = baseLocalPos;

            return;

        }

        if (state == GameManager.GameState.Ended)

        {

            // Stoppe genau dort, wo er ist (oder unten – je nachdem was du willst)

            return;

        }

        // Running => bewegen

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

        // wenn Ziel erreicht: Richtung wechseln + kurze Pause

        if (Vector3.Distance(wormVisual.localPosition, target) < 0.001f)

        {

            goingUp = !goingUp;

            waitTimer = Random.Range(waitMin, waitMax);

        }

    }

}
 