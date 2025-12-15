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
            var l = indexes.Distinct().ToList();

            if(l.Count() != indexes.Length)
                throw new Exception("Button indexes contain duplicates");

            _indexApply = indexes;
        }

        public void Apply(bool[] states) 
        {
            foreach (var index in _indexApply) 
            {
                states[index] = !states[index];
            }
        }

        public void ApplyJoltage(int[] jostages) 
        {
            foreach (var index in _indexApply)
            {
                jostages[index] += 1;
            }
        }

        public bool Contains(int i) 
        {
            bool result = _indexApply.Any(x => x == i);
            return result;
        }

        public int GetNumApply() 
        {
            return _indexApply.Length;
        }

        public int[] GetVector(int dim)
        {
            int[] vector = new int[dim];
            foreach (var index in _indexApply)
            {
                vector[index] = 1;
            }
            return vector;
        }
    }
}
