using Crystal.Framework.Graphics;

namespace Crystal.Framework.LowLevel
{
    public static class CrystalInitialization
    {
        /// <summary>
        /// Initializes all the needed parts of the framework
        /// Must be run before doing anything
        /// </summary>
        public static void Execute(
            CanvasFactory canvasFactory,
            IScaler scaler
        )
        {
            CanvasFactory.Instance = canvasFactory;
            Scaler.Instance = scaler;
        }
    }
}