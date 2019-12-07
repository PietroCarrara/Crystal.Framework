using System.Linq;
using Crystal.Framework.ECS.Query;
using Crystal.Framework.ECS.Components;
using Crystal.Framework.ECS.Components.Graphical;

namespace Crystal.Framework.ECS.Systems.Graphical
{
    public class CameraSpriteRenderer : IRenderer
    {
        public void Render(Scene s)
        {
            var camera = new EntityQuery()
                .HasComponents(typeof(Position), typeof(Camera))
                .WhereComponent<Camera>(c => c.Active)
                .Run(s)
                .FirstOrDefault();

            if (camera == null)
            {
                return;
            }

            var entities = new EntityQuery()
                .HasComponents(typeof(Sprite), typeof(Position))
                .Run(s);

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