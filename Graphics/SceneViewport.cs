using Crystal.Framework.ECS;
using Crystal.Framework.Math;

namespace Crystal.Framework.Graphics
{
    public abstract class SceneViewport : IDrawable
    {
        protected readonly Scene scene;

        public Point Size { get; private set; }

        public abstract int Width { get; }

        public abstract int Height { get; }

        public Matrix4 TransformMatrix { get; private set; }

        public SceneViewport(Scene scene)
        {
            this.scene = scene;
        }

        /// <summary>
        /// Sets the size of this viewport.
        /// Changes at which resolution the game should be rendered
        /// </summary>
        /// <param name="size">The width and height of the viewport</param>
        public virtual void SetSize(Point size)
        {
            this.Size = size;
            this.recalcMatrix();
        }

        private void recalcMatrix()
        {
            this.TransformMatrix = Matrix4.CreateScale(this.Size.X / this.scene.Size.X,
                                                       this.Size.Y / this.scene.Size.Y,
                                                       1);
        }

        public abstract void Dispose();
    }
}