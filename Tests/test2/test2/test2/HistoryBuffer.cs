using System;
using System.Collections.Generic;
using System.Linq;

namespace test2
{
    /// <summary>
    /// Used to store a Circular Buffer of objects with a particular size, that rotates when an item is added to the collection.
    /// </summary>
    public class HistoryBuffer<T>
    {
        private int _addedCount = 0;
        private int _capacity;
        public int Capacity { get { return _capacity; } }
        private T[] _buffer;
        private int _startIndex;

        public HistoryBuffer(int capacity)
        {
            _capacity = capacity;
            _buffer = new T[capacity];
            _startIndex = 0;
        }

        public T ElementAt(int index)
        {
            if ((index >= _buffer.Length) || (index < 0))
                throw new IndexOutOfRangeException();

            index += _startIndex;

            if (index >= _buffer.Length)
                index -= _buffer.Length;

            return _buffer[index];
        }

        public T[] ToArray()
        {
            int i;
            T[] output = new T[_buffer.Length];

            for (i = 0; i < _buffer.Length; i++)
            {
                output[i] = ElementAt(i);
            }

            return output;
        }

        public void Add(T newItem)
        {
            if (!AtCapacity)
                _addedCount++;

            if (_startIndex == 0)
                _startIndex = _buffer.Length - 1;
            else
                _startIndex--;
            _buffer[_startIndex] = newItem;
        }

        public bool AtCapacity { get { return _addedCount >= _capacity; } }

        public T Sum()
        {
            if (!AtCapacity)
                return default(T);
            dynamic sum = 0;
            for (int i = 0; i < _capacity; i++)
                sum += _buffer[i];
            return sum;
        }
        public T Avg()
        {
            if (!AtCapacity)
                return default(T);
            dynamic sum = Sum();
            return sum / _capacity;
        }
    }
}
