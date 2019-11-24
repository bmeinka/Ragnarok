using System.Diagnostics;
using Ragnarok.Gameplay.Control;

namespace Ragnarok.World.Monster
{
    /// <summary>
    /// a base class for monster control states, all of which need a reference to the monster and the map
    /// </summary>
    abstract class MonsterState : IControlState
    {
        protected readonly Monster monster;
        protected readonly Map map;
        public MonsterState(Monster monster, Map map)
        {
            this.monster = monster;
            this.map = map;
        }
        public abstract void Update(Controller parent);
    }
}
