
namespace Crystal.Framework.LowLevel
{
    public static class CrystalInitialization
    {
        /// <summary>
        /// Initializes all the needed parts of the framework
        /// Must be run before doing anything
        /// </summary>
        public static void Execute(
            CanvasFactory canvasFactory
        )
        {
            CanvasFactory.Instance = canvasFactory;
        }
    }
}