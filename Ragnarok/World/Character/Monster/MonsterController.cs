using System;
using Ragnarok.Core;
using Ragnarok.Gameplay.Control;

namespace Ragnarok.World.Monster
{
    class MonsterController : Controller
    {
        public Monster Monster { get; private set; }
        public Map Map { get; private set; }
        public MonsterController(Monster monster, Map map) : base(new IdleState())
        {
            Monster = monster;
            Map = map;
        }
    }
}
