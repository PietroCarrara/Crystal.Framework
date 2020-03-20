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
        /// <value></value>
        NinePatchImage PanelBackground { get; } 
        
        /// <summary>
        /// The available fonts in the theme
        /// </summary>
        Dictionary<string, IFont> Fonts { get; }

        /// <summary>
        /// The available panels in the theme
        /// </summary>
        Dictionary<string, NinePatchImage> PanelBackgrounds { get; }
    }
}