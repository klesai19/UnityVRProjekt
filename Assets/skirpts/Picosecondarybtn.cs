using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Picosecondarybtn : MonoBehaviour
{
   [SerializeField] private InputActionReference secondaryAction;

   private void OnEnable()
   {
      secondaryAction?.action.Enable();
   }
   
   private void OnDisable()
   {
      secondaryAction?.action.Disable();
   }

   private void Update()
   {
      if (secondaryAction == null)
      {
         Debug.Log("secondary action not set");
         return;
      }

      if (secondaryAction.action.WasPressedThisFrame())
      {
         Debug.Log("Pico secondary bbtn pressed");
      }
      
   }
}
