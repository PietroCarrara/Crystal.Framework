using System.Collections.Generic;
namespace Crystal.Framework.ECS
{
    public interface IEntity
    {
        /// <summary>
        /// The name of this entity
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Add a component to this entity
        /// </summary>
        /// <param name="c">The component</param>
        /// <typeparam name="T">The type of the component</typeparam>
        /// <returns>The added component</returns>
        T Add<T>(T c) where T : IComponent;

        /// <summary>
        /// Find all components of type T
        /// </summary>
        /// <typeparam name="T">The type of the component</typeparam>
        IEnumerable<T> FindAll<T>() where T : IComponent;
        
        /// <summary>
        /// Locate the first component of type T
        /// </summary>
        /// <typeparam name="T">The type of the component</typeparam>
        T Find<T>() where T : IComponent;
    }
}