using System.Collections.Generic;
namespace Crystal.Framework.Collections.Specialized
{
    public static class LinkedListNodeExtensions
    {
        public static T Val<T>(this LinkedListNode<T> self)
        {
            if (self == null)
            {
                return default(T);
            }

            return self.Value;
        }
    }
}