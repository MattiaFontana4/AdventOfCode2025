using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Numerics;
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

        private int[] _actualJoltages;
        private int[] _joltagesToBe;

        public int NumButtons { get { return _numButtons; } }
        public int NumStates { get { return _numStates; } }

        /// <summary>
        /// Initializes a new instance of the Macchine class by parsing the specified configuration line.
        /// </summary>
        /// <remarks>The input string should contain segments representing the machine's initial state,
        /// button definitions, and target joltages, each enclosed in specific delimiters (e.g., '[', '(', '{').
        /// Improperly formatted input may result in incorrect initialization or runtime errors.</remarks>
        /// <param name="line">A string containing the configuration data for the machine, including state, button, and joltage
        /// information. The format must match the expected pattern for correct parsing.</param>
        public Macchine(string line)
        {
            var segments = line.Split(' ');
            List<Button> buttons = new List<Button>();

            foreach (var segment in segments)
            {
                if (segment.Contains('['))
                {
                    var stateStr = segment.Trim('[', ']', ' ');
                    _stateToBe = stateStr.Select(c => Char.Equals('#', c)).ToArray();
                }
                else if (segment.Contains('('))
                {
                    var numStr = segment.Trim('(', ')', ' ').Split(',');
                    buttons.Add(new Button(numStr.Select(n => int.Parse(n)).ToArray()));
                }
                else if (segment.Contains('{'))
                {
                    var joltagesStr = segment.Trim(' ', '{', '}').Split(',');
                    _joltagesToBe = joltagesStr.Select(e => int.Parse(e)).ToArray();
                }
            }

            _numStates = _stateToBe.Count();
            _actualJoltages = new int[_numStates]; // devono essere inizializzati tutti a 0
            _actualState = new bool[_numStates]; // devono essere inizializzari tutti a false
            _buttons = buttons.ToArray();
            _numButtons = _buttons.Length;
        }

        /// <summary>
        /// Initializes a new instance of the Macchine class by copying the state from an existing Macchine instance.
        /// </summary>
        /// <remarks>This constructor creates a deep copy of the state arrays to ensure that changes to
        /// the new instance do not affect the original instance.</remarks>
        /// <param name="macchine">The Macchine instance whose state is used to initialize the new instance. Cannot be null.</param>
        public Macchine(Macchine macchine)
        {
            this._stateToBe = macchine._stateToBe;
            this._actualState = (bool[])macchine._actualState.Clone();
            this._numStates = macchine._numStates;
            this._buttons = macchine._buttons;
            this._numButtons = macchine._numButtons;
            this._joltagesToBe = macchine._joltagesToBe;
            this._actualJoltages = (int[])macchine._actualJoltages.Clone();
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// SECTION TO STATE

        /// <summary>
        /// Appliy the button at index to the actual state
        /// </summary>
        /// <param name="index">  </param>        
        public void Apply(int index) 
        {
            _buttons[index].Apply(_actualState);
        }

        
        /// <summary>
        /// Determines whether the current state is valid according to the defined criteria.
        /// </summary>
        /// <returns>true if the current state is correct; otherwise, false.</returns>
        public bool isCorrectState() 
        {
            bool result = isCorrectState(_actualState);

            return result;
        }

        /// <summary>
        /// Determines whether the specified states array matches the expected state configuration.
        /// </summary>
        /// <remarks>The comparison is performed element-wise up to the number of expected states. If the
        /// states array is shorter than required, an exception may be thrown.</remarks>
        /// <param name="states">An array of Boolean values representing the current states to compare. The length of the array must be at
        /// least the number of expected states.</param>
        /// <returns>true if all elements in the states array match the expected state configuration; otherwise, false.</returns>
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

        /// <summary>
        /// Finds the minimum number that can be applied to the current instance, up to the specified limit.
        /// </summary>
        /// <param name="limit">The maximum value to consider when searching for the minimum number. Must be greater than zero. The default
        /// is 10.</param>
        /// <returns>The smallest number that can be applied to the current instance, not exceeding the specified limit.</returns>
        public int FindMinNumApplyTo(int limit = 10) 
        {
            int min = FindMinNumApplyTo(this, limit);
            return min;
        }

        /// <summary>
        /// Recursively determines the minimum number of button applications required to bring the specified machine to
        /// a correct state.
        /// </summary>
        /// <remarks>This method uses recursion to explore possible button applications. If the maximum
        /// call stack depth is exceeded, the method returns a large value to indicate that a solution was not found
        /// within the allowed depth. The method is intended for internal use and may have performance implications for
        /// large or complex machine states.</remarks>
        /// <param name="m">The machine instance to evaluate and modify in search of a correct state.</param>
        /// <param name="maxCallStackDim">The maximum allowed recursion depth. Must be a positive integer. Defaults to 10.</param>
        /// <param name="actualCallStack">The current recursion depth. Used internally to track the call stack level. Defaults to 0.</param>
        /// <returns>The minimum number of button applications needed to reach a correct state from the given machine
        /// configuration. Returns a large value if the maximum recursion depth is exceeded or no solution is found.</returns>
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


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// SECTION JOLTAGES
        /// 

        
        public void ApplyJoltage(int index)
        {
            _buttons[index].ApplyJoltage(_actualJoltages);
        }


        public bool isJoltageOver(int index)
        {
            bool result = _actualJoltages[index] > _joltagesToBe[index];
            return result;
        }

        public bool isJoltageOver()
        {
            bool result = false;
            for (int i = 0; i < _numStates; i++)
            {
                if (isJoltageOver(i))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool isCorrectJoltageAt(int index)
        {
            bool result = _actualJoltages[index] == _joltagesToBe[index];
            return result;
        }

        public bool isCorrectJoltage()
        {
            bool result = true;
            for (int i = 0; i < _numStates; i++)
            {
                if (!isCorrectJoltageAt(i))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public bool isCorrectJoltage(int[] actual) 
        {
            bool result = true;

            for (int i = 0; i < _numStates; i++)
            {
                if (actual[i] != _joltagesToBe[i])
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public int getJoltageMaxCallStack()
        {
            int maxJ = _joltagesToBe.Sum(e => e);
            int minDimBtn = _buttons.Min(b => b.GetNumApply());

            int max = maxJ / minDimBtn;

            if(maxJ % minDimBtn != 0)
                max = max + 1;

            return max;
        }

        public int GetMinNumApplyToJoltage(int limit = 10)
        {
            int min = FindMinNumApplyToJoltage(this, limit);
            return min;
        }



        public int FindMinNumApplyToJoltage(Macchine m, int maxCallStackDim = 10, int actualCallStack = 0)
        {
            int min = int.MaxValue / 2;

            if (!m.isJoltageOver() && actualCallStack < maxCallStackDim) 
            {
                if (m.isCorrectJoltage()) 
                {
                    min = 0;
                    Console.WriteLine($"Finded at stack level: { actualCallStack}");
                }
                else 
                {
                    for (int i = 0; i < _numButtons; i++)
                    {
                        Macchine copy = new Macchine(m);

                        copy.ApplyJoltage(i);

                        int newMaxCallStack = maxCallStackDim < (actualCallStack + min) ? maxCallStackDim : (actualCallStack + min);

                        if (!copy.isJoltageOver())
                        {
                            int finded = this.FindMinNumApplyToJoltage(copy, newMaxCallStack, actualCallStack + 1);

                            if (finded < min)
                            {
                                min = finded;
                            }
                        }
                    }

                    min = min + 1;
                }

            }

            return min;
        }


        public int GetMinNumApplyToJoltageFast() 
        {
            EuclideanDescentGreedy solver = new EuclideanDescentGreedy(_joltagesToBe, _buttons);
            int result = solver.Solve();
            return result;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// SECTION OVERRIDE

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






        [TestMethod]
        public void TestisJoltageOver()
        {
            Macchine macchine = new Macchine(splitted[0]);
            macchine.ApplyJoltage(4);
            macchine.ApplyJoltage(4);
            macchine.ApplyJoltage(4);
            macchine.ApplyJoltage(4);

            Assert.IsTrue(macchine.isJoltageOver(0));

        }

        [TestMethod]
        public void TestisJoltageOver2()
        {
            Macchine macchine = new Macchine(splitted[0]);
            macchine.ApplyJoltage(4);
            macchine.ApplyJoltage(4);
            macchine.ApplyJoltage(4);
            Assert.IsFalse(macchine.isJoltageOver(0));
        }

        [TestMethod]
        public void TestisCorrectJoltageAt()
        {
            Macchine macchine = new Macchine(splitted[0]);
            macchine.ApplyJoltage(4);
            macchine.ApplyJoltage(4);
            macchine.ApplyJoltage(4);
            Assert.IsTrue(macchine.isCorrectJoltageAt(0));
        }

        [TestMethod]
        public void TestisCorrectJoltage1()
        {
            Macchine macchine = new Macchine(splitted[0]);

            Assert.IsFalse(macchine.isCorrectJoltage());

            macchine.ApplyJoltage(0);

            Assert.IsFalse(macchine.isCorrectJoltage());

            macchine.ApplyJoltage(1);
            macchine.ApplyJoltage(1);
            macchine.ApplyJoltage(1);

            macchine.ApplyJoltage(3);
            macchine.ApplyJoltage(3);
            macchine.ApplyJoltage(3);

            Assert.IsFalse(macchine.isCorrectJoltage());

            macchine.ApplyJoltage(4);

            macchine.ApplyJoltage(5);
            macchine.ApplyJoltage(5);

            Assert.IsTrue(macchine.isCorrectJoltage());
        }

        [TestMethod]
        public void TestisCorrectJoltage2()
        {
            Macchine macchine = new Macchine(splitted[1]);

            Assert.IsFalse(macchine.isCorrectJoltage());

            macchine.ApplyJoltage(0);
            macchine.ApplyJoltage(0);

            Assert.IsFalse(macchine.isCorrectJoltage());

            macchine.ApplyJoltage(1);
            macchine.ApplyJoltage(1);
            macchine.ApplyJoltage(1);
            macchine.ApplyJoltage(1);
            macchine.ApplyJoltage(1);

            Assert.IsFalse(macchine.isCorrectJoltage());

            macchine.ApplyJoltage(3);
            macchine.ApplyJoltage(3);
            macchine.ApplyJoltage(3);
            macchine.ApplyJoltage(3);
            macchine.ApplyJoltage(3);

            Assert.IsTrue(macchine.isCorrectJoltage());
        }

        [TestCategory("LongRunning")]

        [TestMethod]
        public void TestgetMinNumApplyToJoltage1()
        {
            Macchine macchine = new Macchine(splitted[0]);
            var maxCallStack = macchine.getJoltageMaxCallStack();
            var result = macchine.GetMinNumApplyToJoltage(maxCallStack);
            Assert.AreEqual(10, result);
        }


        [TestMethod]
        public void TestgetMinNumApplyToJoltage2()
        {
            Macchine macchine = new Macchine(splitted[1]);
            var maxCallStack = macchine.getJoltageMaxCallStack();
            var result = macchine.GetMinNumApplyToJoltage(maxCallStack);
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void TestgetMinNumApplyToJoltage3()
        {
            Macchine macchine = new Macchine(splitted[2]);
            var maxCallStack = macchine.getJoltageMaxCallStack();
            var result = macchine.GetMinNumApplyToJoltage(maxCallStack);
            Assert.AreEqual(11, result);
        }

        [TestMethod]
        public void TestEuclideanDescentGreedy1()
        {
            Macchine macchine = new Macchine(splitted[0]);
            var result = macchine.GetMinNumApplyToJoltageFast();
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void TestEuclideanDescentGreedy2()
        {
            Macchine macchine = new Macchine(splitted[1]);
            var result = macchine.GetMinNumApplyToJoltageFast();
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void TestEuclideanDescentGreedy3()
        {
            Macchine macchine = new Macchine(splitted[2]);
            var result = macchine.GetMinNumApplyToJoltageFast();
            Assert.AreEqual(11, result);
        }
    }
}
