﻿using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.T70XWing
    {
        public class NimiChereen : T70XWing
        {
            public NimiChereen() : base()
            {
                PilotInfo = new PilotCardInfo(
                    "Nimi Chereen",
                    2,
                    47,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.NimiChereenAbility)
                );

                ImageUrl = "https://static.wikia.nocookie.net/xwing-miniatures-second-edition/images/b/b5/Nimi_chireen.png";
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class NimiChereenAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                "Nimi Chereen",
                IsAvailable,
                GetAiPriority,
                DiceModificationType.Change,
                1,
                sidesCanBeSelected: new List<DieSide>() { DieSide.Blank },
                sideCanBeChangedTo: DieSide.Focus
            );
        }

        private bool IsAvailable()
        {
            return Combat.AttackStep == CombatStep.Attack
                && Combat.Defender.State.Initiative > HostShip.State.Initiative
                && Combat.CurrentDiceRoll.Blanks > 0;
        }

        private int GetAiPriority()
        {
            return 100;
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }
    }
}