using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Öffnungswinkel der Tür um die Y-Achse
    private float openAngleY = -120f;

    // Geschwindigkeit der Rotation
    private float rotationSpeed = 3f;

    // Merkt, ob die Tür offen ist
    private bool isOpen;

    // Start- und Zielrotation
    private Quaternion closedRotration;
    private Quaternion openRotation;

    private void Awake()
    {
        // Startrotation speichern (Tür geschlossen)
        closedRotration = transform.rotation;

        // Zielrotation für offene Tür berechnen
        Vector3 eulerAngles = closedRotration.eulerAngles;
        eulerAngles.y += openAngleY;
        openRotation = Quaternion.Euler(eulerAngles);
    }

    private void Update()
    {
        // Je nach Zustand Zielrotation wählen
        Quaternion tragetRotation = isOpen ? openRotation : closedRotration;

        // Sanfte Rotation zur Zielrotation
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            tragetRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    // Wird von der Klinke per Event aufgerufen
    public void toggleDoor()
    {
        Debug.Log("toggle door");
        isOpen = !isOpen;
    }
}