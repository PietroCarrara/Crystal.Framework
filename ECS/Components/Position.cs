using Crystal.Framework;

namespace Crystal.Framework.ECS.Components
{
    public class Position : IComponent
    {
        public Vector2 Vector = Vector2.Zero;
        
        public Position(Vector2? position = null)
        {
            if (position.HasValue)
            {
                this.Vector = position.Value;
            }
        }

        public Position(float x, float y) : this(new Vector2(x, y))
        { }
    }
}