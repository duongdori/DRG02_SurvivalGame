using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSystem : MonoBehaviour
{
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float selectionDistance = 30f;
    [SerializeField] private LayerMask selectionLayer;
    
    private void Update()
    {
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out RaycastHit hitInfo, selectionDistance, selectionLayer))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                hitInfo.transform.GetChild(1).gameObject.SetActive(true);
                Debug.Log(hitInfo.transform.name);
            }
            Debug.DrawLine(rayOrigin.position, hitInfo.point, Color.blue);
        }
        
    }
}
