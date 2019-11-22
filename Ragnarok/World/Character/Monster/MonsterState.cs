using System.Diagnostics;
using Ragnarok.Gameplay.Control;

namespace Ragnarok.World.Monster
{
    /// <summary>
    /// a base class for monster control states, all of which need a reference to the monster and the map
    /// </summary>
    abstract class MonsterState : IControlState
    {
        public abstract void Update(MonsterController parent);
        void IControlState.Update(Controller parent) => Update((MonsterController)parent);
    }
}
