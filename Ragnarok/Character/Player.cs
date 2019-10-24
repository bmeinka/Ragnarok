using OpenTK;

namespace Ragnarok.Character
{
    class Player : Character
    {
        private const float walk_speed = 3f;
        private readonly PlayerController controller;

        /// <summary>
        /// where the player is trying to go or aim in world space
        /// </summary>
        public Vector3 Target { get; set; }
        public Player(Scene scene) : base(scene.SpriteBatch)
        {
            Sprite = new Sprite(new Vector2(1f, 2f), new Vector3(0f, 0.5f, 0.8f));
            Position = Target = scene.Map.SpawnPoint;
            controller = new PlayerController(scene, this);
        }

        public override void Update(float delta)
        {
            // TODO: should the player be responsible for updating its own controller?
            //       if not, how would it get updated?
            controller.Update(delta);
            if (Target != Position)
            {
                var distance = delta * walk_speed;
                var movement = Target - Position;
                // if the player can make it to the destination this frame,
                // just go ahead and place it where it wants to be.
                if (movement.Length < distance)
                    Position = Target;
                else
                    Position += movement.Normalized() * distance;
            }
        }
    }
}
