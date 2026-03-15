using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Mod_PandaRodRechargeMod
{
    internal class actRecharge : Ability
    {
        public override bool Perform()
        {
            if (Act.TC == null)
            {
                Act.TC = Act.CC;
            }

            if (!Act.CC.isThing)
            {
                // Create your custom inventory logic
                var rechargeOwner = new InvOwnerRecharge
                {
                    state = Act.CC.blessedState, // Or CC.blessedState depending on your scope
                    power = 100,                 // Adjust power as needed
                    price = 0,
                    count = 1
                };

                // Pass it to the game's native LayerDragGrid
                LayerDragGrid.TryProc(Act.CC, rechargeOwner);
            }

            if (Act.TC != null && Act.TC.Num > 1)
            {
                Act.TC = Act.TC.Split(1);
            }
            return true;
        }
    }
}

