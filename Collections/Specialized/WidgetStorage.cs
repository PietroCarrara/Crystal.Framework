using System;
using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.Collections.Specialized
{
    public class WidgetStorage
    {
        /// <summary>
        /// Subtree for the widgets that are focused
        /// </summary>
        private LinkedList<Widget> focusedWidgets = new LinkedList<Widget>();

        /// <summary>
        /// Widgets that are under the mouse
        /// </summary>
        private LinkedList<Widget> underMouse = new LinkedList<Widget>();

        public StackingContainer Root { get; private set; }

        public WidgetStorage()
        {
            Root = new StackingContainer();
        }

        /// <summary>
        /// Update focus and fire input events
        /// </summary>
        /// <param name="input">The object to query for input information</param>
        public void UpdateInput(Input input)
        {
            updateInput(input, this.Root);
        }

        private void updateInput(Input input, Widget root)
        {
            var currUnderMouse = pathUnderMouse(root, input).ToList();

            if (input.IsButtonDown(Buttons.MouseLeft) ||
                input.IsButtonDown(Buttons.MouseRight) ||
                input.IsButtonReleased(Buttons.MouseLeft) ||
                input.IsButtonReleased(Buttons.MouseRight))
            {
                focusedWidgets = clickOn(
                    input,
                    currUnderMouse,
                    focusedWidgets
                );
            }

            underMouse = hoverOn(
                input,
                currUnderMouse,
                underMouse
            );
        }

        /// <summary>
        /// Returns the widget subtree that begins on root and
        /// goes to the widget that is currently under the mouse
        /// </summary>
        private IEnumerable<Widget> pathUnderMouse(Widget root, Input input)
        {
            var widget = root;
            var mousePos = input.MousePosition;

            while (widget != null)
            {
                yield return widget;

                widget.Layout.Match(
                    o => widget = o.Children.Reverse().Where(w => w.Layout.Area.Contains(mousePos)).FirstOrDefault(),
                    w => widget = null,
                    w => widget = null,
                    w => widget = null,
                    u => widget = u.Children.Reverse().Where(w => w.Layout.Area.Contains(mousePos)).FirstOrDefault()
                );
            }
        }

        /// <summary>
        /// Given a path of widgets, gives focus to all of the widgets in the path,
        /// triggers click events removes focus from the widgets that should lose it,
        /// and returns a list of the current focused widgets
        /// </summary>
        /// <param name="path">The path which should gain focus</param>
        /// <param name="currentFocus">Who has focus before this function is called</param>
        private LinkedList<Widget> clickOn(Input input, IEnumerable<Widget> path, LinkedList<Widget> currentFocus)
        {
            var newFocusList = new LinkedList<Widget>();
            var focus = currentFocus.First;

            // Wether the path to the new focus is the same as the old focus
            var samePath = true;

            foreach (var widget in path)
            {
                // If we are still walking down the same path...
                if (samePath)
                {
                    // Check to see if this is still the same path
                    if (focus.Val() == widget)
                    {
                        focus = focus.Next;
                    }
                    else
                    {
                        samePath = false;
                    }
                }

                widget.HasFocus = true;

                if (input.IsButtonPressed(Buttons.MouseLeft))
                {
                    widget.OnMouseClick();
                }
                if (input.IsButtonPressed(Buttons.MouseRight))
                {
                    widget.OnMouseClickSecondary();
                }
                if (input.IsButtonDown(Buttons.MouseLeft))
                {
                    widget.OnMouseHold();
                }
                if (input.IsButtonDown(Buttons.MouseRight))
                {
                    widget.OnMouseHoldSecondary();
                }
                if (input.IsButtonReleased(Buttons.MouseLeft))
                {
                    widget.OnMouseReleased();
                }
                if (input.IsButtonReleased(Buttons.MouseRight))
                {
                    widget.OnMouseReleasedSecondary();
                }

                newFocusList.AddLast(widget);
            }

            while (focus != null)
            {
                focus.Value.HasFocus = false;

                focus = focus.Next;
            }

            return newFocusList;
        }

        private LinkedList<Widget> hoverOn(Input input, IEnumerable<Widget> path, LinkedList<Widget> currentUnder)
        {
            var newUnderList = new LinkedList<Widget>();
            var under = currentUnder.First;

            // Wether the path to the new focus is the same as the old focus
            var samePath = true;

            foreach (var widget in path)
            {
                // If we are still walking down the same path...
                if (samePath)
                {
                    // Check to see if this is still the same path
                    if (under.Val() == widget)
                    {
                        under = under.Next;
                    }
                    else
                    {
                        samePath = false;
                    }
                }

                if (!samePath)
                {
                    widget.OnMouseEnter();
                }

                newUnderList.AddLast(widget);
            }

            while (under != null)
            {
                under.Value.OnMouseLeave();

                under = under.Next;
            }

            return newUnderList;
        }

        /// <summary>
        /// Adds a widget to the pool
        /// </summary>
        /// <param name="widget">The widget to add</param>
        public void Add(Widget widget)
        {
            this.Root.Add(widget);
        }
    }
}