using System.Collections.Generic;
using Crystal.Framework.Content;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI
{
    public interface ITheme
    {
        /// <summary>
        /// Perform the theme load
        /// </summary>
        void Load(IContentManager cm);

        /// <summary>
        /// The default small font of the theme
        /// </summary>
        IFont SmallFont { get; }

        /// <summary>
        /// The default medium font of the theme
        /// </summary>
        IFont MediumFont { get; }

        /// <summary>
        /// The default big font of the theme
        /// </summary>
        IFont BigFont { get; }

        /// <summary>
        /// The default panel background
        /// </summary>
        NinePatchImage PanelBackground { get; }

        /// <summary>
        /// The default button theme
        /// </summary>
        /// <value></value>
        ButtonTheme ButtonTheme { get; }

        /// <summary>
        /// The default slider theme
        /// </summary>
        /// <value></value>
        SliderTheme SliderTheme { get; }

        /// <summary>
        /// The available fonts in the theme
        /// </summary>
        Dictionary<string, IFont> Fonts { get; }

        /// <summary>
        /// The available panels in the theme
        /// </summary>
        Dictionary<string, NinePatchImage> PanelBackgrounds { get; }

        /// <summary>
        /// The available button themes in the theme
        /// </summary>
        Dictionary<string, ButtonTheme> ButtonThemes { get; }

        /// <summary>
        /// The available button themes in the theme
        /// </summary>
        Dictionary<string, SliderTheme> SliderThemes { get; }
    }
}