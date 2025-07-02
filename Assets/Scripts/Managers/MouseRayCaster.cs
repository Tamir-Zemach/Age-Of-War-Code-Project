using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseRayCaster : SceneAwareMonoBehaviour<MouseRayCaster>
{
    [SerializeField] private LayerMask raycastLayers;
    [SerializeField] private float raycastDistance = 100f;

    private GameObject currentHover;
    private Camera rayCamera;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void InitializeOnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        rayCamera = rayCamera != null ? rayCamera : Camera.main;
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

    public GameObject GetHitObject() => GetHit()?.collider.gameObject;

    public RaycastHit? GetHit()
    {
        Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green);

        return Physics.Raycast(ray, out RaycastHit hit, raycastDistance, raycastLayers) ? hit : null;
    }
}