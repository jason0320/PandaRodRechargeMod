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

            var harmony = new Harmony("Panda's Rod Recharge Mod");
            harmony.PatchAll();
        }
    }
}
