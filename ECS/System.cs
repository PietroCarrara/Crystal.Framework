using Crystal.Framework.ECS.Query;

namespace Crystal.Framework.ECS
{
    public interface ISystem
    {
        void Update(Scene scene, float delta);
    }
}