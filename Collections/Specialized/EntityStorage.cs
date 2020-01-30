using System.Collections;
using System.Collections.Generic;
using Crystal.Framework.ECS.Query;

namespace Crystal.Framework.Collections.Specialized
{
    public class EntityStorage : IEnumerable<IEntity>, IEntityQuery
    {
        private List<IEntity> data = new List<IEntity>();

        public IEnumerator<IEntity> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        /// <summary>
        /// Add an entity to this collection
        /// </summary>
        /// <param name="e">The entity to be added</param>
        public virtual void Add(IEntity e)
        {
            this.data.Add(e);
        }

        public EntityFilter<T, IEntity> With<T>()
        where T : IComponent
        {
            return new EntityFilter<T, IEntity>(this.data);
        }

        IEntityQuery IEntityQuery.With<T>()
        {
            return this.With<T>();
        }
    }
}