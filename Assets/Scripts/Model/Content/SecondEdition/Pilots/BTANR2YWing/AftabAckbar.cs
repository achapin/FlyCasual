﻿using ActionsList;
using Ship;
using System;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.BTANR2YWing
    {
        public class AftabAckbar : BTANR2YWing
        {
            public AftabAckbar() : base()
            {
                PilotInfo = new PilotCardInfo
                (
                    "Aftab Ackbar",
                    2,
                    34,
                    extraUpgradeIcon: UpgradeType.Talent,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.AftabAckbarAbility)
                );

                ImageUrl = "https://i.imgur.com/j59sl9p.png";
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class AftabAckbarAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnMovementFinish += CheckMovementAbility;
            HostShip.OnActionIsPerformed += CheckActionAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnMovementFinish -= CheckMovementAbility;
            HostShip.OnActionIsPerformed -= CheckActionAbility;
        }

        private void CheckMovementAbility(GenericShip ship)
        {
            if (ship.AssignedManeuver != null
                && ship.AssignedManeuver.ColorComplexity == Movement.MovementComplexity.Complex
                && ship.AssignedManeuver.IsBasicManeuver
                && ship.Tokens.CountTokensByType<Tokens.StressToken>() == 1
            )
            {
                RegisterAbilityTrigger(TriggerTypes.OnMovementFinish, AskToGetStrainToken);
            }
        }

        private void CheckActionAbility(GenericAction action)
        {
            if (action.IsRed
                && action.HostShip.Tokens.CountTokensByType<Tokens.StressToken>() == 1
            )
            {
                RegisterAbilityTrigger(TriggerTypes.OnMovementFinish, AskToGetStrainToken);
            }
        }

        private void AskToGetStrainToken(object sender, EventArgs e)
        {
            AskToUseAbility
            (
                HostShip.PilotInfo.PilotName,
                AlwaysUseByDefault,
                GetStrainTokenInstead,
                descriptionLong: "Do you want to gain 1 Strain token to remove 1 Stress token?",
                imageHolder: HostShip,
                requiredPlayer: HostShip.Owner.PlayerNo
            );
        }

        private void GetStrainTokenInstead(object sender, EventArgs e)
        {
            SubPhases.DecisionSubPhase.ConfirmDecisionNoCallback();

            HostShip.Tokens.AssignToken(typeof(Tokens.StrainToken), RemoveStressToken);
        }

        private void RemoveStressToken()
        {
            HostShip.Tokens.RemoveToken(typeof(Tokens.StressToken), Triggers.FinishTrigger);
        }
    }
}
