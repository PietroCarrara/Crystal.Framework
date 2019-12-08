using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.ECS.Collections.Specialized;

namespace Crystal.Framework.ECS
{
    /// <summary>
    /// Entity that has a component C owned by entity of type E
    /// </summary>
    /// <typeparam name="C">Type of the component</typeparam>
    /// <typeparam name="E">Type of the component's owner</typeparam>
    public class Entity<C, E> : IEntity
    where C : IComponent
    where E : IEntity
    {
        public readonly C Component;
        public readonly E Owner;

        public Entity(C c, E e)
        {
            this.Component = c;
            this.Owner = e;
        }

        public string Name => this.Owner.Name;

        public T Add<T>(T c) where T : IComponent
        {
            return this.Owner.Add(c);
        }

        public T Find<T>() where T : IComponent
        {
            return this.Owner.Find<T>();
        }

        public IEnumerable<T> FindAll<T>() where T : IComponent
        {
            return this.Owner.FindAll<T>();
        }

        public void Deconstruct(out C c, out E e)
        {
            c = this.Component;
            e = this.Owner;
        }
    }
    
    public sealed class Entity : IEntity
    {
        public string Name => this.name;

        private readonly string name;

        internal ComponentStorage Components { get; private set; } = new ComponentStorage();

        public Entity(string name = null)
        {
            this.name = name;
        }

        /// <summary>
        /// Adds a component to the entity
        /// </summary>
        /// <param name="c">The component to be added</param>
        public T Add<T>(T c) where T : IComponent
        {
            this.Components.Add(c);

            return c;
        }
        
        public IEnumerable<T> FindAll<T>() where T : IComponent
        {
            return this.Components.FindAll<T>();
        }

        /// <summary>
        /// Finds the first component of type T
        /// </summary>
        public T Find<T>() where T : IComponent
        {
            return this.Components.FindFirst<T>();
        }
    }
}