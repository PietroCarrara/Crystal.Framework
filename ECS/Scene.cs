using System.Collections.Generic;
using Crystal.Framework.ECS.Collections.Specialized;
using Crystal.Framework.Input;

namespace Crystal.Framework.ECS
{
    public class Scene
    {
        private EntityStorage entities = new EntityStorage();
        private SystemStorage systems = new SystemStorage();
        private RendererStorage renderers = new RendererStorage();

        public IInput Input;

        public EntityStorage Entities
        {
            get => entities;
        }

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
        /// Initialize all this scenes systems
        /// </summary>
        public void Initialize()
        {
            foreach (var system in this.systems)
            {
                system.Initialize(this);
            }
        }

        /// <summary>
        /// Update all this scenes systems
        /// </summary>
        /// <param name="deltaTime">Time in seconds elapsed since last update</param>
        public void Update(float deltaTime)
        {
            this.Input.Update();

            foreach (var system in this.systems)
            {
                system.Update(this, deltaTime);
            }
        }

        /// <summary>
        /// Draws the current state to the screen
        /// </summary>
        public void Render()
        {
            foreach (var renderer in this.renderers)
            {
                renderer.Render(this);
            }
        }

        /// <summary>
        /// Add a entity to this scene
        /// </summary>
        /// <param name="entity">The entity to be added</param>
        /// <returns>The added entity</returns>
        public Entity Add(Entity entity)
        {
            this.entities.Add(entity);
            return entity;
        }

        /// <summary>
        /// Adds a system to this scene
        /// </summary>
        /// <param name="s">The system to be added</param>
        /// <returns>The added system</returns>
        public ISystem Add(ISystem s)
        {
            this.systems.Add(s);
            return s;
        }

        /// <summary>
        /// Adds a renderer to this scene
        /// </summary>
        /// <param name="r">The renderer to be added</param>
        /// <returns>The added renderer</returns>
        public IRenderer Add(IRenderer r)
        {
            this.renderers.Add(r);
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