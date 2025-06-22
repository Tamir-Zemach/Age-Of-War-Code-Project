using Assets.Scripts;
using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    private UnitBaseBehaviour UnitBaseBehaviour;
    private UnitHealthManager UnitHealthManager;

    private Animator _animator;
    private GameObject _parent;
    private void Awake()
    {
        UnitBaseBehaviour = GetComponentInParent<UnitBaseBehaviour>();
        UnitHealthManager = GetComponentInParent<UnitHealthManager>();
        if (UnitHealthManager != null)
        {
            UnitHealthManager.OnDying += PlayDeathAnimation;
        }
        _animator = GetComponent<Animator>();
        _parent = UnitBaseBehaviour.gameObject;
        UnitBaseBehaviour.OnInitialized += GetAnimationClips;
    }
    private void GetAnimationClips()
    {
        if (_animator == null || _animator.runtimeAnimatorController == null)
        {
            Debug.LogWarning("Animator or RuntimeAnimatorController is missing.");
            return;
        }

        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Attack")
            {
                float length = clip.length;
                _animator.speed = length > 0 ? 1f / UnitBaseBehaviour.Unit._initialAttackDelay : 1f;
                break;
            }
        }
    }

    private void Update()
    {
        Animations();
    }
    private void Animations()
    {
        _animator.SetBool("isAttacking", UnitBaseBehaviour.IsAttacking);
        
    }

    private void PlayDeathAnimation()
    {
        _animator.SetTrigger("isDying");
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
