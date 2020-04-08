namespace Crystal.Framework.UI
{
    public class KeyInputData
    {
        /// <summary>
        /// The pressed key
        /// </summary>
        public readonly Buttons Key;

        public KeyInputData(Buttons key)
        {
            this.Key = key;
        }
    }
}