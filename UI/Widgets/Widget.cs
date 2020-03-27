using System.Collections.ObjectModel;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public abstract class Widget
    {
        private List<Widget> children = new List<Widget>();
        public ReadOnlyCollection<Widget> Children => children.AsReadOnly();
        
        private TextureSlice availableArea;
        /// <summary>
        /// The area available to this widget
        /// </summary>
        public TextureSlice AvailableArea
        {
            get => availableArea;
            set
            {
                if (value != availableArea)
                {
                    this.availableArea = value;
                    this.ChangeState();
                }
            }
        }

        private TextureSlice? area;
        /// <summary>
        /// The area this widget actually occupies
        /// </summary>
        /// <value></value>
        public TextureSlice Area
        {
            get
            {
                if (this.needsRebuild)
                {
                    this.rebuild();
                }
                
                if (area.HasValue)
                {
                    return area.Value;
                }
                else
                {
                    return this.AvailableArea;
                }
            }
            protected set
            {
                area = value;
            }
        }

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
                this.ChangeState();
            }
        }


        private ITheme theme;
        /// <summary>
        /// The information about how should we style this widget
        /// </summary>
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
                    this.rebuild();
                }

                return this.layout;
            }
        }

        private void rebuild()
        {
            debugValidate();
            this.needsRebuild = false;
            this.layout = this.Build();
            // Just marvel at this beauty
            this.layout.Match(
                setBuilder,
                setBuilder,
                setBuilder,
                setBuilder
            );
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

            w.children.Add(this);
            this.Parent = w;
        }

        /// <summary>
        /// Should be called everytime there is a change in state.
        /// Signals to other objects that this widget has changed its state.
        /// </summary>
        protected void ChangeState()
        {
            this.sigalForRebuild();
        }

        /// <summary>
        /// Should be called to signal this widget to be rebuilt
        /// </summary>
        private void sigalForRebuild()
        {
            this.needsRebuild = true;
        }

        [Conditional("DEBUG")]
        private void debugValidate()
        {
            Debug.Assert(this.AvailableArea.Area > 0, "A widget must have some available area!");
        }

        
        private void setBuilder(OrderedUILayouts l)
        {
            l.Builder = this;
            this.layout = l;
        }
        
        private void setBuilder(IDrawableUILayout l)
        {
            l.Builder = this;
            this.layout = l;
        }
        
        private void setBuilder(TextUILayout l)
        {
            l.Builder = this;
            this.layout = l;
        }
        
        private void setBuilder(IAnimatableUILayout l)
        {
            l.Builder = this;
            this.layout = l;
        }
    }
}