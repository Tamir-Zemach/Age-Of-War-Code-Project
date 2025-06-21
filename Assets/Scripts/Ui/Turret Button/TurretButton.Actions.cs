using System;
using Assets.Scripts.Enems;
namespace Assets.Scripts.Ui.TurretButton
{
    public partial class TurretButton
    {
        public void DeployTurret()
        {
            ExecuteTurretButtonAction(
                TurretButtonType.DeployTurret,
                VisualFeedbackType.Flash,
                AddTurretToEmptySlotLogic,
                () => PlayerCurrency.Instance.HasEnoughMoney(_cost) && CanDeployTurret());
        }

        public void AddTurretSlot()
        {
            ExecuteTurretButtonAction(
                TurretButtonType.AddSlot,
                VisualFeedbackType.Flash,
                AddSlotLogic,
                () => PlayerCurrency.Instance.HasEnoughMoney(_cost) && CanAddSlot());
        }

        public void SellTurret()
        {
            ExecuteTurretButtonAction(
                TurretButtonType.SellTurret,
                VisualFeedbackType.Flash,
                SellTurretLogic,
                HaveAnyTurrets);
        }

        private bool CanDeployTurret() => _turretSpawnPoints.Exists(p => !p.HasTurret && p.IsUnlocked);
        private bool CanAddSlot() => _turretSpawnPoints.Exists(p => !p.IsUnlocked);
        private bool HaveAnyTurrets() => _turretSpawnPoints.Exists(p => p.HasTurret);

        


        /// Executes a turret action by verifying currency,
        /// applying visual feedback, and waiting for a valid spawn point click.
        private void ExecuteTurretButtonAction(
            TurretButtonType type,
            VisualFeedbackType feedback,
            Action<TurretSpawnPoint> logic,
            Func<bool> currencyCheck)
        {
            if (!currencyCheck()) return;

            var condition = _conditions[type];
            SetVisualFeedback(condition, feedback);
            StartCoroutine(WaitForSlotClick(condition, logic));
        }

    }
}