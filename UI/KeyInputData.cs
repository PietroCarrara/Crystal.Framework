namespace Crystal.Framework.UI
{
    public class KeyInputData
    {
        /// <summary>
        /// The pressed key
        /// </summary>
        public readonly Buttons Key = Buttons.None;

        public KeyInputData(Buttons key)
        {
            this.Key = key;
        }
    }
}