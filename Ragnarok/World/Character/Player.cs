﻿using OpenTK;
using Ragnarok.Core;
using Ragnarok.Core.Graphics;
using Ragnarok.Gameplay;

namespace Ragnarok.World
{
    class Player : Character
    {
        private const float walk_speed = 3f;
        private readonly PlayerController controller;

        public Player(Scene scene) : base(scene.SpriteBatch, scene.World, scene.Map.SpawnPoint)
        {
            Sprite = new Sprite(new Vector2(1f, 2f), new Vector3(0f, 0.5f, 0.8f));
            controller = new PlayerController(scene, this);
        }

        public void MoveTo(Vector2 position) => body.MoveTo(position);

        public override void Update(float delta)
        {
            // TODO: should the player be responsible for updating its own controller?
            //       if not, how would it get updated?
            controller.Update(delta);
        }
    }
}