using System.Diagnostics;
using OpenTK;
using OpenTK.Input;
using Ragnarok.Core;
using Ragnarok.Gameplay;
using Ragnarok.Gameplay.Control;
using Ragnarok.Gameplay.Control.State;

namespace Ragnarok.World
{
    class PlayerController : Controller
    {
        private const float click_delay = 0.5f;

        private readonly Player player;
        private readonly TopDownCamera camera;
        private readonly Map map;
        private readonly Stopwatch timer;

        private MouseTarget target;

        public PlayerController(TopDownCamera camera, Map map, Player player)
        {
            this.player = player;
            this.camera = camera;
            this.map = map;
            timer = new Stopwatch();

            Game.Mouse.Down += Click;
        }

        /// <summary>
        /// used to determine if a repeating command should continue to repeat
        /// </summary>
        /// <returns>true if the mouse button is held, false if it is released</returns>
        private bool Continue() => Game.Mouse.IsButtonDown(MouseButton.Left);

        private void HandleTarget()
        {
            switch (target.Type)
            {
                case TargetType.Terrain:
                    Collapse(new Move(player, target.GetPosition));
                    break;
                case TargetType.Monster:
                    Collapse(new Attack(player, target.Monster, Continue));
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

        public override void Update()
        {
            if (Continue() && timer.Elapsed.TotalSeconds >= click_delay)
                target.UpdatePosition();
            base.Update();
            camera.Target = new Vector3(player.Position.X, player.Position.Y, 0f);
        }
        public override IControlState GetDefaultState() => new Idle(player);
    }
}
