using System;
using Ragnarok.Core;

namespace Ragnarok.World.Monster
{
    class MonsterController
    {
        private readonly (float Min, float Max) pause_range;
        private readonly (float Min, float Max) move_range;
        private bool moving;
        private readonly Monster monster;
        private readonly Timer timer;
        public MonsterController(Monster monster)
        {
            this.monster = monster;
            moving = false;
            pause_range = (0f, 3f);
            move_range = (2f, 3f);
            timer = new Timer();
            timer.Event += Timeout;
        }

        private void Timeout(object sender, EventArgs e)
        {
            if (moving)
            {
                monster.Stop();
                moving = false;
                timer.Start(Game.Random.Float(pause_range));
            }
            else
            {
                monster.Move(Game.Random.Vector2());
                moving = true;
                timer.Start(Game.Random.Float(move_range));
            }
        }
        public void Start() => timer.Start(Game.Random.Float(pause_range));
    }
}
