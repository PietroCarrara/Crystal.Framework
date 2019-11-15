using System.Collections;
using System.Collections.Generic;
using System;

namespace Crystal.Framework.ECS.Collections.Specialized
{
    public class EntityStorage : IEnumerable<Entity>
    {
        private List<Entity> data = new List<Entity>();

        public IEnumerator<Entity> GetEnumerator()
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
        public void Add(Entity e)
        {
            this.data.Add(e);
        }

        /// <summary>
        /// Finds a named entity
        /// </summary>
        /// <param name="name">The name to look for</param>
        /// <returns></returns>
        public Entity FindNamed(string name)
        {
            if (name == null)
            {
                throw new Exception("Cannot search for a named entity with name null!");
            }

            return this.data.Find(x => x.Name == name);
        }
    }
}