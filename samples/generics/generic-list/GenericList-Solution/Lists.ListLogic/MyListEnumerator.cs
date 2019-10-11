using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lists.ListLogic
{
    internal class MyListEnumerator<T> : IEnumerator<T>
        where T: IComparable<T>
    {
        private Node<T> _head;
        private Node<T> _actualNode;
        private bool _isReset;
        public MyListEnumerator(Node<T> head)
        {
            _head = head;
            Reset();
        }

        public bool MoveNext()
        {
            if (_isReset)
            {
                _actualNode = _head;
                _isReset = false;
            }
            else
            {
                _actualNode = _actualNode.Next;
            }
            return _actualNode != null;
        }

        public void Reset()
        {
            _actualNode = null;
            _isReset = true;
        }

        object IEnumerator.Current => Current;

        public T Current { get { return _actualNode.DataObject; } }
        public void Dispose()
        {
            // nichts zu disposen
        }
    }
}