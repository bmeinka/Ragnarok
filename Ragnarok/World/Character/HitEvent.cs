using System;

namespace Ragnarok.World
{
    class HitEvent : EventArgs
    {
        public Mob Attacker { get; private set; }
        public Mob Target { get; private set; }
        public HitEvent(Mob attacker, Mob target) =>
            (Attacker, Target) = (attacker, target);
    }
}
