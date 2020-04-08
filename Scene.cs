using System;
using System.Numerics;
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
        /// The default theme of the UI
        /// </summary>
        public ITheme Theme
        {
            get => this.widgets.Root.Theme;
            set => this.widgets.Root.Theme = value;
        }

        /// <summary>
        /// The design size of this scene
        /// </summary>
        public readonly Point Size = new Point(1280, 720);

        private bool initialized;

        private EntityStorage entities = new EntityStorage();
        private SystemStorage systems = new SystemStorage();
        private RendererStorage renderers = new RendererStorage();
        private WidgetStorage widgets = new WidgetStorage();
        private InputActionStorage actions = new InputActionStorage();

        public EntityStorage Entities => entities;
        public WidgetStorage UI => widgets;
        public InputActionStorage Actions => actions;

        /// <summary>
        /// Dynamic loader of content. Use only if your
        /// content can't be used with scene.Resource()
        /// </summary>
        public readonly IContentManager Content;

        /// <summary>
        /// The main canvas of this scene. It's contents are displayed on the
        /// screen after the end of each render call
        /// </summary>
        public readonly IResizeableRenderTarget Canvas;

        /// <summary>
        /// A canvas that is always the size of the window
        /// </summary>
        public readonly IRenderTarget WindowCanvas;

        /// <summary>
        /// How to scale the canvases when drawing
        /// </summary>
        public readonly IScaler Scaler;

        /// <summary>
        /// Creates a new scene
        /// </summary>
        /// <param name="size">The design resolution size</param>
        /// <param name="canvas">The canvas where the scene draws</param>
        /// <param name="windowCanvas">The canvas where the scene draw, should always be at the window size</param>
        public Scene(Point size,
                     IContentManager content,
                     IResizeableRenderTarget canvas,
                     IRenderTarget windowCanvas,
                     IScaler scaler)
        {
            this.Size = size;
            this.Content = content;
            this.Canvas = canvas;
            this.WindowCanvas = windowCanvas;
            this.Scaler = scaler;
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
        public virtual void Initialize()
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
        public void Update(float deltaTime, Input input)
        {
            this.widgets.UpdateInput(input);

            foreach (var system in this.systems)
            {
                system.Update(this, input, deltaTime);
            }
        }

        /// <summary>
        /// Draws the game state
        /// </summary>
        public void Render(float deltaTime, IDrawer drawer)
        {
            this.BeforeRender();

            this.Canvas.Clear(Color.Transparent);
            this.WindowCanvas.Clear(Color.Transparent);

            foreach (var renderer in this.renderers)
            {
                renderer.Render(this, drawer, deltaTime);
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
            this.UI.Add(w);
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

        public void Dispose()
        {
            this.Content.Dispose();
            this.Canvas.Dispose();
            this.WindowCanvas.Dispose();
        }
    }
}