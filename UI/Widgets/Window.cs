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

        public Window()
        {
            this.background = new Panel();
            background.BecomeChildOf(this);
        }

        private Vector2 mousePos,
                        mousePosSecondary,
                        startMousePosSecondary;

        public override void OnMouseClick()
        {
            this.mousePos = Input.Instance.MousePosition;
        }

        public override void OnMouseHold()
        {
            if (!Movable)
            {
                return;
            }

            this.Position += Input.Instance.MousePosition - mousePos;
            this.mousePos = Input.Instance.MousePosition;
            this.ChangeState();
        }

        public override void OnMouseClickSecondary()
        {
            startMousePosSecondary = mousePosSecondary = Input.Instance.MousePosition;
        }

        public override void OnMouseHoldSecondary()
        {
            if (!Resizable)
            {
                return;
            }

            // Distance to the edges, so we can decide the direction to resize
            var left = startMousePosSecondary.X - windowArea.TopLeft.X;
            var right = windowArea.BottomRight.X - startMousePosSecondary.X;
            var top = startMousePosSecondary.Y - windowArea.TopLeft.Y;
            var bottom = windowArea.BottomRight.Y - startMousePosSecondary.Y;

            // The mouse movement since the last frame
            var diff = Input.Instance.MousePosition - this.mousePosSecondary;

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

            this.mousePosSecondary = Input.Instance.MousePosition;
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