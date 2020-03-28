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
        public bool IsUp()
        {
            foreach (var button in this.Buttons)
            {
                if (!Input.Instance.IsButtonUp(button))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tells if all of the buttons were up last frame
        /// </summary>
        public bool WasUp()
        {
            foreach (var button in this.Buttons)
            {
                if (!Input.Instance.WasButtonUp(button))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tells if all of the buttons are down this frame
        /// </summary>
        public bool IsDown()
        {
            foreach (var button in this.Buttons)
            {
                if (!Input.Instance.IsButtonDown(button))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tells if all of the buttons were down last frame
        /// </summary>
        public bool WasDown()
        {
            foreach (var button in this.Buttons)
            {
                if (!Input.Instance.WasButtonDown(button))
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
        public bool IsPressed()
        {
            foreach (var bt in Buttons)
            {
                if (Input.Instance.WasButtonUp(bt))
                {
                    return IsDown();
                }
            }

            return false;
        }

        /// <summary>
        /// Tells if the action has just been released
        /// (All of the buttons were down last frame and at least one is up this frame)
        /// </summary>
        public bool IsReleased()
        {
            foreach (var bt in Buttons)
            {
                if (Input.Instance.IsButtonUp(bt))
                {
                    return WasDown();
                }
            }

            return false;
        }
    }
}