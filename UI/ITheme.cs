using System.Collections.Generic;
using Crystal.Framework.Content;

namespace Crystal.Framework.UI
{
    public interface ITheme
    {
        void Load(IContentManager cm);
        
        Dictionary<string, IFont> Fonts { get; }
    }
}