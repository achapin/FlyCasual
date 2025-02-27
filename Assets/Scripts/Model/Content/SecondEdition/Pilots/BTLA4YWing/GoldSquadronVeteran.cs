﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.BTLA4YWing
    {
        public class GoldSquadronVeteran : BTLA4YWing
        {
            public GoldSquadronVeteran() : base()
            {
                PilotInfo = new PilotCardInfo(
                    "Gold Squadron Veteran",
                    3,
                    31,
                    extraUpgradeIcons: new List<UpgradeType>() { UpgradeType.Talent, UpgradeType.Modification },
                    seImageNumber: 17
                );
            }
        }
    }
}
