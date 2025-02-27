﻿using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.NimbusClassVWing
    {
        public class ShadowSquadronEscort : NimbusClassVWing
        {
            public ShadowSquadronEscort() : base()
            {
                PilotInfo = new PilotCardInfo(
                    "Shadow Squadron Escort",
                    3,
                    28,
                    extraUpgradeIcon: UpgradeType.Talent
                );

                ImageUrl = "https://images-cdn.fantasyflightgames.com/filer_public/c0/b0/c0b03f12-cff6-43af-99df-6ddf61fd471a/swz80_ship_shadow-escort.png";
            }
        }
    }
}