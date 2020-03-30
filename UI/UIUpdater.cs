using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.UI
{
    public class UIUpdater
    {
        /// <summary>
        /// Subtree for the widgets that are focused
        /// </summary>
        private LinkedList<Widget> focusedWidgets = new LinkedList<Widget>();

        /// <summary>
        /// Widgets that are under the mouse
        /// </summary>
        private LinkedList<Widget> underMouse = new LinkedList<Widget>();

        /// <summary>
        /// Updates focus and fires events on UI
        /// </summary>
        /// <param name="widgets">The widgets to update</param>
        public void Update(IEnumerable<Widget> widgets, Input input)
        {
            widgets = widgets.OrderBy(w => w.HasFocus ? 1 : 0);
            var currUnderMouse = pathUnderMouse(widgets.Reverse(), input).ToList();

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

            Input.Instance.IsMouseOverUI = underMouse.Any();

            // TODO: Widgets consume keys input

            underMouse = hoverOn(
                input,
                currUnderMouse,
                underMouse
            );
        }

        /// <summary>
        /// Returns the widget subtree that begins on one of the informed
        /// widget and goes to the widget that is currently under the mouse
        /// </summary>
        /// <param name="widgets">Widgets in focus order</param>
        /// <param name="input">Object to query for mouse position information</param>
        /// <returns></returns>
        private IEnumerable<Widget> pathUnderMouse(IEnumerable<Widget> widgets, Input input)
        {
            var mousePos = input.MousePosition;

            var widget = widgets.Where(w => w.Layout.Area.Contains(mousePos)).FirstOrDefault();

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
        private LinkedList<Widget> clickOn(Input input,
                                           IEnumerable<Widget> path,
                                           LinkedList<Widget> currentFocus)
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
                    if (focus != null && focus.Value == widget)
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
                    if (under != null && under.Value == widget)
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
    }
}