using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseRayCaster : MonoBehaviour
{
    public static MouseRayCaster Instance { get; private set; }

    [SerializeField] private LayerMask raycastLayers;
    [SerializeField] private float raycastDistance = 100f;

    private GameObject currentHover;
    private Camera rayCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        if (rayCamera == null)
        {
            rayCamera = Camera.main;
        }
    }



    private void Update()
    {
        HandleHover();
    }

    private void HandleHover()
    {
        GameObject hitObject = GetHitObject();

        if (hitObject != currentHover)
        {
            currentHover = hitObject;
        }
    }

    public GameObject GetHitObject()
    {
        return GetHit()?.collider.gameObject;
    }
    public RaycastHit? GetHit()
    {
        Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, raycastLayers))
        {
            return hit;
        }
        return null;
    }


}