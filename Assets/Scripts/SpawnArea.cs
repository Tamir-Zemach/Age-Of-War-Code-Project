using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [Tooltip("The layer of the Friendly Units")]
    [SerializeField] private LayerMask _friendlyUnitLayer;
    private BoxCollider collider;
    public bool _hasUnitInside {  get; private set; }

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
       
    }

    private void Update()
    {
        _hasUnitInside = Physics.CheckBox(transform.TransformPoint(collider.center), collider.size , collider.transform.rotation, _friendlyUnitLayer);
    }


}
