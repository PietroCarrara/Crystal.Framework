using System;
using System.Linq;
using Crystal.Framework.Math;
using Crystal.Framework.ECS.Components;
using Crystal.Framework.ECS.Components.Graphical;

namespace Crystal.Framework.ECS.Systems.Graphical
{
    public class CameraSpriteRenderer : IRenderer
    {
        public void Render(Scene s, float delta)
        {
            var camera = s.Entities
                .With<Camera>()
                .Many()
                .Where(e => e.Component.Active)
                .FirstOrDefault();

            if (camera == null)
            {
                return;
            }

            var cam = camera.Component;

            var entities = s.Entities
                .With<ISprite>()
                .With<Position>()
                .Many();

            s.SpriteBatch.BeginDraw(transformMatrix: cam.TransformationMatrix * s.Viewport.TransformMatrix);

            foreach (var entity in entities)
            {
                var (pos, _) = entity;

                foreach (var sprite in entity.FindAll<ISprite>())
                {
                    sprite.Draw(pos.Vector, delta, s.SpriteBatch);
                }
            }

            s.SpriteBatch.EndDraw();
        }
    }
}