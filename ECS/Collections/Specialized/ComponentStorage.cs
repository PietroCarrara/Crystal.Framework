using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Crystal.ECS.Collections.Specialized
{
    internal class ComponentStorage : IEnumerable<IComponent>
    {
        private List<IComponent> data = new List<IComponent>();

        public IEnumerator<IComponent> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        /// <summary>
        /// Add a component to this collection
        /// </summary>
        /// <param name="c">The component to be added</param>
        public void Add(IComponent c)
        {
            this.data.Add(c);
        }

        /// <summary>
        /// Returns the first component of type T
        /// </summary>
        /// <typeparam name="T">The type to look for</typeparam>
        /// <returns></returns>
        public T FindFirst<T>() where T : IComponent
        {
            foreach (var component in this.data)
            {
                if (component is T type)
                {
                    return type;
                }
            }

            return default(T);
        }

        public IEnumerable<T> FindAll<T>() where T : IComponent
        {
            return this.data.OfType<T>();
        }

        /// <summary>
        /// Tells if all the types appear in this collection.
        /// If one type is informed more than once, we look for
        /// that number of occurrences
        /// </summary>
        /// <param name="components">The types to look for</param>
        public bool ContainsAll(params Type[] components)
        {
            // Array telling which types have already been found
            bool[] contains = new bool[components.Length];


            foreach (var component in this.data)
            {
                int i = 0;
                foreach (var type in components)
                {
                    // If we have not yet seen this type and
                    // it matches, we have found it
                    if (!contains[i] && component.GetType() == type)
                    {
                        contains[i] = true;
                        break;
                    }

                    i++;
                }
            }

            // Return true if everyone is true
            return contains.All(b => b);
        }
    }
}