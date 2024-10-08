using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtusDictionary
{
    public class OtusDictionary
    {
        private int _currentIndex;
        private int _size;
        private int[] _keys;
        private string[] _values;
        public OtusDictionary()
        {
            _size = 32;
            _keys = new int[_size];
            _values = new string[_size];
            _currentIndex = 0;
        }

        public void Add(int key, string? value)
        {
            if (value is null) throw new ArgumentNullException("Value must not be null!");

            if (_keys.Contains(key)) throw new Exception($"{key} already exsist!");

            _currentIndex = GetHash(key);

            if (_currentIndex >= _size) Resize();

            if (_values[_currentIndex] != null) GetAnotherIndex();

            _keys[_currentIndex] = key;
            _values[_currentIndex] = value;
        }

        public string Get(int key)
        {
            _currentIndex = Array.IndexOf(_keys, key);
            if (_currentIndex < 0)
            {
                throw new ArgumentOutOfRangeException($"Argument - [{key}], out of range!");
            }
            return _values[_currentIndex];
        }

        public int GetHash(int key)
        {
            return key % _size;
        }

        public void Resize()
        {
            _size = _size * 2;
            int[] tmpKeys = _keys;
            string[] tmpValues = _values;
            _keys = new int[_size];
            _values = new string[_size];

            for (int i=0; i < tmpKeys.Length; i++)
            {
                _currentIndex = GetHash(tmpKeys[i]);
                _keys[_currentIndex] = tmpKeys[i];
                _values[_currentIndex] = tmpValues[i];
            }
        }

        public int GetAnotherIndex()
        {
            _currentIndex = 0;
            while (_values[_currentIndex] != null)
            {
                _currentIndex++;
                if (_currentIndex >= _size)
                {
                    Resize();
                }
            }
            return (_currentIndex - 1);
        }

        public string? this[int index]
        {
            get
            {
                if (_size <= index)
                {
                    throw new IndexOutOfRangeException();
                }
                else 
                {
                    if (string.IsNullOrEmpty(_values[index])) throw new Exception($"No value by index - {index}!");
                    else return _values[index];
                }
            }
            set => Add(index, value);
        }
    }
}
