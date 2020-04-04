using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Numerics;
using Crystal.Framework.UI;

namespace Crystal.Framework
{
    public abstract class Input
    {
        public static Input Instance { get; internal set; }

        /// <summary>
        /// Tells if the mouse is over any UI object
        /// </summary>
        public bool IsMouseOverUI { get; internal set; }

        /// <summary>
        /// Returns the text data of the keys that were pressed this frame.
        /// This should return info concerning TEXT input, aware of the OS
        /// keyboard layout, key repetition, and such
        /// </summary>
        public abstract IEnumerable<TextInputData> GetText();

        /// <summary>
        /// Returns the keys that were pressed this frame. This should return
        /// info concerning TEXT input, but for keys that do not represent
        /// text, but other controls (shift, control, alt, arrow keys...),
        /// aware of the OS keyboard layout and such. When key repetition
        /// is triggered, this should be reflected here with the same amount
        /// of items
        /// </summary>
        public abstract IEnumerable<KeyInputData> GetKeysPressed();

        /// <summary>
        /// Returns the keys that were released this frame. This should return
        /// info concerning TEXT input, but for keys that do not represent
        /// text, but other controls (shift, control, alt, arrow keys...),
        /// aware of the OS keyboard layout and such
        /// </summary>
        public abstract IEnumerable<KeyInputData> GetKeysReleased();

        /// <summary>
        /// Returns the position of the mouse relative to window
        /// </summary>
        public abstract Vector2 MousePosition { get; }

        /// <summary>
        /// Tells if a button is being pressed this exact frame
        /// </summary>
        /// <param name="button">The button to check</param>
        public abstract bool IsButtonDown(Buttons button);

        /// <summary>
        /// Tells if a button is not being pressed this exact frame
        /// </summary>
        /// <param name="button">The button to check</param>
        public bool IsButtonUp(Buttons button) => !IsButtonDown(button);

        /// <summary>
        /// Tells if a button was being pressed last frame
        /// </summary>
        /// <param name="button">The button to check</param>
        public abstract bool WasButtonDown(Buttons button);

        /// <summary>
        /// Tells if a button was not being pressed last frame
        /// </summary>
        /// <param name="button">The button to check</param>
        public bool WasButtonUp(Buttons button) => !WasButtonDown(button);

        /// <summary>
        /// Tells if a button has just been pressed this frame
        /// </summary>
        /// <param name="button">The button to check</param>
        public bool IsButtonPressed(Buttons button) =>
            WasButtonUp(button) && IsButtonDown(button);

        /// <summary>
        /// Tells if a button has just been released this frame
        /// </summary>
        /// <param name="button">The button to check</param>
        public bool IsButtonReleased(Buttons button) =>
            WasButtonDown(button) && IsButtonUp(button);

        /// <summary>
        /// Updates input state
        /// </summary>
        /// <param name="delta">Time elapsed since last frame</param>
        public abstract void Update(float delta);
    }
}