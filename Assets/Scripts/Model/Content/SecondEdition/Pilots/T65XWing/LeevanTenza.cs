﻿using System.Collections;
using System.Collections.Generic;
using Actions;
using ActionsList;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.T65XWing
    {
        public class LeevanTenza : T65XWing
        {
            public LeevanTenza() : base()
            {
                PilotInfo = new PilotCardInfo(
                    "Leevan Tenza",
                    3,
                    41,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.LeevanTenzaAbility),
                    extraUpgradeIcons: new List<UpgradeType>() { UpgradeType.Talent, UpgradeType.Illicit },
                    seImageNumber: 8
                );

                ModelInfo.SkinName = "Partisan";
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class LeevanTenzaAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnActionIsPerformed += CheckLeevanTenzaAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnActionIsPerformed -= CheckLeevanTenzaAbility;
        }

        private void CheckLeevanTenzaAbility(GenericAction action)
        {
            if (action is BoostAction || action is BarrelRollAction)
            {
                RegisterAbilityTrigger(TriggerTypes.OnActionIsPerformed, AskToUseLeevanTenzaAbility);
            }
        }

        private void AskToUseLeevanTenzaAbility(object sender, System.EventArgs e)
        {
            HostShip.AskPerformFreeAction(
                new EvadeAction() { Color = ActionColor.Red },
                Triggers.FinishTrigger,
                HostShip.PilotInfo.PilotName,
                "After you perform a Barrel Roll or Boost action, you may perform a red Evade action.",
                HostShip
            );
        }
    }
}
