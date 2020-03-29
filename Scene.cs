using System;
using System.Collections.Generic;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI;
using Crystal.Framework.UI.Widgets;
using Crystal.Framework.Content;
using Crystal.Framework.Collections.Specialized;

namespace Crystal.Framework
{
    public abstract class Scene : IDisposable
    {
        /// <summary>
        /// The object to which we can delegate draw calls
        /// </summary>
        public IDrawer Drawer;

        /// <summary>
        /// The default theme of the UI
        /// </summary>
        public ITheme Theme
        {
            get => this.widgets.Root.Theme;
            set => this.widgets.Root.Theme = value;
        }

        /// <summary>
        /// Dynamic loader of content. Use only if your
        /// content can't be used with scene.Resource()
        /// </summary>
        public IContentManager Content;

        /// <summary>
        /// The design size of this scene. You can assume the screen is always
        /// this size, and the engine will scale everything accordingly
        /// </summary>
        public readonly Point Size = new Point(1280, 720);

        private bool initialized;

        private EntityStorage entities = new EntityStorage();
        private SystemStorage systems = new SystemStorage();
        private RendererStorage renderers = new RendererStorage();
        private CanvasStorage canvases = new CanvasStorage();
        private WidgetStorage widgets = new WidgetStorage();
        private InputActionStorage actions = new InputActionStorage();

        public EntityStorage Entities => entities;
        public WidgetStorage Widgets => widgets;
        public CanvasStorage Canvases => canvases;
        public InputActionStorage Actions => actions;

        /// <summary>
        /// Dictionary of resources
        /// The idea is that you preload assets into your scene and
        /// then access them here
        /// </summary>
        private Dictionary<string, object> resources = new Dictionary<string, object>();

        public readonly string Name;

        public Scene(string name, Point? size = null)
        {
            this.Name = name;

            if (size.HasValue)
            {
                this.Size = size.Value;
            }
        }

        /// <summary>
        /// Pushes a new scene into the scene stack
        /// </summary>
        /// <param name="name">The name of the scene to be pushed</param>
        /// <returns>The initialized scene</returns>
        public abstract Scene Push(string name);

        /// <summary>
        /// Swaps this scene scene with the informed scene onto the stack
        /// </summary>
        /// <param name="name">The name of the scene to swap</param>
        /// <returns>The initialized scene</returns>
        public abstract Scene Swap(string name);

        /// <summary>
        /// Remove this scene from the stack. If there are no scenes left on the
        /// stack, the game should exit
        /// </summary>
        public abstract void Pop();

        /// <summary>
        /// Load and initialize scene
        /// </summary>
        public virtual void Load()
        { }

        /// <summary>
        /// Unload scene
        /// </summary>
        public virtual void Unload()
        { }

        /// <summary>
        /// Called before starting to render this scene
        /// </summary>
        public virtual void BeforeRender()
        { }

        /// <summary>
        /// Called after ending to render this scene
        /// </summary>
        public virtual void AfterRender()
        { }

        /// <summary>
        /// Initialize all this scenes systems
        /// </summary>
        public void Initialize()
        {
            if (this.initialized)
            {
                throw new Exception("Scene already initialized!");
            }

            foreach (var system in this.systems)
            {
                system.Initialize(this);
            }

            foreach (var renderer in this.renderers)
            {
                renderer.Initialize(this);
            }

            this.initialized = true;
        }

        /// <summary>
        /// Update all this scene's systems
        /// </summary>
        /// <param name="deltaTime">Time in seconds elapsed since last update</param>
        public void Update(float deltaTime)
        {
            Input.Instance.Update();

            this.widgets.UpdateInput(Input.Instance);

            foreach (var system in this.systems)
            {
                system.Update(this, deltaTime);
            }
        }

        /// <summary>
        /// Draws the game state
        /// </summary>
        public void Render(float deltaTime)
        {
            this.BeforeRender();

            foreach (var renderer in this.renderers)
            {
                renderer.Render(this, deltaTime);
            }

            this.AfterRender();
        }

        /// <summary>
        /// Add a entity to this scene
        /// </summary>
        /// <param name="entity">The entity to be added</param>
        /// <returns>The added entity</returns>
        public IEntity Add(IEntity entity)
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

            if (this.initialized)
            {
                s.Initialize(this);
            }

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

        public Widget Add(Widget w)
        {
            this.Widgets.Add(w);
            return w;
        }

        /// <summary>
        /// Create a entity, add it to the scene and return it
        /// </summary>
        /// <returns>The newly created entity</returns>
        public IEntity Entity(string name = null)
        {
            return this.Add(new Entity(name));
        }

        /// <summary>
        /// Fetch a preloaded resource
        /// </summary>
        /// <param name="name">Resource name</param>
        /// <typeparam name="T">The resource type</typeparam>
        /// <returns>The resource</returns>
        public T Resource<T>(string name)
        {
            return (T)this.resources[name];
        }

        public void AddResource(string name, object res)
        {
            this.resources.Add(name, res);
        }

        public void Dispose()
        {
            this.Canvases.Dispose();
        }
    }
}