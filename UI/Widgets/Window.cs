using System;
using System.Numerics;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class Window : Widget
    {
        private Vector2 position;
        private float width, height;
        private Vector2? minSize, maxSize;

        private Widget child;
        private Panel background;

        private TextureSlice windowArea;

        private Vector2? mousePos,
                         mousePosSecondary,
                         startMousePosSecondary;


        public bool Resizable, Movable;

        public Vector2? MinSize
        {
            get => minSize;
            set
            {
                this.minSize = value;
                this.ChangeState();
            }
        }

        public Vector2? MaxSize
        {
            get => maxSize;
            set
            {
                maxSize = value;
                this.ChangeState();
            }
        }

        public Vector2 Position
        {
            get => position;
            set
            {
                this.position = value;
                this.ChangeState();
            }
        }

        public float Width
        {
            get => width;
            set
            {
                this.width = value;
                this.ChangeState();
            }
        }

        public float Height
        {
            get => height;
            set
            {
                this.height = value;
                this.ChangeState();
            }
        }

        public Widget Child
        {
            get => child;
            set
            {
                if (child != null)
                {
                    throw new Exception("Can't change the child of a window!");
                }

                this.child = value;
                this.background.Child = child;
                this.ChangeState();
            }
        }

        public Color Tint
        {
            get => background.Tint;
            set
            {
                background.Tint = value;
                this.ChangeState();
            }
        }

        public Window()
        {
            this.background = new Panel();
            background.BecomeChildOf(this);
        }

        public override void OnMouseClick(UIEvent e, Input input)
        {
            this.mousePos = input.GetMousePositionRaw();
        }

        public override void OnMouseReleased(UIEvent e, Input input)
        {
            this.mousePos = null;
        }

        public override void OnMouseHold(UIEvent e, Input input)
        {
            if (!Movable || !mousePos.HasValue)
            {
                return;
            }

            this.Position += input.GetMousePositionRaw() - mousePos.Value;
            this.mousePos = input.GetMousePositionRaw();
            e.PreventPropagation();
            this.ChangeState();
        }

        public override void OnMouseClickSecondary(UIEvent e, Input input)
        {
            startMousePosSecondary = mousePosSecondary = input.GetMousePositionRaw();
        }

        public override void OnMouseReleasedSecondary(UIEvent e, Input input)
        {
            startMousePosSecondary = mousePosSecondary = null;
        }

        public override void OnMouseHoldSecondary(UIEvent e, Input input)
        {
            if (!Resizable || !startMousePosSecondary.HasValue || !mousePosSecondary.HasValue)
            {
                return;
            }

            // Distance to the edges, so we can decide the direction to resize
            var left = startMousePosSecondary.Value.X - windowArea.TopLeft.X;
            var right = windowArea.BottomRight.X - startMousePosSecondary.Value.X;
            var top = startMousePosSecondary.Value.Y - windowArea.TopLeft.Y;
            var bottom = windowArea.BottomRight.Y - startMousePosSecondary.Value.Y;

            // The mouse movement since the last frame
            var diff = input.GetMousePositionRaw() - this.mousePosSecondary.Value;

            // Analyze the mouse movement based on the edge it's touching
            // and check if we can still grow/shrink
            var leftCond = diff.X > 0 ? canShrinkWidth() : canGrowWidth();
            var rightCond = diff.X > 0 ? canGrowWidth() : canShrinkWidth();
            var topCond = diff.Y > 0 ? canShrinkHeight() : canGrowHeight();
            var bottomCond = diff.Y > 0 ? canGrowHeight() : canShrinkHeight();

            // Pick nearest border
            if (left < right)
            {
                if (leftCond)
                {
                    this.width -= diff.X;
                    this.position.X += diff.X * this.Alignment.X;
                }
            }
            else if (rightCond)
            {
                this.width += diff.X;
                this.position.X += diff.X * this.Alignment.X;
            }

            // Pick nearest border
            if (top < bottom)
            {
                if (topCond)
                {
                    this.height -= diff.Y;
                    this.position.Y += diff.Y * this.Alignment.Y;
                }
            }
            else if (bottomCond)
            {
                this.height += diff.Y;
                this.position.Y += diff.Y * this.Alignment.Y;
            }

            this.mousePosSecondary = input.GetMousePositionRaw();
            e.PreventPropagation();
            this.ChangeState();
        }

        protected override IUILayout Build()
        {
            windowArea = this.Alignment.Apply(
                this.AvailableArea,
                new TextureSlice(
                    (Point)this.Position,
                    (int)width,
                    (int)height
                )
            );

            this.background.AvailableArea = windowArea;

            return background.Layout;
        }

        private bool canShrinkWidth()
        {
            return !minSize.HasValue || minSize.Value.X < width;
        }

        private bool canShrinkHeight()
        {
            return !minSize.HasValue || minSize.Value.Y < height;
        }

        private bool canGrowHeight()
        {
            return !maxSize.HasValue || maxSize.Value.X > width;
        }

        private bool canGrowWidth()
        {
            return !maxSize.HasValue || maxSize.Value.Y > height;
        }
    }
}