using System.Linq;
using Crystal.ECS.Query;
using Crystal.ECS.Components;
using Crystal.ECS.Components.Graphical;

namespace Crystal.ECS.Systems.Graphical
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

            foreach (var entity in entities)
            {
                var pos = entity.FindFirst<Position>();

                foreach (var sprite in entity.FindAll<Sprite>())
                {
                    sprite.Draw(pos);
                }
            }
        }
    }
}