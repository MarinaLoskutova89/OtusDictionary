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
        private KeyValuePair<int, string>[] _keyValuePair;

        public OtusDictionary()
        {
            _size = 32;
            _keyValuePair = new KeyValuePair<int, string>[_size];
        }

        public void Add(int key, string? value)
        {
            if (value is null) throw new ArgumentNullException("Value must not be null!");
            
            KeyValuePair<int, string> newPair = new(key, value);
            
            if (_keyValuePair.Contains(newPair)) throw new Exception($"Input values already exsist!");

            int currentIndex = GetHash(key);

            if (currentIndex >= _size) Resize(currentIndex);
            if (_keyValuePair[currentIndex].Value != null) currentIndex = GetAnotherIndex(currentIndex);
            
            _keyValuePair[currentIndex] = newPair;
        }

        public string Get(int key)
        {
            int currentIndex = GetHash(key);

            if (currentIndex >= _size)
            {
                throw new ArgumentOutOfRangeException($"Argument - [{key}], out of range!");
            }
            if (_keyValuePair[currentIndex].Value == null)
            {
                throw new ArgumentNullException($"Key - [{key}] does not have value!");
            }

            return _keyValuePair[currentIndex].Value;
        }

        public int GetHash(int key)
        {
            return key % _size;
        }

        public void Resize(int currentIndex)
        {
            _size = _size * 2;
            KeyValuePair<int, string>[] tmpKeyValuePair = _keyValuePair;
            _keyValuePair = new KeyValuePair<int, string>[_size];

            for (int i = 0; i < tmpKeyValuePair.Length; i++)
            {
                int newIndex = tmpKeyValuePair[i].Key.GetHashCode();
                if (_keyValuePair[newIndex].Value != null) newIndex = GetAnotherIndex(newIndex);
                _keyValuePair[newIndex] = tmpKeyValuePair[i];
            }
        }

        public int GetAnotherIndex(int currentIndex)
        {
            int newIndex = currentIndex;
            while (_keyValuePair[newIndex].Value != null)
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
