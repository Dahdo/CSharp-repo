#define STAGE1
//#define STAGE2
//#define STAGE3
//#define STAGE4

using System;
using System.Collections;
using System.Linq;

namespace EN_Lab_09 {
    class Program {
        static void Main() {
#if STAGE1
            Console.WriteLine("############# STAGE 1 #############");
            Console.WriteLine("<<<<< Repeat tests >>>>>");
            CheckEnumerator(new Repeat(0, 3),
                new object[] { 0, 0, 0 },
                true);
            CheckEnumerator(new Repeat(-1, 5),
                new object[] { -1, -1, -1, -1, -1 },
                true);
            CheckEnumerator(new Repeat(7, 1),
                new object[] { 7 },
                true);
            CheckEnumerator(new Repeat(42, 0),
                new object[] { },
                true);
            Console.WriteLine("<<<<< ArithmeticSeries tests >>>>>");
            CheckEnumerator(new ArithmeticProgression(0, 1),
                new object[] { 0, 1, 2, 3, 4, 5 },
                false);
            CheckEnumerator(new ArithmeticProgression(1, 1),
                new object[] { 1, 2, 3, 4, 5, 6 },
                false);
            CheckEnumerator(new ArithmeticProgression(0),
                new object[] { 0, 1, 2, 3, 4, 5 },
                false);
            CheckEnumerator(new ArithmeticProgression(-2, 0),
                new object[] { -2, -2, -2, -2, -2, -2 },
                false);
            CheckEnumerator(new ArithmeticProgression(13, -2),
                new object[] { 13, 11, 9, 7, 5, 3 },
                false);
            Console.WriteLine("<<<<< GeometricSeries tests >>>>>");
            CheckEnumerator(new GeometricProgression(3, 2),
                new object[] { 3, 6, 12, 24, 48, 96, 192 },
                false);
            CheckEnumerator(new GeometricProgression(7, 2),
                new object[] { 7, 14, 28, 56, 112, 224 }, 
                false);
            CheckEnumerator(new GeometricProgression(9, 1),
                new object[] { 9, 9, 9, 9, 9 },
                false);
            CheckEnumerator(new GeometricProgression(4, -3),
                new object[] { 4, -12, 36, -108, 324, -972 },
                false);
            Console.WriteLine("<<<<< PowersOf tests >>>>>");
            CheckEnumerator(new PowersOf(2),
                new object[] { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512 },
                false);
            CheckEnumerator(new PowersOf(-3),
                new object[] { 1, -3, 9, -27, 81, -243, 729, -2187 },
                false);
#endif
#if STAGE2
            Console.WriteLine("############# STAGE 2 #############");
            Console.WriteLine("<<<<< SumSeries tests >>>>>");
            CheckEnumerator(new SequenceSum(), new Repeat(3, 5),
                new object[] { 3, 6, 9, 12, 15 },
                true);
            CheckEnumerator(new SequenceSum(), new ArithmeticProgression(-70, 30),
                new object[] { -70, -110, -120, -100, -50, 30, 140, 280 },
                false);
            CheckEnumerator(new SequenceSum(), new PowersOf(5),
                new object[] { 1, 6, 31, 156, 781, 3906, 19531 },
                false);
            Console.WriteLine("<<<<< LessThanFilter tests >>>>>");
            CheckEnumerator(new LessThanFilter(10), new Repeat(3, 5),
                new object[] { 3, 3, 3, 3, 3 },
                true);
            CheckEnumerator(new LessThanFilter(0), new ArithmeticProgression(70, -30),
                new object[] { -20, -50, -80, -110, -140, -170 },
                false);
            CheckEnumerator(new LessThanFilter(0), new GeometricProgression(5, -2),
                new object[] { -10, -40, -160, -640, -2560 },
                false);
            CheckEnumerator(new LessThanFilter(0, true), new GeometricProgression(5, -2),
                new object[] { -10, -10, -40, -40, -160, -160 },
                false);
            CheckEnumerator(new LessThanFilter(0), new object[] { 2, 1, 0, -1, -2, -1, 0, 1, 2, 1, 0, -1, -2 },
                new object[] { -1, -2, -1, -1, -2 },
                true);
            CheckEnumerator(new LessThanFilter(0, true), new object[] { 2, 1, 0, -1, -2, -1, 0, 1, 2, 1, 0, -1, -2 },
                new object[] { -1, -2, -1, -1, -1, -1, -1, -1, -1, -2 },
                true);
            CheckEnumerator(new LessThanFilter(0), new PowersOf(-7),
                new object[] { -7, -343, -16807, -823543, -40353607, -1977326743, -381759919 },
                false);
            Console.WriteLine("<<<<< MedianFilter tests >>>>>");
            CheckEnumerator(new MedianFilter(3), new Repeat(5, 6),
                new object[] { 5, 5, },
                true);
            CheckEnumerator(new MedianFilter(3), new Repeat(5, 7),
                new object[] { 5, 5, 5, },
                true);
            CheckEnumerator(new MedianFilter(5), new ArithmeticProgression(-3, 5),
                new object[] { 7, 32, 57, 82, 107, 132, 157, 182, 207 },
                false);
            CheckEnumerator(new MedianFilter(4), new ArithmeticProgression(-3, 5),
                new object[] { 7, 27, 47, 67, 87, 107, 127 },
                false);
            CheckEnumerator(new MedianFilter(3), new GeometricProgression(6, -2),
                new object[] { 6, -48, 384, -3072, 24576, -196608 },
                false);
#endif
#if STAGE3
            Console.WriteLine("############# STAGE 3 #############");
            CheckEnumerator(new CantorPair(), new ArithmeticProgression(0, -1), new ArithmeticProgression(0, 1),
                new object[] { (0, 0), (-1, 0), (0, 1), (-2, 0), (-1, 1), (0, 2), (-3, 0), (-2, 1), (-1, 2), (0, 3) },
                false);
#endif
#if STAGE4
            Console.WriteLine("############# STAGE 4 #############");
            CheckEnumerator(new CantorPair(), new int[] { 0, 1, 2, 3 }, new int[] { 0, -1, -2 },
                new object[] { (0, 0), (1, 0), (0, -1), (2, 0), (1, -1), (0, -2), (3, 0), (2, -1), (1, -2), (3, -1), (2, -2), (3, -2) },
                true);
            CheckEnumerator(new CantorPair(), new int[] { 0, -1 }, new ArithmeticProgression(0, 1),
                new object[] { (0, 0), (-1, 0), (0, 1), (-1, 1), (0, 2), (-1, 2), (0, 3), (-1, 3) },
                false);
            CheckEnumerator(new CantorPair(), new ArithmeticProgression(0, 1), new int[] { 0, -1 },
                new object[] { (0, 0), (1, 0), (0, -1), (2, 0), (1, -1), (3, 0), (2, -1), (4, 0) },
                false);
#endif
        }
#if STAGE3
        private static void CheckEnumerator(ISequenceCombine decorator, IEnumerable series1, IEnumerable series2, object[] correct, bool shouldEnds)
        {
            CheckEnumerator($"{decorator.Description()} of {series1} and {series2}",
                decorator.Combine(series1, series2), correct, shouldEnds);
        }
#endif
#if STAGE2
        private static void CheckEnumerator(ISequenceDecorator decorator, IEnumerable series, object[] correct, bool shouldEnds)
        {
            CheckEnumerator($"{decorator.Description()} of {series}",
                decorator.Decorate(series), correct, shouldEnds);
        }
        private static void CheckEnumerator(ISequenceDecorator decorator, ISequence series, object[] correct, bool shouldEnds)
        {
            CheckEnumerator($"{decorator.Description()} of {series.Description()}",
                decorator.Decorate(series), correct, shouldEnds);
        }
#endif
#if STAGE1
        private static void CheckEnumerator(ISequence series, object[] correct, bool shouldEnds)
        {
            CheckEnumerator(series.Description(), series, correct, shouldEnds);
        }
#endif
        private static void CheckEnumerator(string name, IEnumerable enumerator, object[] correct, bool shouldEnds) {
            var corrStr = correct.Select(i => i.ToString()).ToList();
            var enumStr = enumerator.Cast<object>().Take(correct.Length + 1).Select(i => i.ToString()).ToList();
            if (enumStr.Count > correct.Length)
                enumStr[enumStr.Count - 1] = "...";
            else
                enumStr.Add("END");
            corrStr.Add(shouldEnds ? "END" : "...");
            enumStr.AddRange(Enumerable.Repeat("-", corrStr.Count - enumStr.Count));
            var strLen = corrStr.Zip(enumStr, (a, b) => Math.Max(a.Length, b.Length)).ToArray();
            bool[] areSame = corrStr.Zip(enumStr, (a, b) => a == b).ToArray();
            bool isCorrect = areSame.Aggregate(true, (a, v) => v & a);
            var color = Console.ForegroundColor;
            Console.Write($"{name}: ");
            Console.ForegroundColor = isCorrect ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(isCorrect ? "Ok" : "Wrong");
            Console.ForegroundColor = color;
            Console.Write("  Enumerable = ");
            int idx = 0;
            foreach (var s in enumStr) {
                Console.ForegroundColor = areSame[idx] ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write(string.Format($" {{0,{strLen[idx]}}}", s));
                ++idx;
            }
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.Write("  Correct    = ");
            idx = 0;
            foreach (var s in corrStr) {
                Console.Write(string.Format($" {{0,{strLen[idx]}}}", s));
                ++idx;
            }
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}