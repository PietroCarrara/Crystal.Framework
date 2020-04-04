namespace Crystal.Framework.UI
{
    public class TextInputData
    {
        /// <summary>
        /// Character representaion of the input key
        /// </summary>
        public readonly char Character;

        /// <summary>
        /// The button that was pressed to produce this key
        /// </summary>
        public readonly Buttons Button;

        public TextInputData(char character, Buttons button)
        {
            this.Character = character;
            this.Button = button;
        }
    }
}