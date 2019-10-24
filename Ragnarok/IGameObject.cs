namespace Ragnarok
{
    interface IDrawable { void Draw(float delta); }
    interface IUpdateable { void Update(float delta); }
    interface IGameObject : IDrawable, IUpdateable { }
}
