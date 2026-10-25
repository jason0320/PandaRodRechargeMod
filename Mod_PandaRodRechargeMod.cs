using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace Mod_PandaRodRechargeMod
{
    [BepInPlugin("panda.rodrecharge.mod", "Panda's Rod Recharge Mod", "1.0.0.0")]
    internal class Mod_PandaRodRechargeMod : BaseUnityPlugin
    {
        private void OnStartCore()
        {
            var dir = Path.GetDirectoryName(Info.Location);
            var excel = dir + "/Element/Element.xlsx";
            var sources = Core.Instance.sources;
            ModUtil.ImportExcel(excel, "Element", sources.elements);

            // The game caches its random scroll / rod / spellbook spell pools the first time
            // one is rolled. Clear them so the new Recharge row is always part of the pool.
            try
            {
                TraitScrollRandom.selector.listSource.Clear();
                TraitRodRandom.selecter.listSource.Clear();
                TraitBaseSpellbook.selecter.listSource.Clear();
            }
            catch (Exception e)
            {
                Logger.LogWarning("Could not reset random spell pools: " + e.Message);
            }

            var harmony = new Harmony("Panda's Rod Recharge Mod");
            harmony.PatchAll();
        }
    }
}
