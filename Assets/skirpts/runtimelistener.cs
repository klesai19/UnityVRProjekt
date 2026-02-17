using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class runtimelistener : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    private void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }

    private void OnEnable()
    {
        if(socket==null) return;
        socket.selectEntered.AddListener((OnObjectinserted));
        socket.selectExited.AddListener(OnObjectremoved);
    }
    
    private void OnDisable()
    {
        if(socket==null) return;
        socket.selectEntered.RemoveListener((OnObjectinserted));
        socket.selectExited.RemoveListener(OnObjectremoved);
    }

    private void OnObjectinserted(SelectEnterEventArgs args)
    {
        Debug.Log("Socket Inerted: ");
    }
    
    
    private void OnObjectremoved(SelectExitEventArgs args)
    {
        Debug.Log("Socket removed: "+ args.interactableObject.transform.name);
    }
}