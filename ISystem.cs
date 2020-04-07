namespace Crystal.Framework
{
    public interface ISystem
    {
        void Update(Scene scene, Input input, float delta);

        void Initialize(Scene scene)
        {

        }
    }
}