using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI
{
    public interface ISkin
    {
        /// <summary>
        /// Fonts ordered by size
        /// </summary>
        IFont[] Fonts { get; }

        /// <summary>
        /// Background used for panels
        /// </summary>
        IDrawable PanelBackground { get; }
    }
}