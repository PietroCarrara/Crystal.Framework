using System;
using System.Diagnostics;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public abstract class Widget
    {
        /// <summary>
        /// The area available to this widget
        /// </summary>
        public TextureSlice Area;

        /// <summary>
        /// The widget this is associated with.
        /// Null if this is the root element of a UI tree
        /// </summary>
        public Widget Parent { get; private set; }

        private Alignment? alignment;
        /// <summary>
        /// The alignment this widget should follow.
        /// Defaults to it's parent's alignment.
        /// </summary>
        public Alignment Alignment
        {
            get => alignment.HasValue ? alignment.Value :
                   Parent != null ? Parent.Alignment :
                                    Alignment.Center;
            set
            {
                this.alignment = value;
            }
        }


        private ITheme theme;
        /// <summary>
        /// The information about how should we style this widget
        /// </summary>
        /// <value></value>
        public ITheme Theme
        {
            set => this.theme = value;
            get
            {
                if (this.theme != null) return this.theme;

                if (this.Parent != null) return this.Parent.Theme;

                return null;
            }
        }

        private bool needsRebuild = true;
        /// <summary>
        /// Whether or not this widget has changed its state
        /// </summary>
        public bool NeedsRebuild => this.needsRebuild;

        private IUILayout layout;
        /// <summary>
        /// The information on how should this widget be drawn
        /// </summary>
        public IUILayout Layout
        {
            get
            {
                if (needsRebuild)
                {
                    debugValidate();
                    this.layout = this.Build();
                    this.needsRebuild = false;
                }

                return this.layout;
            }
        }

        /// <summary>
        /// Builds information on how this should be drawn to the screen
        /// </summary>
        /// <returns>Intructions on how to draw this widget</returns>
        protected abstract IUILayout Build();

        public void BecomeChildOf(Widget w)
        {
            if (this.Parent != null)
            {
                throw new Exception("Widget already has a parent!");
            }

            this.Parent = w;
        }

        /// <summary>
        /// Should be called everytime there is a change in state.
        /// Signals to other objects that this widget has changed its state.
        /// </summary>
        protected void ChangeState()
        {
            this.needsRebuild = true;
        }

        [Conditional("DEBUG")]
        private void debugValidate()
        {
            Debug.Assert(this.Area.Area > 0, "A widget must have a area!");
        }
    }
}