using System.Linq;
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

        

        public IEnumerable<T> FindAll<T>() where T : IComponent
        {
            return this.Components.FindAll<T>();
        }

        /// <summary>
        /// Finds the first component of type T
        /// </summary>
        /// <typeparam name="T">The type to look for</typeparam>
        /// <returns></returns>
        public T Find<T>() where T : IComponent
        {
            return this.Components.FindFirst<T>();
        }

        /// <summary>
        /// Finds the first components of type T
        /// </summary>
        public (T1, T2) Find<T1, T2>()
            where T1 : IComponent
            where T2 : IComponent
        {
            T1 a = default(T1);
            T2 b = default(T2);
            var found = new bool[2];

            foreach (var component in this.Components)
            {
                if (!found[0] && component is T1 t1)
                {
                    a = t1;
                    found[0] = true;
                    continue;
                }

                if (!found[1] && component is T2 t2)
                {
                    b = t2;
                    found[1] = true;
                    continue;
                }

                if (found.All(p => p))
                {
                    break;
                }
            }

            return (a, b);
        }

        /// <summary>
        /// Finds the first components of type T
        /// </summary>
        public (T1, T2, T3) Find<T1, T2, T3>()
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
        {
            T1 a = default(T1);
            T2 b = default(T2);
            T3 c = default(T3);
            var found = new bool[3];

            foreach (var component in this.Components)
            {
                if (!found[0] && component is T1 t1)
                {
                    a = t1;
                    found[0] = true;
                    continue;
                }

                if (!found[1] && component is T2 t2)
                {
                    b = t2;
                    found[1] = true;
                    continue;
                }

                if (!found[2] && component is T3 t3)
                {
                    c = t3;
                    found[2] = true;
                    continue;
                }

                if (found.All(p => p))
                {
                    break;
                }
            }

            return (a, b, c);
        }

        /// <summary>
        /// Finds the first components of type T
        /// </summary>
        public (T1, T2, T3, T4) Find<T1, T2, T3, T4>()
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
        {
            T1 a = default(T1);
            T2 b = default(T2);
            T3 c = default(T3);
            T4 d = default(T4);
            var found = new bool[4];

            foreach (var component in this.Components)
            {
                if (!found[0] && component is T1 t1)
                {
                    a = t1;
                    found[0] = true;
                    continue;
                }

                if (!found[1] && component is T2 t2)
                {
                    b = t2;
                    found[1] = true;
                    continue;
                }

                if (!found[2] && component is T3 t3)
                {
                    c = t3;
                    found[2] = true;
                    continue;
                }

                if (!found[3] && component is T4 t4)
                {
                    d = t4;
                    found[3] = true;
                    continue;
                }

                if (found.All(p => p))
                {
                    break;
                }
            }

            return (a, b, c, d);
        }

        /// <summary>
        /// Finds the first components of type T
        /// </summary>
        public (T1, T2, T3, T4, T5) Find<T1, T2, T3, T4, T5>()
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
        {
            T1 a = default(T1);
            T2 b = default(T2);
            T3 c = default(T3);
            T4 d = default(T4);
            T5 e = default(T5);
            var found = new bool[5];

            foreach (var component in this.Components)
            {
                if (!found[0] && component is T1 t1)
                {
                    a = t1;
                    found[0] = true;
                    continue;
                }

                if (!found[1] && component is T2 t2)
                {
                    b = t2;
                    found[1] = true;
                    continue;
                }

                if (!found[2] && component is T3 t3)
                {
                    c = t3;
                    found[2] = true;
                    continue;
                }

                if (!found[3] && component is T4 t4)
                {
                    d = t4;
                    found[3] = true;
                    continue;
                }

                if (!found[4] && component is T5 t5)
                {
                    e = t5;
                    found[4] = true;
                    continue;
                }

                if (found.All(p => p))
                {
                    break;
                }
            }

            return (a, b, c, d, e);
        }
    }
}