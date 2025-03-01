﻿using Upgrade;

namespace Ship
{
    namespace SecondEdition.T70XWing
    {
        public class NienNunb : T70XWing
        {
            public NienNunb() : base()
            {
                PilotInfo = new PilotCardInfo(
                    "Nien Nunb",
                    5,
                    54,
                    isLimited: true,
                    abilityType: typeof(Abilities.FirstEdition.NienNunbAbility),
                    extraUpgradeIcon: UpgradeType.Talent
                );

                ImageUrl = "https://squadbuilder.fantasyflightgames.com/card_images/en/00a3c393a33b33168bc61e47749e1474.png";
            }
        }
    }
}
