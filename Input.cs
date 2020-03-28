using System.Numerics;

namespace Crystal.Framework
{
    public abstract class Input
    {
        public static Input Instance { get; internal set; }
        
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
        public abstract void Update();
    }
}