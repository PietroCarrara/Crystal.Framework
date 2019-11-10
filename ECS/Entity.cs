using System;
using System.Collections.Generic;
using Crystal.ECS.Collections.Specialized;

namespace Crystal.ECS
{
    public sealed class Entity
    {
        public readonly string Name;

        internal ComponentStorage Components { get; private set; } = new ComponentStorage();

        public Entity(string name = null)
        {
            this.Name = name;
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

        /// <summary>
        /// Add a component to the entity
        /// </summary>
        /// <param name="c"></param>
        /// <returns>The entity</returns>
        public Entity With<T>(T c) where T : IComponent
        {
            this.Add(c);

            return this;
        }

        /// <summary>
        /// Finds the first component of type T
        /// </summary>
        /// <typeparam name="T">The type to look for</typeparam>
        /// <returns></returns>
        public T FindFirst<T>() where T : IComponent
        {
            return this.Components.FindFirst<T>();
        }

        public IEnumerable<T> FindAll<T>() where T : IComponent
        {
            return this.Components.FindAll<T>();
        }
    }
}