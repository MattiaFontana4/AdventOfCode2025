using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Day10
{
    public class Macchine
    {
        private int _numStates;
        private bool[] _actualState;
        private bool[] _stateToBe;

        private int _numButtons;
        private Button[] _buttons;

        public int NumButtons { get { return _numButtons; } }
        public int NumStates { get { return _numStates; } }

        public Macchine(string line) 
        {
            var segments = line.Split(' ');
            List<Button> buttons = new List<Button>();
            List<bool> states = new List<bool>();

            foreach (var segment in segments) 
            {
                if (segment.Contains('['))
                {
                    var stateStr = segment.Trim('[', ']', ' ');
                    _stateToBe = stateStr.Select(c => Char.Equals('#',c)).ToArray();
                    _numStates = _stateToBe.Count();

                    for (int i = 0; i < _numStates; i++)
                    {
                        states.Add(false);
                    }
                }
                else if (segment.Contains('('))
                {
                    var numStr = segment.Trim('(', ')', ' ').Split(',');
                    buttons.Add(new Button(numStr.Select(n => int.Parse(n)).ToArray()));
                }
                else if (segment.Contains('{')) 
                {

                }
            }

            _actualState = states.ToArray();
            _buttons = buttons.ToArray();
            _numButtons = _buttons.Length;
        }

        public bool[] XorStates()
        {
            return XorArray(_actualState, _stateToBe);
        }


        public static bool[] XorArray(bool[] actualState, bool[] stateToBe)
        {
            if (actualState is null) throw new ArgumentNullException(nameof(actualState));
            if (stateToBe is null) throw new ArgumentNullException(nameof(stateToBe));
            if (actualState.Length != stateToBe.Length)
                throw new ArgumentException("Gli array devono avere la stessa lunghezza.");

            var res = new bool[actualState.Length];
            for (int i = 0; i < res.Length; i++)
                res[i] = actualState[i] ^ stateToBe[i];
            return res;
        }

        public Macchine(Macchine macchine) 
        {
            this._stateToBe = macchine._stateToBe;
            this._actualState = (bool[])macchine._actualState.Clone();
            this._numStates = macchine._numStates;
            this._buttons = macchine._buttons;
            this._numButtons = macchine._numButtons;
        }

        public void Apply(int index) 
        {
            _buttons[index].Apply(_actualState);
        }

        public bool isCorrectState() 
        {
            bool result = isCorrectState(_actualState);

            return result;
        }

        public bool isCorrectState(bool[] states) 
        {
            bool result = true;

            for (int i = 0; i < _numStates; i++)
            {
                if (states[i] != _stateToBe[i])
                {
                    result = false;
                    break;
                }
            }

            return result;
        }


        public bool TryApply(int n) 
        {
            Macchine m = new Macchine(this);

            m.Apply(n);

            return m.isCorrectState();
        }

        public int FindMinNumApplyTo(int limit = 10) 
        {
            int min = FindMinNumApplyTo(this, limit);
            return min;
        }

        private int FindMinNumApplyTo(Macchine m, int maxCallStackDim = 10, int actualCallStack = 0) 
        {
            int min = int.MaxValue;

            if (actualCallStack > maxCallStackDim)
            {
                min = int.MaxValue / 2;
            }
            else if (!m.isCorrectState())
            {

                for (int i = 0; i < _numButtons; i++)
                {
                    Macchine copy = new Macchine(m);
                    copy.Apply(i);

                    int newMaxCallStack = maxCallStackDim < (actualCallStack + min) ? maxCallStackDim : (actualCallStack + min);

                    int finded = this.FindMinNumApplyTo(copy, newMaxCallStack, actualCallStack + 1);

                    if (finded < min)
                    {
                        min = finded;
                    }
                }

                min = min + 1;
            }
            else 
            {
                min = 0;
                Console.WriteLine($"Finded at stack level: { actualCallStack}");
            }

            return min;
        }

        public int[] GetCurrentApplicableButtonsIndexes() 
        {
            var diff = this.XorStates();

            var filderedIndexStates = diff.Select((value, index) => new { value, index }).Where(x => x.value).Select(x => x.index);
            var filteredIndexBtns = _buttons.Select((value, index) => new { value, index })
                .Where(x => filderedIndexStates.Any(n => x.value.Contains(n)))
                .Select(x => x.index).ToArray();

            return filteredIndexBtns;
        }

        public override bool Equals(object? obj)
        {
            return obj is Macchine macchine &&
                   _numStates == macchine._numStates &&
                   _actualState.SequenceEqual(macchine._actualState) &&
                   _stateToBe.SequenceEqual(macchine._stateToBe) &&
                   _numButtons == macchine._numButtons &&
                   //EqualityComparer<Button[]>.Default.Equals(_buttons, macchine._buttons) &&
                   NumButtons == macchine.NumButtons;
        }
    }

    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class MacchineTestClass
    {
        string testText = "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}\n" +
            "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}\n" +
            "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}";

        string[] splitted;

        public MacchineTestClass() 
        {
            splitted = testText.Split('\n');
        }

        [TestMethod]
        public void TestFindMinNumApplyTo1()
        {
            Macchine macchine = new Macchine(splitted[0]);

            var result = macchine.FindMinNumApplyTo();

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void TestFindMinNumApplyTo2()
        {
            Macchine macchine = new Macchine(splitted[1]);

            var result = macchine.FindMinNumApplyTo();

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestFindMinNumApplyTo3()
        {
            Macchine macchine = new Macchine(splitted[2]);

            var result = macchine.FindMinNumApplyTo();

            Assert.AreEqual(2, result);
        }


        [TestMethod]
        public void TestIsCorrectState() 
        {
            Macchine macchine = new Macchine(splitted[0]);

            macchine.Apply(4);
            macchine.Apply(5);

            bool result = macchine.isCorrectState();
        
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEquals1() 
        {
            Macchine macchine1 = new Macchine(splitted[0]);
            Macchine macchine2 = new Macchine(splitted[0]);

            bool isEquals = macchine1.Equals(macchine2);

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void TestEquals2()
        {
            Macchine macchine1 = new Macchine(splitted[0]);
            Macchine macchine2 = new Macchine(macchine1);

            bool isEquals = macchine1.Equals(macchine2);

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void TestEquals3()
        {
            Macchine macchine1 = new Macchine(splitted[0]);
            Macchine macchine2 = new Macchine(splitted[1]);

            bool isEquals = macchine1.Equals(macchine2);

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void TestEquals4() 
        {
            Macchine macchine1 = new Macchine(splitted[0]);
            Macchine macchine2 = new Macchine(macchine1);

            macchine1.Apply(0);

            bool isEquals = macchine1.Equals(macchine2);

            Assert.IsFalse(isEquals);
        }

        [TestMethod]
        public void TestEquals5()
        {
            Macchine macchine1 = new Macchine(splitted[0]);
            Macchine macchine2 = new Macchine(macchine1);

            macchine1.TryApply(0);

            bool isEquals = macchine1.Equals(macchine2);

            Assert.IsTrue(isEquals);
        }

        [TestMethod]
        public void TestXorState1() 
        {
            Macchine macchine = new Macchine(splitted[0]);
            var xor = macchine.XorStates();

            Assert.IsFalse(xor[0]);
            Assert.IsTrue(xor[1]);
            Assert.IsTrue(xor[2]);
            Assert.IsFalse(xor[3]);
        }

        [TestMethod]
        public void TestXorState2()
        {
            Macchine macchine = new Macchine(splitted[0]);
            macchine.Apply(4);
            macchine.Apply(5);
            
            var xor = macchine.XorStates();

            Assert.IsFalse(xor[0]);
            Assert.IsFalse(xor[1]);
            Assert.IsFalse(xor[2]);
            Assert.IsFalse(xor[3]);
        }

        [TestMethod]
        public void TestGetCurrentApplicableButtonsIndexes() 
        {
            Macchine macchine = new Macchine(splitted[0]);

            var indexes = macchine.GetCurrentApplicableButtonsIndexes();

            Assert.That(() => !indexes.Contains(0), "");
            Assert.That(() => indexes.Contains(1), "");
            Assert.That(() => indexes.Contains(2), "");
            Assert.That(() => indexes.Contains(3), "");
            Assert.That(() => indexes.Contains(4), "");
            Assert.That(() => indexes.Contains(5), "");
        }

    }

}
