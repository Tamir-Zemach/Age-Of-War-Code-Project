namespace Assets.Scripts
{
    using UnityEngine;
    using System.Collections;

    [RequireComponent(typeof(Character))]

    public class Attacker : MonoBehaviour
    {
        private Character _character; 

        [SerializeField] private GameObject _attackTriggerArea;


        private void Awake()
        {
            _attackTriggerArea.SetActive(false);
            _character = GetComponent<Character>(); // Attacker manages a Character component
            if (_character != null)
            {
               _character.OnAttack += Attack;
               _character.OnAttackCancel += AttackCancel;
            }

        }


        private void Attack()
        {
            _attackTriggerArea.SetActive(true);
        }
        private void AttackCancel()
        {
            _attackTriggerArea.SetActive(false);
        }

    }
}