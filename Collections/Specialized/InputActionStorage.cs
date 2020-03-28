using System.Collections;
using System.Collections.Generic;
namespace Crystal.Framework.Collections.Specialized
{
    public class InputActionStorage : IEnumerable<KeyValuePair<string, InputAction>>
    {
        private Dictionary<string, InputAction> data = new Dictionary<string, InputAction>();

        public void Add(InputAction action)
        {
            this.data.Add(
                action.Name,
                action
            );
        }

        public InputAction this[string index]
        {
            get => data[index];
        }

        public IEnumerator<KeyValuePair<string, InputAction>> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}