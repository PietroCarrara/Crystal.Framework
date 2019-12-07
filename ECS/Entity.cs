using System;
using System.Collections.Generic;
using Crystal.Framework.ECS.Collections.Specialized;

namespace Crystal.Framework.ECS
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

        /// <summary>
        /// Searches and executes an action with some of this entities components
        /// </summary>
        /// <param name="action">The action to perform on the components</param>
        public void With<T1>(Action<T1> action) where T1 : IComponent
        {
            action(
                this.FindFirst<T1>()
            );
        }

        /// <summary>
        /// Searches and executes an action with some of this entities components
        /// </summary>
        /// <param name="action">The action to perform on the components</param>
        public void With<T1, T2>(Action<T1, T2> action)
            where T1 : IComponent
            where T2 : IComponent
        {
            action(
                this.FindFirst<T1>(),
                this.FindFirst<T2>()
            );
        }

        /// <summary>
        /// Searches and executes an action with some of this entities components
        /// </summary>
        /// <param name="action">The action to perform on the components</param>
        public void With<T1, T2, T3>(Action<T1, T2, T3> action) 
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
        {
            action(
                this.FindFirst<T1>(),
                this.FindFirst<T2>(),
                this.FindFirst<T3>()
            );
        }

        /// <summary>
        /// Searches and executes an action with some of this entities components
        /// </summary>
        /// <param name="action">The action to perform on the components</param>
        public void With<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) 
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
        {
            action(
                this.FindFirst<T1>(),
                this.FindFirst<T2>(),
                this.FindFirst<T3>(),
                this.FindFirst<T4>()
            );
        }

        /// <summary>
        /// Searches and executes an action with some of this entities components
        /// </summary>
        /// <param name="action">The action to perform on the components</param>
        public void With<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action) 
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
            where T5 : IComponent
        {
            action(
                this.FindFirst<T1>(),
                this.FindFirst<T2>(),
                this.FindFirst<T3>(),
                this.FindFirst<T4>(),
                this.FindFirst<T5>()
            );
        }
    }
}