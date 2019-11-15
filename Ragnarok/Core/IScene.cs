namespace Ragnarok.Core
{
    /// <summary>
    /// A scene is a central location for game logic. One scene can be loaded at a time.
    /// </summary>
    interface IScene
    {
        /// <summary>
        /// called once when the scene is loaded
        /// </summary>
        void Load();
        /// <summary>
        /// called once just before a new scene is loaded or the end of the game
        /// </summary>
        void Unload();
        /// <summary>
        /// called once per update frame while the scene is currently loaded
        /// </summary>
        /// <param name="delta">the amount of time in seconds since the previous frame</param>
        void Update(float delta);
        /// <summary>
        /// called once per render frame while the scene is currently loaded
        /// </summary>
        void Draw();
    }
}
