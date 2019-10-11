using System;

namespace Lists.ListLogic
{
    public class Node<T> 
        where T : IComparable<T>
    {
        public Node(T dataObject)
        {
            if (dataObject == null)
            {
                throw new ArgumentNullException("dataObject");
            }

            DataObject = dataObject;
        }

        public Node<T> Next { get; set; }

        public T DataObject { get; private set; }
    }
}
