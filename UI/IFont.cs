using System.Numerics;

namespace Crystal.Framework.UI
{
    public interface IFont
    {
        Vector2 MeasureString(string str);
    }
}