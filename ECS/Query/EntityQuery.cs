using System;
using System.Linq;
using System.Collections.Generic;

namespace Crystal.ECS.Query
{
    public class EntityQuery
    {
        protected Func<Entity, bool> selector;

        public EntityQuery()
        {
            this.selector = (e) => true;
        }

        /// <summary>
        /// Creates a query based on another query
        /// </summary>
        /// <param name="prev">The previous query selector</param>
        /// <param name="curr">The selector of this query</param>
        private EntityQuery(Func<Entity, bool> prev, Func<Entity, bool> curr)
        {
            this.selector = (e) => prev(e) && curr(e);
        }

        public IEnumerable<Entity> Run(Scene s)
        {
            return s.Entities.Where(this.selector);
        }

        public EntityQuery HasComponents(params Type[] components)
        {
            return new EntityQuery(
                this.selector,
                (e) => e.Components.ContainsAll(components)
            );
        }

        /// <summary>
        /// Filters a entity collection based on a predicate
        /// </summary>
        /// <param name="predicate">Entities that return true for this will remain in the collection</param>
        public EntityQuery Where(Func<Entity, bool> predicate)
        {
            return new EntityQuery(
                this.selector,
                predicate
            );
        }

        /// <summary>
        /// Finds entities that contain a component AND that component follows a certain predicate
        /// </summary>
        /// <param name="predicate">A predicate the component must follow</param>
        /// <typeparam name="T">The component type</typeparam>
        public EntityQuery WhereComponent<T>(Func<T, bool> predicate) where T : IComponent
        {
            return new EntityQuery(
                this.selector,
                (e) => {
                    T c1 = e.FindFirst<T>();

                    if (c1 == null)
                    {
                        return false;
                    }

                    return predicate(c1);
                }
            );
        }
    }
}