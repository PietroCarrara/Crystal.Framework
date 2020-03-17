using System.Collections.Generic;
using Crystal.Framework.UI.Widgets;

namespace Crystal.Framework.UI
{
    public interface ITheme
    {
        Dictionary<string, IFont> Fonts { get; }
    }
}