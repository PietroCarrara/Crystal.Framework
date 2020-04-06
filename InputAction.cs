namespace Crystal.Framework
{
    /// <summary>
    /// An abstraction for input handling that allows the association of a name
    /// to a combination of buttons
    /// </summary>
    public class InputAction
    {
        public readonly string Name;

        public readonly Buttons[] Buttons;

        public InputAction(string name, params Buttons[] buttons)
        {
            this.Name = name;
            this.Buttons = buttons;
        }

        /// <summary>
        /// Tells if all of the buttons are up this frame
        /// </summary>
        public bool IsUp(Input input)
        {
            foreach (var button in this.Buttons)
            {
                if (!input.IsButtonUp(button))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tells if all of the buttons were up last frame
        /// </summary>
        public bool WasUp(Input input)
        {
            foreach (var button in this.Buttons)
            {
                if (!input.WasButtonUp(button))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tells if all of the buttons are down this frame
        /// </summary>
        public bool IsDown(Input input)
        {
            foreach (var button in this.Buttons)
            {
                if (!input.IsButtonDown(button))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tells if all of the buttons were down last frame
        /// </summary>
        public bool WasDown(Input input)
        {
            foreach (var button in this.Buttons)
            {
                if (!input.WasButtonDown(button))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tells if the actions has just been pressed
        /// (All of the buttons are down this frame and at least one was up last frame)
        /// </summary>
        public bool IsPressed(Input input)
        {
            foreach (var bt in Buttons)
            {
                if (input.WasButtonUp(bt))
                {
                    return IsDown(input);
                }
            }

            return false;
        }

        /// <summary>
        /// Tells if the action has just been released
        /// (All of the buttons were down last frame and at least one is up this frame)
        /// </summary>
        public bool IsReleased(Input input)
        {
            foreach (var bt in Buttons)
            {
                if (input.IsButtonUp(bt))
                {
                    return WasDown(input);
                }
            }

            return false;
        }
    }
}