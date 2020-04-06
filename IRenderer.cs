using Crystal.Framework.Graphics;

namespace Crystal.Framework
{
    public interface IRenderer
    {
        void Initialize(Scene scene)
        { }

        void Render(Scene scene, IDrawer drawer, float delta);
    }
}