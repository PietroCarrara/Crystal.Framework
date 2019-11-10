using Crystal.Framework;

namespace Crystal.ECS.Components
{
    public class Position : IComponent
    {
        private Vector2 position = Vector2.Zero;

        public float X
        {
            get => position.X;
            set => position.X = value;
        }

        public float Y
        {
            get => position.Y;
            set => position.Y = value;
        }

        public Position(Vector2? position = null)
        {
            if (position.HasValue)
            {
                this.position = position.Value;
            }
        }

        public Position(float x, float y) : this(new Vector2(x, y))
        { }

        public static implicit operator Vector2(Position p)
        {
            return p.position;
        }

        public static implicit operator Position(Vector2 v)
        {
            return new Position(v);
        }
    }
}