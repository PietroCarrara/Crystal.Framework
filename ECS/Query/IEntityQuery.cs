using System.Collections.Generic;
namespace Crystal.Framework.ECS.Query
{
    public interface IEntityQuery
    {
        // Query only for entities that have component T
        IEntityQuery With<T>() where T : IComponent;
    }

    public interface IEntityQueryResults
    {
        // First result
        IEntity One();

        // All of the results
        IEnumerable<IEntity> Many();
    }
}