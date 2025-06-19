using UnityEngine;

[RequireComponent(typeof(TurretBaseBehavior))]

public class TurretDebugger : MonoBehaviour
{
    private TurretBaseBehavior TurretBaseBehavior;

    [SerializeField] private Color _boxColor = Color.red;

    private void OnDrawGizmos()
    {
        if (TurretBaseBehavior == null)
            TurretBaseBehavior = GetComponent<TurretBaseBehavior>();

        Gizmos.color = _boxColor;

        // Perform the BoxCast
        if (Physics.BoxCast(TurretBaseBehavior.Origin,
            TurretBaseBehavior.BoxSize, 
            TurretBaseBehavior.Direction, 
            out RaycastHit hitInfo, 
            TurretBaseBehavior.BoxCastOrigin.rotation, 
            TurretBaseBehavior.Range,
            TurretBaseBehavior.OppositeUnitLayer))
        {
            // Draw the hit box
            Gizmos.DrawWireCube(hitInfo.point, TurretBaseBehavior.BoxSize);
        }

        // Draw the initial box
        Gizmos.DrawWireCube(TurretBaseBehavior.Origin, TurretBaseBehavior.BoxSize);

        // Draw the movement path
        Gizmos.DrawLine(TurretBaseBehavior.Origin, 
            TurretBaseBehavior.Origin + TurretBaseBehavior.Direction * TurretBaseBehavior.Range);

    }

}
