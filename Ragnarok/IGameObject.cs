namespace Ragnarok
{
    // TODO: get rid of this in favor of something better
    interface IDrawable { void Draw(float delta); }
    interface IUpdateable { void Update(float delta); }
    interface IGameObject : IDrawable, IUpdateable { }
}
