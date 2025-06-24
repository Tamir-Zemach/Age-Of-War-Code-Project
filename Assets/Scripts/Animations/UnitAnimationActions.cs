using Assets.Scripts;
using UnityEngine;

public class UnitAnimationActions : MonoBehaviour
{
    private GameObject _parent;
    private UnitBaseBehaviour UnitBaseBehaviour;

    private void Awake()
    {
        UnitBaseBehaviour = GetComponentInParent<UnitBaseBehaviour>();
        _parent = UnitBaseBehaviour.gameObject;
    }
    public void DestroyObject()
    {
        if (_parent != null)
        {
            Destroy(_parent.gameObject);
        }
    }

    public void AttackInvoke()
    {
        GameObject target = UnitBaseBehaviour.GetAttackTarget();
        if (target == null) return;

        Attacker attacker = UnitBaseBehaviour.GetComponent<Attacker>();
        if (attacker != null)
        {
            attacker.Attack(target);
        }
        Range ranger = UnitBaseBehaviour.GetComponent<Range>();
        if (ranger != null)
        {
            ranger.FireProjectile(target);
        }
    }
}
