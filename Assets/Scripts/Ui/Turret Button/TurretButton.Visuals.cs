﻿using Assets.Scripts.Enems;
using System;

namespace Assets.Scripts.Ui.TurretButton
{
    public partial class TurretButton
    {
        /// <summary>
        /// Applies visual feedback to turret spawn points based on a condition and feedback mode.
        /// </summary>
        /// <remarks>
        /// Use this method for short-term, interaction-based visuals (e.g., during Deploy, Sell, or AddSlot).
        /// For persistent highlighting after turret state changes (like placing or selling a turret),
        /// </remarks>
        /// <param name="condition">Condition to filter which spawn points receive visual feedback.</param>
        /// <param name="mode">The type of visual feedback to apply (Highlight, Flash, or Off).</param>
        /// <param name="flashInterval">Optional interval for flashing mode.</param>
        private void SetVisualFeedback(Func<TurretSpawnPoint, bool> condition,
            VisualFeedbackType mode, float flashInterval = 0.2f)
        {
            foreach (var point in _turretSpawnPoints)
            {
                if (condition(point))
                {
                    switch (mode)
                    {
                        case VisualFeedbackType.Highlight:
                            point.ShowHighlight(true);
                            break;
                        case VisualFeedbackType.Flash:
                            point.StartFlashing(flashInterval);
                            break;
                        case VisualFeedbackType.StopFlash:
                            point.StopFlashing();
                            break;
                        case VisualFeedbackType.Off:
                            point.ShowHighlight(false);
                            point.StopFlashing();
                            break;
                    }
                }
            }
        }

    }
}