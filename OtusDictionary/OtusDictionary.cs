using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtusDictionary
{
    public class OtusDictionary
    {
        private int _size;
        private string[] _values;
        public OtusDictionary()
        {
            _size = 32;
            _values = new string[_size];
        }

        public void Add(int key, string? value)
        {
            if (value is null) throw new ArgumentNullException("Value must not be null!");
            if (_values.Contains(value)) throw new Exception($"Value - {value} already exist!");
            int currentIndex = GetHash(key);
            if (currentIndex >= _size) Resize(currentIndex);
            if (_values[currentIndex] != null) currentIndex = GetAnotherIndex(currentIndex);

            _values[currentIndex] = value;
        }

        public string Get(int key)
        {
            int currentIndex = GetHash(key);
            if (currentIndex >= _size)
            {
                throw new ArgumentOutOfRangeException($"Argument - [{key}], out of range!");
            }
            if (_values[currentIndex] == null)
            {
                throw new ArgumentNullException($"Key - [{key}] does not have value!");
            }
            return _values[currentIndex];
        }

        public int GetHash(int key)
        {
            return key % _size;
        }

        public void Resize(int currentIndex)
        {
            _size = _size * 2;
            string[] tmpValues = _values;
            _values = new string[_size];

            for (int i=0; i < tmpValues.Length; i++)
            {
                _values[i] = tmpValues[i];
            }
        }

        public int GetAnotherIndex(int currentIndex)
        {
            int newIndex = currentIndex;
            while (_values[newIndex] != null)
            {
                newIndex++;
                if (newIndex >= _size)
                {
                    Resize(newIndex);
                }
            }
            return (newIndex - 1);
        }

        public string? this[int index]
        {
            get => Get(index);
            set => Add(index, value);
        }
    }
}
