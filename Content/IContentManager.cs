
namespace Crystal.Framework.Content
{
    /// <summary>
    /// A manager that can load assets directly from the filesystem
    /// Use when you need to dinamically load content into your game.
    /// 
    /// Otherwise, you are better using scene.Resource()
    /// </summary>
    public interface IContentManager
    {
        T Load<T>(string path);
    }
}