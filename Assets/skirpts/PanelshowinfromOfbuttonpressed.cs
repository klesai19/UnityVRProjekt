using System;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanelshowinfromOfbuttonpressed : MonoBehaviour
{

    [Header("Input Action")] [SerializeField]
    private InputActionReference secondaryAction;

    [Header("UI/panel")] [SerializeField] private GameObject panelObject;

    [Header("positioning on main camera in xr origin")] [SerializeField]
    private Transform playerTransform;
    [SerializeField] private float distanceInFront=1.5f;
    [SerializeField] private float hightOffset = 0.0f;

    private void OnEnable()
    {
        secondaryAction.action.Enable();
    }

    private void OnDisable()
    {
        secondaryAction.action.Disable();
    }

    private void Update()
    {
        if (secondaryAction.action.WasPressedThisFrame())
        {
            panelObject.SetActive(!panelObject.activeSelf);
        }

        if (!panelObject.activeSelf)
        {
            return;
        }

        Vector3 forward = playerTransform.forward;
        forward.y = 0f;
        forward.Normalize();
        Vector3 targetPos = playerTransform.position + forward * distanceInFront;
        targetPos.y += hightOffset;
        panelObject.transform.position = targetPos;
        Vector3 lookdir = panelObject.transform.position - playerTransform.position;
        lookdir.y = 0f;
        //magnitude=x^"*Y^"*Z^
        if (lookdir.magnitude>0.01f)
        {
            panelObject.transform.rotation = Quaternion.LookRotation(lookdir);
        }
    }
}
