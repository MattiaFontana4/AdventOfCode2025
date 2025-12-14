using System;
using System.Collections.Generic;
using System.Text;

namespace Day10
{
    public struct Button
    {
        private int[] _indexApply;

        public Button(int[] indexes) 
        {
            _indexApply = indexes;
        }

        public void Apply(bool[] states) 
        {
            foreach (var index in _indexApply) 
            {
                states[index] = !states[index];
            }
        }

        public bool Contains(int i) 
        {
            bool result = _indexApply.Any(x => x == i);
            return result;
        }

    }
}
