namespace Crystal.Framework
{
    public interface IRenderer
    {
        void Initialize(Scene scene)
        { }

        void Render(Scene scene, float delta);
    }
}