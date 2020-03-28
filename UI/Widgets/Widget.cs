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

        public delegate void StateChangedHandler(Widget widget);
        public event StateChangedHandler StateChanged;

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

        private bool hasFocus;
        public bool HasFocus
        {
            get => hasFocus;
            internal set
            {
                if (hasFocus != value)
                {
                    this.hasFocus = value;
                    this.ChangeState();
                }
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
            this.StateChanged?.Invoke(this);
            this.needsRebuild = true;
        }

        /// <summary>
        /// Called everytime the mouse enters this widget's area
        /// </summary>
        public virtual void OnMouseEnter()
        { }

        /// <summary>
        /// Called everytime the mouse leaves this widget's area
        /// </summary>
        public virtual void OnMouseLeave()
        { }

        /// <summary>
        /// Called when the mouse left clicks this widget's area
        /// </summary>
        public virtual void OnMouseClick()
        { }

        /// <summary>
        /// Called when the mouse right clicks this widget's area
        /// </summary>
        public virtual void OnMouseClickSecondary()
        { }

        /// <summary>
        /// Called when the mouse releases the left button on top of this widget
        /// </summary>
        public virtual void OnMouseReleased()
        { }

        /// <summary>
        /// Called when the mouse releases the right button on top of this widget
        /// </summary>
        public virtual void OnMouseReleasedSecondary()
        { }

        /// <summary>
        /// Called every frame that the mouse is on top of this window and the left button is down
        /// </summary>
        public virtual void OnMouseHold()
        { }

        /// <summary>
        /// Called every frame that the mouse is on top of this window and the right button is down
        /// </summary>
        public virtual void OnMouseHoldSecondary()
        { }

        [Conditional("DEBUG")]
        private void debugValidate()
        {
            Debug.Assert(this.AvailableArea.Area > 0, "A widget must have some available area!");
        }
    }
}