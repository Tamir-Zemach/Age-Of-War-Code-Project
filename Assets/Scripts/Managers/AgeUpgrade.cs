using Assets.Scripts.units;
using UnityEngine;

public class AgeUpgrade : PersistentMonoBehaviour<AgeUpgrade>
{
    public int CurrentPlayerAge { get; private set; } = 1;
    public int CurrentEnemyAge { get; private set; } = 1;

    public void AdvanceAge(bool isPlayer)
    {
        var state = isPlayer ? CurrentPlayerAge++ : CurrentEnemyAge++;
    }


    public void UpgradeCoreStats(UnitData unit, UnitLevelUpData levelUpData)
    {
        unit._range += levelUpData._range;
        unit._speed += levelUpData._speed;
        unit._health += levelUpData._health;
        unit._strength += levelUpData._strength;
        unit._initialAttackDelay = Mathf.Max(0.1f, unit._initialAttackDelay - levelUpData._initialAttackDelay);
        unit._unitPrefab = levelUpData._unitPrefab;

    }


}
