﻿using Upgrade;

namespace Ship
{
    namespace SecondEdition.LancerClassPursuitCraft
    {
        public class SabineWren : LancerClassPursuitCraft
        {
            public SabineWren() : base()
            {
                PilotInfo = new PilotCardInfo(
                    "Sabine Wren",
                    3,
                    58,
                    isLimited: true,
                    abilityType: typeof(Abilities.FirstEdition.SabineWrenLancerPilotAbility),
                    extraUpgradeIcon: UpgradeType.Talent,
                    seImageNumber: 220
                );

                PilotNameCanonical = "sabinewren-lancerclasspursuitcraft";
            }
        }
    }
}
