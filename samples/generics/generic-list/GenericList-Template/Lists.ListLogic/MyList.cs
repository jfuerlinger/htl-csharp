using System;
using System.Collections;
using System.Collections.Generic;

namespace Lists.ListLogic
{
    /// <summary>
    /// Die Liste verwaltet beliebige Elemente und implementiert
    /// das IList-Interface und damit auch ICollection und IEnumerable
    /// </summary>
    public class MyList<T> : IList<T>
        where T : IComparable<T>
    {
        private Node<T> _head;

        #region IList Members

        /// <summary>
        /// Ein neues Objekt wird in die Liste am Ende
        /// eingefügt. Etwaige Typinkompatiblitäten beim Vergleich werden
        /// nicht behandelt und lösen eine Exception aus.
        /// </summary>
        /// <param name="value">Einzufügender Datensatz</param>
        /// <returns>Index des Werts in der Liste</returns>
        public void Add(T value)
        {
            Node<T> insertNode = new Node<T>(value);
            if (_head == null)
            {
                _head = insertNode;
                return;
            }
            Node<T> searchNode = _head;
            while (searchNode.Next != null)
            {
                searchNode = searchNode.Next;
            }
            searchNode.Next = insertNode;
        }

        /// <summary>
        /// Die Liste wird geleert. Die Elemente werden einfach ausgekettet.
        /// Der GC räumt dann schon auf.
        /// </summary>
        public void Clear()
        {
            _head = null;
        }

        /// <summary>
        /// Gibt es den gesuchten DataObject zumindest ein mal?
        /// </summary>
        /// <param name="value">gesuchter DataObject</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        /// <summary>
        /// Der DataObject wird gesucht und dessen Index wird zurückgegeben.
        /// </summary>
        /// <param name="value">gesuchter DataObject</param>
        /// <returns>Index oder -1, falls der DataObject nicht in der Liste ist</returns>
        public int IndexOf(T item)
        {
            Node<T> searchNode = _head;
            int index = 0;
            while (searchNode != null && !searchNode.DataObject.Equals(item))
            {
                searchNode = searchNode.Next;
                index++;
            }
            if (searchNode == null)
            {
                return -1;
            }
            return index;
        }

        /// <summary>
        /// DataObject an bestimmter Position in Liste einfügen.
        /// Es ist auch erlaubt, einen DataObject hinter dem letzten Element
        /// (index == count) einzufügen.
        /// </summary>
        /// <param name="index">Einfügeposition</param>
        /// <param name="value">Einzufügender DataObject</param>
        public void Insert(int index, T value)
        {
            if (index > Count || index < 0)
            {
                return;
            }
            Node<T> newNode = new Node<T>(value);
            if (index == 0)
            {
                newNode.Next = _head;
                _head = newNode;
                return;
            }
            Node<T> searchNode = _head;
            for (int i = 1; i < index; i++)
            {
                searchNode = searchNode.Next;
            }
            newNode.Next = searchNode.Next;
            searchNode.Next = newNode;
        }

        /// <summary>
        /// Wird nicht verwendet ==> Immer false
        /// </summary>
        public bool IsFixedSize
        {
            get { return false; }
        }

        /// <summary>
        /// Wird nicht verwendet ==> Immer false
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Ein DataObject wird aus der Liste entfernt, wenn es ihn 
        /// zumindest ein mal gibt.
        /// </summary>
        /// <param name="value">zu entfernender DataObject</param>
        public bool Remove(T value)
        {
            int index = IndexOf(value);
            if (index < 0)
            {
                return false;
            }
            RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Der DataObject an der Position Index wird entfernt.
        /// Gibt es die Position nicht, geschieht nichts.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (index >= Count || index < 0)
            {
                return;
            }
            if (index == 0)
            {
                _head = _head.Next;
                return;
            }
            Node<T> searchNode = _head;
            for (int i = 1; i < index; i++)
            {
                searchNode = searchNode.Next;
            }
            searchNode.Next = searchNode.Next.Next;
        }

        /// <summary>
        /// Indexer zum Einfügen und Lesen von Werten an
        /// gesuchten Positionen.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    return default(T);
                }
                Node<T> searchNode = _head;
                for (int i = 0; i < index; i++)
                {
                    searchNode = searchNode.Next;
                }
                return searchNode.DataObject;
            }
            set
            {
                Insert(index, value);
            }
        }

        #endregion

        #region ICollection Members

        /// <summary>
        /// Werte in ein bereits angelegtes Array kopieren.
        /// Ist das übergebene Array zu klein, oder stimmt der
        /// Startindex nicht, geschieht nichts.
        /// </summary>
        /// <param name="array">Zielarray, existiert bereits</param>
        /// <param name="index">Startindex</param>
        public void CopyTo(T[] array, int index)
        {
            if (array.Length < Count - index)
            {
                return;
            }
            Node<T> searchNode = _head;
            for (int i = 0; i < index; i++)
            {
                searchNode = searchNode.Next;
            }
            int targetIndex = 0;
            while (searchNode != null)
            {
                array.SetValue(searchNode.DataObject, targetIndex);
                searchNode = searchNode.Next;
                targetIndex++;
            }
        }

        /// <summary>
        /// Die Anzahl von Elementen in der Liste wird immer 
        /// explizit gezählt und ist nicht redundant gespeichert.
        /// </summary>
        public int Count
        {
            get
            {
                int counter = 0;
                Node<T> searchNode = _head;
                while (searchNode != null)
                {
                    searchNode = searchNode.Next;
                    counter++;
                }
                return counter;
            }
        }

        /// <summary>
        /// Multithreading wird nicht verwendet
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// Multithreading wird nicht verwendet
        /// </summary>
        public object SyncRoot
        {
            get { return null; }
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            return new MyListEnumerator<T>(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region Additional Methods

        public void Sort(IComparer<T> comparer = null)
        {
            if (_head == null)
            {
                return;
            }

            // Kopiert alle Nodes in ein Array zur leichteren Sortierung
            Node<T>[] sortArray = new Node<T>[Count];
            Node<T> currentNode = _head;
            int idx = 0;

            while (currentNode != null)
            {
                sortArray[idx++] = currentNode;
                currentNode = currentNode.Next;
            }

            // Sortiert das Array ohne, dass die Next-Zeiger verändert werden
            for (int i = 0; i < sortArray.Length; i++)
            {
                bool swapped = false;
                for (int j = 0; j < sortArray.Length - i - 1; j++)
                {
                    int compareResult;

                    if (comparer != null)
                    {
                        // Wenn ein Comparer-Objekt für den Objektvergleich übergeben wurde, so wird dieser verwendet.
                        compareResult = comparer.Compare(sortArray[j].DataObject, sortArray[j + 1].DataObject);
                    }
                    else
                    {
                        // Wenn kein Comparer-Objekt für den Objektvergleich übergeben wurde, so wird mit der natürlichen Sortierung gearbeitet.
                        // Voraussetzung dafür ist, dass T das Interface IComparable<T> implementiert!
                        // Dies ist durch folgenden Constraint sichergestellt:
                        //      "where T : IComparable<T>" (siehe MyList-Klassendefinition)
                        compareResult = sortArray[j].DataObject.CompareTo(sortArray[j + 1].DataObject);
                    }

                    if (compareResult > 0)
                    {
                        SwapArrayCells(sortArray, j, j + 1);
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }

            // Zeiger korrigieren
            for (int i = 0; i < sortArray.Length - 1; i++)
            {
                sortArray[i].Next = sortArray[i + 1];
            }

            _head = sortArray[0];
        }

        private static void SwapArrayCells(Node<T>[] sortArray, int idx1, int idx2)
        {
            Node<T> tempNode;

            tempNode = sortArray[idx1];
            sortArray[idx1] = sortArray[idx2];
            sortArray[idx2] = tempNode;
        }

        #endregion
    }
}
