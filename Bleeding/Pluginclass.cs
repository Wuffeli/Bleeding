using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synapse.Api.Plugin;
using Synapse;

namespace Bleeding
{
    [PluginInformation(
        Author = "Wuffeli",
        Description = "This Plugin will add bleeding to the game!",
        LoadPriority = 0,
        Name = "Bleeding",
        SynapseMajor = 2,
        SynapseMinor = 6,
        SynapsePatch = 1,
        Version = "V1.0.0"
        )]
    public class Pluginclass : AbstractPlugin
    {
        public override void Load()
        {
            base.Load();
            Synapse.Api.Logger.Get.Info("Bleeding started successfully!");
            Server.Get.Events.Player.PlayerDamageEvent += Player_PlayerDamageEvent;
        }

        private void Player_PlayerDamageEvent(Synapse.Api.Events.SynapseEventArguments.PlayerDamageEventArgs ev)
        {
            if (ev.DamageAmount >= 5 && ev.HitInfo.GetDamageType() != DamageTypes.None)
            {
                ev.Victim.GiveTextHint("You will die in 60 seconds because of blood loss! Heal yourself with a MEDKIT!", 5);
                var health = ev.Victim.Health - ev.DamageAmount;

                MEC.Timing.CallDelayed(60f, () =>
                {
                    if (ev.Victim.Health <= health)
                        ev.Victim.Kill(DamageTypes.None);
                });
            }
        }
    }
}
