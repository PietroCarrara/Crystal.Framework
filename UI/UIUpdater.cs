using System;
using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.UI.Widgets;
using Crystal.Framework.UI.UILayouts;

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

        public bool FocusedUIStealsInput()
        {
            foreach (var widget in focusedWidgets)
            {
                if (widget.StealsInput)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Updates focus and fires events on UI
        /// </summary>
        /// <param name="widgets">The widgets to update</param>
        public void Update(UnorderedWidgetsLayout ui, Input input)
        {
            var widgets = ui.Children;
            var currUnderMouse = pathUnderMouse(widgets.Reverse(), input).ToList();

            input.IsMouseOverUI = underMouse.Any();

            if (input.IsButtonPressed(Buttons.MouseLeft) || input.IsButtonPressed(Buttons.MouseRight))
            {
                focusedWidgets = giveFocusTo(
                    input,
                    currUnderMouse,
                    focusedWidgets
                );
            }

            updateButtonEvents(input, focusedWidgets.First);

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
        /// and removes focus from the widgets that should lose it.
        /// </summary>
        /// <param name="input">Object to query input information</param>
        /// <param name="path">The widgets to give focus</param>
        /// <param name="currentFocus">Returns a list of the widgets that are now in focus</param>
        /// <returns>The list of widgets that now have focus</returns>
        private LinkedList<Widget> giveFocusTo(Input input, IEnumerable<Widget> path, LinkedList<Widget> currentFocus)
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

                if (!samePath)
                {
                    widget.HasFocus = true;
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

        /// <summary>
        /// Fires events related to mouse buttons
        /// </summary>
        /// <param name="input">Object to query for input information</param>
        /// <param name="widgetList">The widgets to fire the events</param>
        /// <returns>Returns true when no events were consumed</returns>
        private void updateButtonEvents(Input input, LinkedListNode<Widget> widgetList)
        {
            // Mouse buttons
            if (input.IsButtonPressed(Buttons.MouseLeft))
            {
                updateUIEvent(widgetList, (w, e) => w.OnMouseClick(e, input));
            }
            if (input.IsButtonPressed(Buttons.MouseRight))
            {
                updateUIEvent(widgetList, (w, e) => w.OnMouseClickSecondary(e, input));
            }
            if (input.IsButtonDown(Buttons.MouseLeft))
            {
                updateUIEvent(widgetList, (w, e) => w.OnMouseHold(e, input));
            }
            if (input.IsButtonDown(Buttons.MouseRight))
            {
                updateUIEvent(widgetList, (w, e) => w.OnMouseHoldSecondary(e, input));
            }
            if (input.IsButtonReleased(Buttons.MouseLeft))
            {
                updateUIEvent(widgetList, (w, e) => w.OnMouseReleased(e, input));
            }
            if (input.IsButtonReleased(Buttons.MouseRight))
            {
                updateUIEvent(widgetList, (w, e) => w.OnMouseReleasedSecondary(e, input));
            }

            // Keyboard text
            var text = input.GetText();
            if (text.Any())
            {
                updateUIEvent(widgetList, (w, e) => w.OnText(e, text));
            }

            // Keyboard non-text keys
            var keys = input.GetKeysPressed();
            if (keys.Any())
            {
                updateUIEvent(widgetList, (w, e) => w.OnKey(e, keys));
            }
        }

        /// <summary>
        /// Updates recursively a event in the list of widgets
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        /// <returns>True when the UIEvent if still free to propagate</returns>
        private bool updateUIEvent(LinkedListNode<Widget> node, Action<Widget, UIEvent> action)
        {
            if (node != null)
            {
                if (!updateUIEvent(node.Next, action))
                {
                    return false;
                }

                var e = new UIEvent();
                action(node.Value, e);
                return !e.ShouldPreventPropagation;
            }

            return true;
        }

        /// <summary>
        /// Fires events related to mouse hovering
        /// </summary>
        /// <param name="input">Object to query for input information</param>
        /// <param name="path">Objects under the mouse now</param>
        /// <param name="currentUnder">Objects previously under the mouse</param>
        /// <returns>The list of objects under the mouse</returns>
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
                    widget.OnMouseEnter(input);
                }

                newUnderList.AddLast(widget);
            }

            while (under != null)
            {
                under.Value.OnMouseLeave(input);

                under = under.Next;
            }

            return newUnderList;
        }
    }
}