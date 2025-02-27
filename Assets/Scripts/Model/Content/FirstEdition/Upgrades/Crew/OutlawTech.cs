﻿using Ship;
using Upgrade;
using UnityEngine;
using System.Linq;
using Tokens;
using System;

namespace UpgradesList.FirstEdition
{
    public class OutlawTech : GenericUpgrade
    {
        public OutlawTech() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Outlaw Tech",
                UpgradeType.Crew,
                cost: 2,
                restriction: new FactionRestriction(Faction.Scum),
                abilityType: typeof(Abilities.FirstEdition.OutlawTechAbility)
            );
        }        
    }
}

namespace Abilities.FirstEdition
{
    public class OutlawTechAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnMovementFinish += RegisterOutlawTechAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnMovementFinish -= RegisterOutlawTechAbility;
        }

        private void RegisterOutlawTechAbility(GenericShip ship)
        {
            if (BoardTools.Board.IsOffTheBoard(ship)) return;

            if (HostShip.GetLastManeuverColor() == Movement.MovementComplexity.Complex)
            {
                RegisterAbilityTrigger(TriggerTypes.OnMovementFinish, AskAssignFocusToken);
            }
        }

        private void AskAssignFocusToken(object sender, EventArgs e)
        {
            if (!alwaysUseAbility)
            {
                AskToUseAbility(
                    HostUpgrade.UpgradeInfo.Name,
                    AlwaysUseByDefault,
                    AssignToken,
                    descriptionLong: "Do you want to assign 1 Focus Token to your ship?",
                    imageHolder: HostUpgrade,
                    showAlwaysUseOption: true
                );
            }
            else
            {
                HostShip.Tokens.AssignToken(typeof(FocusToken), Triggers.FinishTrigger);
            }
        }

        private void AssignToken(object sender, EventArgs e)
        {
            HostShip.Tokens.AssignToken(typeof(FocusToken), SubPhases.DecisionSubPhase.ConfirmDecision);
        }
    }
}