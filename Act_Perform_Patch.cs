using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Mod_PandaRodRechargeMod
{
    internal class Act_Perform_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Act), "Perform", new Type[] { })]
        public static bool Prefix(Act __instance, ref bool __result)
        {
            if (__instance.id != 1234567)
            {
                return true;
            }
            __result = true;
            if (__instance.TargetType.Range == TargetRange.Self && !Act.forcePt)
            {
                Act.TC = Act.CC;
                Act.TP.Set(Act.CC.pos);
            }
            int pow = Act.CC.elements.GetOrCreateElement(__instance.source.id).GetPower(Act.CC) * Act.powerMod / 100;
            switch (__instance.id)
            {
                case 1234567:
                    Recharge(__instance, pow);
                    break;
            }
            return false;
        }

        public static void Recharge(Act act, int pow)
        {
            if (Act.TC == null)
            {
                Act.TC = Act.CC;
            }

            if (!Act.CC.isThing)
            {
                var rechargeOwner = new InvOwnerRecharge
                {
                    state = Act.CC.blessedState,
                    power = 100,                 
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
        }
    }
}
