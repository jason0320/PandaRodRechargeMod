using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Mod_PandaRodRechargeMod
{
    [HarmonyPatch(typeof(TraitScrollStatic), "OnRead")]
    internal class TraitScrollStatic_OnRead_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(TraitScrollStatic __instance, Chara c)
        {
            if (__instance == null || c == null)
                return;

            var src = __instance.source;
            if (src == null)
                return;

            const int customId = 1234567;
            const string customAlias = "spRecharge";

            if (src.id != customId && (src.alias == null || src.alias != customAlias))
                return;

            int pow = __instance.Power;

            try
            {
                if (Act.CC != null && Act.CC.elements != null)
                {
                    pow = Act.CC.elements.GetOrCreateElement(src.id).GetPower(Act.CC) * Act.powerMod / 100;
                }
                else if (c.elements != null)
                {
                    pow = c.elements.GetOrCreateElement(src.id).GetPower(c) * Act.powerMod / 100;
                }
            }
            catch
            {
                
            }

            Chara caller = Act.CC ?? c;
            if (caller == null)
                return;

            if (Act.CC != null && Act.TC == null)
                Act.TC = Act.CC;

            if (!caller.isThing)
            {
                var rechargeOwner = new InvOwnerRecharge
                {
                    state = caller.blessedState,
                    power = pow,
                    price = 0,
                    count = 1
                };

                LayerDragGrid.TryProc(caller, rechargeOwner);
            }

            if (Act.TC != null && Act.TC.Num > 1)
            {
                Act.TC = Act.TC.Split(1);
            }

        }
    }
}