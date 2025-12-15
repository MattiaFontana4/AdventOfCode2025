using System;
using System.Collections.Generic;
using System.Text;

namespace Day10
{
    public class EuclideanDescentGreedy
    {
        int numComponents;
        int numVectors;

        int[] _current;
        int[] _destination;
        int[][] _vectors;

        public EuclideanDescentGreedy(int[] dest, Button[] buttons)
        {
            _destination = dest;
            _current = new int[dest.Length];
            _vectors = buttons.Select(b => b.GetVector(dest.Length)).ToArray();

            numComponents = dest.Length;
            numVectors = buttons.Length;
        }

        private int Distance2(int[] actual)
        {
            int sum = 0;
            for (int i = 0; i < numComponents; i++)
            {
                int diff = actual[i] - _destination[i];
                sum += diff * diff;
            }
            return sum;
        }

        public int Solve()
        {
            int numIters = 0;

            int distance = Distance2(this._current);

            if (distance != 0)
            {
                numIters = SolveRecursive(_current);
            }

            return numIters;
        }

        private int SolveRecursive(int[] actualrec)
        {
            int result = int.MinValue;

            var distTup = _vectors.Select(v =>
            {
                var newVec = AddVectors(actualrec, v);
                var distance = Distance2(newVec);
                return (vector: newVec, distance: distance);
            }).Where(t => !IsOverShoot(t.vector, this._destination)).OrderBy(e => e.distance);

            foreach (var tup in distTup)
            {
                if (tup.distance == 0)
                {
                    result = 1;
                    break;
                }
                else
                {
                    int res = SolveRecursive(tup.vector);
                    if (res > 0)
                    {
                        result = res + 1;
                        break;
                    }
                }
            }

            return result;
        }



        public static int[] AddVectors(int[] a, int[] b)
        {
            int[] result = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] + b[i];
            }

            return result;
        }

        public static bool IsOverShoot(int[] actual, int[] destination, int[] vector)
        {
            for (int i = 0; i < actual.Length; i++)
            {
                if (Math.Abs(actual[i] + vector[i]) > Math.Abs(destination[i]))
                    return true;
            }
            return false;
        }

        public static bool IsOverShoot(int[] actual, int[] destination)
        {
            for (int i = 0; i < actual.Length; i++)
            {
                if (Math.Abs(actual[i]) > Math.Abs(destination[i]))
                    return true;
            }
            return false;
        }
    }
}
