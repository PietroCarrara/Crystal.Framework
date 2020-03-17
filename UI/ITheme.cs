using System.Collections.Generic;
using Crystal.Framework.Content;

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
        /// The available fonts in the theme
        /// </summary>
        Dictionary<string, IFont> Fonts { get; }
    }
}