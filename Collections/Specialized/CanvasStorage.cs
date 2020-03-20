using System;
using System.Collections;
using Crystal.Framework.LowLevel;
using System.Collections.Generic;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.Collections.Specialized
{
    public class CanvasStorage : IEnumerable<Canvas>, IDisposable
    {
        private List<Canvas> data = new List<Canvas>();

        /// <summary>
        /// Creates a canvas that is the size of the window and resizes when the window does
        /// </summary>
        public Canvas Create()
        {
            var c = CanvasFactory.Instance.Create();
            data.Add(c);
            return c;
        }

        /// <summary>
        /// Creates a canvas of the specified size
        /// </summary>
        public Canvas Create(Point size)
        {
            var c = CanvasFactory.Instance.Create(size);
            data.Add(c);
            return c;
        }

        public IEnumerator<Canvas> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Dispose()
        {
            foreach (var canvas in this.data)
            {
                canvas.Dispose();
            }
        }
    }
}