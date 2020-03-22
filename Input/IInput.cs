using System.Numerics;

namespace Crystal.Framework.Input
{
    public interface IInput
    {
        bool IsActionDown(string action);

        /// <summary>
        /// Tells if a user is pressing a action
        /// </summary>
        /// <param name="action">The action</param>
        bool IsActionUp(string action);

        /// <summary>
        /// Tells if a action has been pressed this exact frame
        /// </summary>
        /// <param name="action">The action</param>
        bool IsActionPressed(string action);

        /// <summary>
        /// Tells if a action has been released this exact frame
        /// </summary>
        /// <param name="action">The action</param>
        bool IsActionReleased(string action);

        /// <summary>
        /// How strong is the action.
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>
        /// Returns a value in range [0, 1] if the mouse wheel is not present in the action.
        /// For actions that are binary (true or false), returns 0 or 1
        /// </returns>
        float GetActionStrength(string action);

        /// <summary>
        /// Returns the position of the mouse relative to the screen
        /// </summary>
        Vector2 GetMousePosition();

        /// <summary>
        /// Updates input state
        /// </summary>
        void Update();
    }
}