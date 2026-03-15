using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod_PandaRodRechargeMod
{
    public class InvOwnerRecharge : InvOwnerEffect
    {
        public override bool CanTargetAlly => true;

        public override string langTransfer => "invEnchant";

        public override string langWhat => "target_what";

        public override Thing CreateDefaultContainer()
        {
            return ThingGen.CreateScroll(1234567);
        }

        public override bool ShouldShowGuide(Thing t)
        {
            return t.trait is TraitRod ||
                   t.trait is TraitRodRandom ||
                   t.trait is TraitSpellbook ||
                   t.trait is TraitSpellbookRandom;
        }

        public override void _OnProcess(Thing t)
        {
            int amount = 3 + ELayer.rnd(this.power / 10 + 1);

            if (this.state == BlessedState.Blessed) amount *= 2;
            if (this.state == BlessedState.Cursed) amount = 1;

            t.c_charges += amount;

            t.PlayEffect("identify");
            t.PlaySound("reconstruct");

            if (Lang.isJP)
            {
                Msg.SayRaw(t.GetName(NameStyle.Full) + amount + "の回数が回復した。");
            }
            else
            {
                Msg.SayRaw(t.GetName(NameStyle.Full) + amount + " charges restored!");
            }

        }
    }

}
