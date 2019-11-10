using Crystal.ECS.Query;

namespace Crystal.ECS
{
    public interface ISystem
    {
        void Update(Scene scene, float delta);
    }
}