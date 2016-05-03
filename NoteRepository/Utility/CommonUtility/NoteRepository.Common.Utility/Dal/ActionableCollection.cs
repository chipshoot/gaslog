using System;
using System.Collections;

namespace NoteRepository.Common.Utility.Dal
{
    /// <summary>
    /// The list is used for apply action on element when add it to list,
    /// the point is this list just deal with element and don't save any
    /// actual element to memory, this way it can mimic forward only data reader
    /// and reduce memory consumer
    /// </summary>
    public class ActionableCollection<T> : IList
    {
        #region private fields

        private readonly Action<T> _action;

        #endregion private fields

        #region constructor

        public ActionableCollection(Action<T> action)
        {
            _action = action;
        }

        #endregion constructor

        public int Add(object value)
        {
            _action((T)value);
            Count++;
            return -1;
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public object this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool IsReadOnly { get { return true; } }

        public bool IsFixedSize { get; private set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count { get; private set; }

        public object SyncRoot { get; private set; }

        public bool IsSynchronized { get; private set; }
    }
}