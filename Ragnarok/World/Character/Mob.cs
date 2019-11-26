using OpenTK;
using Ragnarok.Core.Graphics;
using Ragnarok.Core.Physics;

namespace Ragnarok.World
{
    /// <summary>
    /// A Player or a Monster. Drawn as a Sprite in the game world.
    /// </summary>
    abstract class Mob
    {
        public int HP { get; protected set; }
        public int ATK { get; protected set; }
        public float Sight => 10f;
        public Skill BasicAttack => new Skill(ATK, 1.1f, 0.7f);

        protected DynamicBody body;
        protected readonly Sprite sprite;
        public Vector2 Position => body.Position;
        public Mob(Sprite sprite) => this.sprite = sprite;
        public void Spawn(Map map, Vector2 position)
        {
            body = map.SpawnMob(position);
            body.EnabledCallback = IsAlive;
        }
        public void Draw(SpriteBatch sb)
        {
            if (IsAlive())
                sb.Add(sprite, new Vector3(Position.X, Position.Y, 0f));
        }
        public void Move(Vector2 direction) => body.Move(direction);
        public void MoveTo(Vector2 position) => body.MoveTo(position);
        public void Stop() => body.MoveTo(body.Position);
        public bool IsAlive() => HP > 0;
        public void TakeHit(Skill skill)
        {
            HP -= skill.Damage;
            System.Console.WriteLine($"-{skill.Damage} : {HP}");
        }
    }
}
