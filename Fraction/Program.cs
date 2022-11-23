#define STAGE1
#define STAGE2
#define STAGE3
//#define STAGE4
using lab7;
using System;
using System.Globalization;

namespace FractionN {
    class Program {
        private static int TestCounter = 0;
        static void Test(object obj1, object obj2, bool equals = true) {
            if (obj1.Equals(obj2) == equals)
                Console.WriteLine($"  {++TestCounter:00}. OK! \"{obj1.ToString()}\" " + (equals ? "==" : "!=") + $" \"{obj2.ToString()}\"");
            else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  {++TestCounter:00}. Error! \"{obj1.ToString()}\" == \"{obj2.ToString()}\" is not {equals.ToString()}!");
            }
            Console.ResetColor();
        }
        static void Main(string[] args) {

            Console.WriteLine("\nTesting basic constructor and ToString method.\n");
#if STAGE1
            Fraction frac;
            frac = new Fraction(1, 2);
            Test($"{frac}", "[1/2]");

            frac = new Fraction(1, 3);
            Test($"{frac}", "[1/3]");

            frac = new Fraction(2, 4);
            Test($"{frac}", "[1/2]");

            frac = new Fraction(-7, -14);
            Test($"{frac}", "[1/2]");

            frac = new Fraction(-1, 3);
            Test($"{frac}", "-[1/3]");

            frac = new Fraction(4, 2);
            Test($"{frac}", "[2]");

            frac = new Fraction(10, 3);
            Test($"{frac}", "[3 1/3]");

            frac = new Fraction(-6, 3);
            Test($"{frac}", "-[2]");

            frac = new Fraction(7, -3);
            Test($"{frac}", "-[2 1/3]");

            Console.WriteLine("\nTesting Properties\n");

            frac = new Fraction(2, 4);
            Test($"l={frac.Numerator} m={frac.Denominator}", "l=1 m=2");

            frac = new Fraction(6, -3);
            Test($"l={frac.Numerator} m={frac.Denominator}", "l=-2 m=1");

            frac = new Fraction(1, 6);
            frac.Numerator = 2;
            Test($"l={frac.Numerator} m={frac.Denominator}", "l=1 m=3");
            frac.Numerator = 0;
            Test($"l={frac.Numerator} m={frac.Denominator}", "l=0 m=1");

            frac = new Fraction(4, 3);
            frac.Denominator = -6;
            Test($"l={frac.Numerator} m={frac.Denominator}", "l=-2 m=3");

            Console.WriteLine("\nTesting array creation\n");

            var array = new Fraction[1];
            try
            {
                array[0].Numerator = 4;
                array[0].Denominator = 5;
                Test(array[0].ToString(), "[4/5]");
            }
            catch (Exception)
            {
                Test("null", "[4/5]");
            }
#endif
#if STAGE2
            Console.WriteLine("\nTesting constructors and converters\n");

            frac = new Fraction(7);
            Test($"{frac}", "[7]");

            frac = 5;
            Test($"{frac}", "[5]");

            frac = new Fraction(-6, 4);
            double d = (double)frac;
            Test($"{d}", "-1.5");

            frac = new Fraction(14, 5);
            long l = (long)frac;
            Test($"{l}", "2");

            frac = new Fraction(-20, 7);
            l = (long)frac;
            Test($"{l}", "-2");

            Console.WriteLine("\nTesting arithmetic operators\n");

            Fraction u1, u2;

            u1 = new Fraction(1, 30000000000);
            u2 = new Fraction(1, 2);
            u2 = u2 - u1;
            frac = u1 + u2;
            Test($"{frac}", "[1/2]");

            u1 = new Fraction(1234567890, 7777777773);
            u2 = u1;
            u1 = 42 * u1;
            frac = u1 / u2;
            Test($"{frac}", "[42]");
#endif
#if STAGE3
            Console.WriteLine("\nTesting reciprocal\n");
            Test(u1.Reciprocal, new Fraction(7777777773, 1234567890L * 42));
            Test(u1.Reciprocal.Reciprocal, u1);
            Test(new Fraction(-2, -4).Reciprocal, new Fraction(2));
            Test(u1.Reciprocal, 12345678901, false);

            Console.WriteLine("\nTesting comparison operators\n");
            u1 = new Fraction(5, 2222222222222222222);
            u2 = new Fraction(5, 4444444444444444444);
            Test(u1 > u2, true);
            Test(u1 == u2, false);
            Test(u2 <= u1, true);

            u1 = new Fraction(1, 9000000000000000000);
            u2 = new Fraction(1, 9000000000000000001);
            Test(u1 < u2, false);
            Test(u1 == u2, false);
            Test(u1 >= u2, true);

            u1 = new Fraction(1, 2);
            u2 = new Fraction(1, 2);
            Test(u1.Equals(u2), true);
#endif
#if STAGE4
            Console.WriteLine("\nHarder tests\n");

            u1 = new Fraction(22_807, 23_753);
            u1 *= new Fraction(-26_203, 20_287);
            Test(u1.ToString(), "-[1 115734710/481877111]");
            u1 *= new Fraction(25_873, 29_383);
            Test(u1.ToString(), "-[1 1303015492220/14158995152513]");
            u1 *= new Fraction(28_499, -25_997);
            Test(u1.ToString(), "[1 72560444384365306/368091396979880461]");
            u1 -= new Fraction(13_462_010_644_733, 14_158_995_152_513);
            Test(u1.ToString(), "[90679950633121966/368091396979880461]");
            u1 *= new Fraction(25_997, 52_067);
            Test(u1.ToString(), "[1741601218298/14158995152513]");
            u1 *= new Fraction(29_383, 3_826);
            Test(u1.ToString(), "[455201573/481877111]");
            u1 *= new Fraction(20_287);
            Test(u1.ToString(), "[19163 22834/23753]");
            u1 *= new Fraction(23_753);
            Test(u1.ToString(), "[455201573]");

            try
            {
                u1.Denominator = 0;
                Test(frac.ToString(), "ArgumentException");
            }
            catch (ArgumentException)
            {
                Test("ArgumentException", "ArgumentException");
            }

            Console.WriteLine("\nTest finished!\n");
#endif
        }
    }
}