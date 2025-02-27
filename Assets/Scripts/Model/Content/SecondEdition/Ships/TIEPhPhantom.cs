﻿using System.Collections;
using System.Collections.Generic;
using Movement;
using ActionsList;
using Actions;
using Arcs;
using Upgrade;
using Ship;
using Tokens;

namespace Ship
{
    namespace SecondEdition.TIEPhPhantom
    {
        public class TIEPhPhantom : FirstEdition.TIEPhantom.TIEPhantom, TIE
        {
            public TIEPhPhantom() : base()
            {
                ShipInfo.ShipName = "TIE/ph Phantom";

                ShipInfo.ArcInfo = new ShipArcsInfo(ArcType.Front, 3);
                ShipInfo.Hull = 3;

                ShipInfo.UpgradeIcons.Upgrades.Remove(UpgradeType.Crew);
                ShipInfo.UpgradeIcons.Upgrades.Add(UpgradeType.Gunner);
                
                ShipAbilities.Add(new Abilities.SecondEdition.StygiumArray());

                IconicPilots[Faction.Imperial] = typeof(Echo);

                DialInfo.AddManeuver(new ManeuverHolder(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Bank), MovementComplexity.Normal);
                DialInfo.AddManeuver(new ManeuverHolder(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Bank), MovementComplexity.Normal);
                
                ManeuversImageUrl = "https://vignette.wikia.nocookie.net/xwing-miniatures-second-edition/images/4/44/Maneuver_tie_phantom.png";
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class StygiumArray : GenericAbility
    {
        public override string Name { get { return "Stygium Array (ID:" + HostShip.ShipId + ")"; } }

        public override void ActivateAbility()
        {
            HostShip.OnDecloak += RegisterPerformFreeEvadeAction;
            Phases.Events.OnEndPhaseStart_Triggers += RegisterCloakAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnDecloak -= RegisterPerformFreeEvadeAction;
            Phases.Events.OnEndPhaseStart_Triggers -= RegisterCloakAbility;
        }

        private void RegisterPerformFreeEvadeAction()
        {
            RegisterAbilityTrigger(TriggerTypes.OnDecloak, ProposeFreeEvadeAction);
        }

        private void ProposeFreeEvadeAction(object sender, System.EventArgs e)
        {
            HostShip.AskPerformFreeAction(
                new EvadeAction() { HostShip = HostShip },
                Triggers.FinishTrigger,
                descriptionShort: Name,
                descriptionLong: "After you decloak, you may perform an Evade action"
            );
        }

        private void RegisterCloakAbility()
        {
            if (HostShip.Tokens.HasToken<EvadeToken>() && !(HostShip.Tokens.HasToken<CloakToken>()))
            {
                RegisterAbilityTrigger(TriggerTypes.OnEndPhaseStart, AskToCloak);
            }
        }

        private void AskToCloak(object sender, System.EventArgs e)
        {
            Selection.ChangeActiveShip(HostShip);

            AskToUseAbility(
                "Stygium Array",
                NeverUseByDefault,
                TradeEvadeForCloakToken,
                descriptionLong: "Do you want to spend an Evade Token to gain a Cloak Token?"
            );
        }

        private void TradeEvadeForCloakToken(object sender, System.EventArgs e)
        {
            SubPhases.DecisionSubPhase.ConfirmDecisionNoCallback();

            if (HostShip.Tokens.HasToken<EvadeToken>())
            {
                HostShip.Tokens.RemoveToken(typeof(EvadeToken), AssignCloakToken);
            }
            else
            {
                Messages.ShowError(HostShip.PilotInfo.PilotName + " doesn't have any Evade token to spend!");
                Triggers.FinishTrigger();
            }
        }

        private void AssignCloakToken()
        {
            HostShip.Tokens.AssignToken(typeof(CloakToken), Triggers.FinishTrigger);
        }
    }
}
