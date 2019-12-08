using System.Linq;
using Crystal.Framework.ECS.Components;
using Crystal.Framework.ECS.Components.Graphical;

namespace Crystal.Framework.ECS.Systems.Graphical
{
    public class CameraSpriteRenderer : IRenderer
    {
        public void Render(Scene s)
        {
            var camera = s.Entities
                .With<Position>()
                .With<Camera>()
                .Many()
                .Where(e =>
                {
                    var (cam, _) = e;
                    return cam.Active;
                })
                .FirstOrDefault();

            if (camera == null)
            {
                return;
            }

            var entities = s.Entities
                .With<Sprite>()
                .With<Position>()
                .Many();

            s.SpriteBatch.BeginDraw();

            foreach (var entity in entities)
            {
                var pos = entity.Find<Position>();

                foreach (var sprite in entity.FindAll<Sprite>())
                {
                    // TODO: Apply camera transformations on the sprite
                    sprite.Draw(pos.Vector, s.SpriteBatch);
                }
            }

            s.SpriteBatch.EndDraw();
        }
    }
}