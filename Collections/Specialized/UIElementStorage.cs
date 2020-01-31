using System;
using System.Collections;
using System.Collections.Generic;
using Crystal.Framework.UI;

namespace Crystal.Framework.Collections.Specialized
{
    internal class UIElementStorage : IEnumerable<IUIElement>
    {
        private List<IUIElement> data = new List<IUIElement>();
        
        public void Add(IUIElement element)
        {
            data.Add(element);
        }
        
        public IEnumerator<IUIElement> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}