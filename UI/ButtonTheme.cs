using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI
{
    public struct ButtonTheme
    {
        public readonly ThreePatchImage Default;
        public readonly ThreePatchImage OnHover;

        public ButtonTheme(ThreePatchImage def, ThreePatchImage onHover)
        {
            this.Default = def;
            this.OnHover = onHover;
        }
    }
}