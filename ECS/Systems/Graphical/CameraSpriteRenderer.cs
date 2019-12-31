using System.Linq;
using Crystal.Framework.ECS.Components;
using Crystal.Framework.ECS.Components.Graphical;

namespace Crystal.Framework.ECS.Systems.Graphical
{
    public class CameraSpriteRenderer : IRenderer
    {
        public void Render(Scene s, float delta)
        {
            var camera = s.Entities
                .With<Position>()
                .With<Camera>()
                .Many()
                .Where(e => e.Component.Active)
                .FirstOrDefault();

            if (camera == null)
            {
                return;
            }

            var (_, (camPos, _)) = camera;

            var entities = s.Entities
                .With<ISprite>()
                .With<Position>()
                .Many();

            s.SpriteBatch.BeginDraw();

            foreach (var entity in entities)
            {
                var (pos, _) = entity;

                foreach (var sprite in entity.FindAll<ISprite>())
                {
                    // TODO: Apply other camera transformations to the sprite
                    sprite.Draw(pos.Vector - camPos.Vector, delta, s.SpriteBatch);
                }
            }

            s.SpriteBatch.EndDraw();
        }
    }
}