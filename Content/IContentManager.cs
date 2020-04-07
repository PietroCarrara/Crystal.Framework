using System;

namespace Crystal.Framework.Content
{
    /// <summary>
    /// A manager that can load assets directly from the filesystem
    /// Use when you need to dinamically load content into your game.
    ///
    /// Otherwise, you are better using scene.Resource()
    /// </summary>
    public interface IContentManager : IDisposable
    {
        /// <summary>
        /// Load an asset
        /// </summary>
        /// <param name="path">Path to the asset</param>
        /// <param name="manage">Should the content manager handle disposing?</param>
        /// <typeparam name="T">Type of the object to be loaded</typeparam>
        /// <returns>The loaded resource</returns>
        T Load<T>(string path, bool manage = true) where T : IDisposable;
    }
}