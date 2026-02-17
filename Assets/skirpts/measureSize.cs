using UnityEngine;

public class measureSize : MonoBehaviour
{
   void Start()
       {
           MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
           if (meshRenderer != null)
           {
               Bounds bounds = meshRenderer.bounds;
               Debug.Log($"Bounds Center: {bounds.center}, Size: {bounds.size}");
           }
           else
           {
               Debug.LogWarning("Kein MeshRenderer gefunden!");
           }
       }
   }