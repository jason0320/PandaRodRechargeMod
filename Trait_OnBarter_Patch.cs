using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Mod_PandaRodRechargeMod
{
    [HarmonyPatch]
    internal class Trait_OnBarter_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Trait), "OnBarter")]
        public static void ExpireDays(Trait __instance)
        {
            Thing t = __instance.owner.things.Find("chest_merchant");
            ShopType shopType = __instance.ShopType;
            if (shopType == ShopType.Plat)
            {
                NoRestock(ThingGen.CreateSkillbook(12345678));
            }

            void NoRestock(Thing _t)
            {
                HashSet<string> hashSet = EClass.player.noRestocks.TryGetValue(__instance.owner.id);
                if (hashSet == null)
                {
                    hashSet = new HashSet<string>();
                }
                if (!hashSet.Contains(_t.trait.IdNoRestock))
                {
                    hashSet.Add(_t.trait.IdNoRestock);
                    EClass.player.noRestocks[__instance.owner.id] = hashSet;
                    _t.SetInt(101, 1);
                    AddThing(_t);
                }
            }

            Thing AddThing(Thing _t)
            {
                return t.AddThing(_t);
            }
        }
    }
}
