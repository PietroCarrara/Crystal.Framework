using System.Collections;
using System.Collections.Generic;

namespace Crystal.ECS.Collections.Specialized
{
    public class SystemStorage : IEnumerable<ISystem>
    {
        private List<ISystem> data = new List<ISystem>();
        
        public IEnumerator<ISystem> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public void Add(ISystem s)
        {
            this.data.Add(s);
        }
    }
}