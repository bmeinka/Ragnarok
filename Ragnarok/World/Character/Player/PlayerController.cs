using System.Diagnostics;
using OpenTK;
using OpenTK.Input;
using Ragnarok.Core;
using Ragnarok.Gameplay;
using Ragnarok.Gameplay.Control;

namespace Ragnarok.World.Player
{
    class PlayerController : Controller
    {
        private const float click_delay = 0.5f;

        private readonly Player player;
        private readonly TopDownCamera camera;
        private readonly Map map;
        private readonly Stopwatch timer;

        private MouseTarget target;

        public PlayerController(TopDownCamera camera, Map map, Player player) : base(new IdleState())
        {
            this.player = player;
            this.camera = camera;
            this.map = map;
            timer = new Stopwatch();

            Game.Mouse.Down += Click;
        }
        private void HandleTarget()
        {
            switch (target.Type)
            {
                case TargetType.Terrain:
                    if (map.MouseIntersection(camera, out Vector2 position))
                        Replace(new MoveState(player, position));
                    break;
                case TargetType.Monster:
                    Replace(new MoveState(player, target.Monster.Position));
                    // TODO: attack the monster
                    break;
            }
        }
        private void Click(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                target = new MouseTarget(camera, map);
                timer.Restart();
                HandleTarget();
            }
        }
        public void Update(float delta)
        {
            if (Game.Mouse.IsButtonDown(MouseButton.Left) && timer.Elapsed.TotalSeconds >= click_delay)
                HandleTarget();
            camera.Target = new Vector3(player.Position.X, player.Position.Y, 0f);
        }
    }
}
