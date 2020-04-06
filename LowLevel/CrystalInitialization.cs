using Crystal.Framework.Graphics;

namespace Crystal.Framework.LowLevel
{
    /// <summary>
    /// TODO: Remove
    /// </summary>
    public static class CrystalInitialization
    {
        /// <summary>
        /// Initializes all the needed parts of the framework
        /// Must be run before doing anything
        /// </summary>
        public static void Execute(
            CanvasFactory canvasFactory,
            IScaler scaler,
            Input input
        )
        {
            CanvasFactory.Instance = canvasFactory;
            Scaler.Instance = scaler;
        }
    }
}