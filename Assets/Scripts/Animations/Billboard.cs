using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        // Make the object face the camera
        transform.forward = Camera.main.transform.forward;
    }
}