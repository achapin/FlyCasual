﻿using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIESfFighter
    {
        public class OmegaSquadronExpert : TIESfFighter
        {
            public OmegaSquadronExpert() : base()
            {
                PilotInfo = new PilotCardInfo(
                    "Omega Squadron Expert",
                    3,
                    33,
                    extraUpgradeIcon: UpgradeType.Talent
                );

                ImageUrl = "https://squadbuilder.fantasyflightgames.com/card_images/en/784d00f653ff7cd58cb634c7a59e47c1.png";
            }
        }
    }
}
