using System;
using System.Linq;
using System.Collections.Generic;


namespace Crystal.Framework.ECS.Query
{
    /// <summary>
    /// Filter containing only entities of type E with component C
    /// </summary>
    /// <typeparam name="C">Component all of the entities have</typeparam>
    /// <typeparam name="E">The entity type</typeparam>
    public class EntityFilter<C, E> : IEntityQuery, IEntityQueryResults
    where C : IComponent
    where E : IEntity
    {
        private IEnumerable<Entity<C, E>> data;

        public EntityFilter(IEnumerable<E> data)
        {
            this.data = data.Select(e => 
            {
                var c = e.Find<C>();

                if (c != null)
                {
                    return new Entity<C, E>(c, e);
                }

                return null;
            })
            .Where(e => e != null);
        }

        public IEnumerable<Entity<C, E>> Many()
        {
            return this.data;
        }

        public Entity<C, E> One()
        {
            return this.data.FirstOrDefault();
        }

        public EntityFilter<T, Entity<C, E>> With<T>() where T : IComponent
        {
            return new EntityFilter<T, Entity<C, E>>(this.data);
        }

        IEnumerable<IEntity> IEntityQueryResults.Many()
        {
            return this.Many();
        }

        IEntity IEntityQueryResults.One()
        {
            return this.One();
        }

        IEntityQuery IEntityQuery.With<T>()
        {
            return this.With<T>();
        }
    }
}