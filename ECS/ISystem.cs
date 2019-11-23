namespace Crystal.Framework.ECS
{
    public interface ISystem
    {
        void Update(Scene scene, float delta);

        void Initialize(Scene scene)
        {

        }
    }
}