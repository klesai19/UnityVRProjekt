using UnityEngine;

public class kreis : MonoBehaviour
{
    [Header("Handle Rotation")]
    // Winkel, um den die Klinke gedrückt wird
    [SerializeField] private float pressAngleZ = 30f;

    // Geschwindigkeit der Bewegung
    [SerializeField] private float pressSpeed = 6f;

    // Start- und Zielrotation
    private Quaternion startRotation;
    private Quaternion pressedRotation;

    // Merkt, ob die Klinke gedrückt wurde
    private bool isPressed;

    private void Awake()
    {
        // Startrotation der Klinke speichern
        startRotation = transform.localRotation;

        // Zielrotation: nur Z-Achse verändern
        pressedRotation = startRotation * Quaternion.Euler(0, 0, pressAngleZ);
    }

    private void Update()
    {
        if (!isPressed) return;

        // Sanfte Rotation zur gedrückten Position
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            pressedRotation,
            Time.deltaTime * pressSpeed
        );
    }

    // Wird durch den Poke-Interactor ausgelöst
    public void PressHandle()
    {
        isPressed = true;
    }
}