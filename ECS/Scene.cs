using System.Collections.Generic;
using Crystal.ECS.Collections.Specialized;

namespace Crystal.ECS
{
    public class Scene
    {
        public EntityStorage Entities { get; private set; } = new EntityStorage();
        public SystemStorage Systems { get; private set; } = new SystemStorage();
        public RendererStorage Renderers { get; private set; } = new RendererStorage();

        /// <summary>
        /// If this scene has already been initialized
        /// </summary>
        public bool Initialized;
        
        /// <summary>
        /// Dictionary of resources
        /// The idea is that you preload assets into your scene and
        /// then access them here
        /// </summary>
        private Dictionary<string, object> resources = new Dictionary<string, object>();

        public readonly string Name;

        public Scene(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Add a entity to this scene
        /// </summary>
        /// <param name="entity">The entity to be added</param>
        /// <returns>The added entity</returns>
        public Entity Add(Entity entity)
        {
            this.Entities.Add(entity);
            return entity;
        }

        /// <summary>
        /// Adds a system to this scene
        /// </summary>
        /// <param name="s">The system to be added</param>
        /// <returns>The added system</returns>
        public ISystem Add(ISystem s)
        {
            this.Systems.Add(s);
            return s;
        }

        /// <summary>
        /// Adds a renderer to this scene
        /// </summary>
        /// <param name="r">The renderer to be added</param>
        /// <returns>The added renderer</returns>
        public IRenderer Add(IRenderer r)
        {
            this.Renderers.Add(r);
            return r;
        }

        /// <summary>
        /// Create a entity, add it to the scene and return it
        /// </summary>
        /// <returns>The newly created entity</returns>
        public Entity Entity(string name = null)
        {
            return this.Add(new Entity(name));
        }

        /// <summary>
        /// Fetch a preloaded resource
        /// </summary>
        /// <param name="name">Resource name</param>
        /// <typeparam name="T">The resource type</typeparam>
        /// <returns></returns>
        public T Resource<T>(string name)
        {
            return (T)this.resources[name]; 
        }

        public void AddResource(string name, object res)
        {
            this.resources.Add(name, res);
        }
    }
}