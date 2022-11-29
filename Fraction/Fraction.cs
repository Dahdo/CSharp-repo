using System;
using System.Diagnostics.CodeAnalysis;

namespace lab7 {
    public struct Fraction {
        private long numerator;
        private long denominator;

        public long Numerator {
            get { return numerator; }
            set { numerator = value; Adjust(); }
        }

        public long Denominator {
            get { return denominator; }
            set {
                if (value == 0)
                    throw new ArgumentException("Denominator can't be zero!");
                else
                    denominator = value;
                Adjust();
            }
        }

        public Fraction Reciprocal {
            get { return new Fraction(denominator, numerator); }
        }

        public override string ToString() {
            long decPart = numerator / denominator;
            long remainder = numerator % denominator;
            if (Math.Abs(decPart) == 0)
                return numerator < 0 ? $"-[{Math.Abs(numerator)}/{denominator}]" : $"[{Math.Abs(numerator)}/{denominator}]";

            else if (Math.Abs(decPart) == 1 && remainder == 0)
                return numerator < 0 ? $"-[{Math.Abs(numerator)}]" : $"[{numerator}]";

            else {
                if(remainder == 0)
                    return decPart < 0 ? $"-[{Math.Abs(decPart)}]" : $"[{decPart}]";
                return decPart < 0 ? $"-[{Math.Abs(decPart)} {Math.Abs(remainder)}/{denominator}]" : $"[{Math.Abs(decPart)} {Math.Abs(remainder)}/{denominator}]";
            }
        }
        private long GCD(long a, long b) {
            if (b == 0)
                return a;
            return GCD(b, a % b);
        }
        private void Adjust() { //helper function
            if(denominator < 0) {
                numerator *= -1;
                denominator = Math.Abs(denominator);
            }

            long gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));

            numerator /= gcd;
            denominator /= gcd;


            //for(long i = denominator; i > 1; i--) {
            //    if(numerator % i == 0 && denominator % i == 0) {
            //        numerator /= i;
            //        denominator /= i;
            //        i = denominator;
            //    }
            //}
        }
        public Fraction(long num, long denom = 1) {
            numerator = num;
            if (denom == 0)
                denominator = 1;
            else
               denominator = denom;
            Adjust();
        }
        //Task2

        public static implicit operator Fraction(long value) {
            return new Fraction(value);
        }
        public static explicit operator long(Fraction fraction) {
            return fraction.numerator / fraction.denominator;
        }

        public static explicit operator double(Fraction fraction) {
            return (double)fraction.numerator / (double)fraction.denominator;
        }

        public static Fraction operator +(Fraction fraction1, Fraction fraction2) {
            long denom = fraction1.denominator * fraction2.denominator;
            long num1 = fraction2.denominator * fraction1.numerator;
            long num2 = fraction1.denominator * fraction2.numerator;

            return new Fraction(num1 + num2, denom);
        }

        public static Fraction operator -(Fraction fraction1, Fraction fraction2) {
            long denom = fraction1.denominator * fraction2.denominator;
            long num1 = denom / fraction1.denominator * fraction1.numerator;
            long num2 = denom / fraction2.denominator * fraction2.numerator;

            return new Fraction(num2 - num1, denom);
        }

        public static Fraction operator-(Fraction fraction) {
            return new Fraction(-fraction.numerator, fraction.denominator);
        }

        public static Fraction operator*(Fraction fraction1, Fraction fraction2) {
            return new Fraction(fraction1.numerator * fraction2.numerator, fraction1.denominator * fraction2.denominator);
        }

        public static Fraction operator/(Fraction fraction1, Fraction fraction2) {
            return fraction1 * new Fraction(fraction2.denominator, fraction2.numerator);
        }

        //Task3

        public static bool operator <(Fraction fraction1, Fraction fraction2) {
            return (fraction1.numerator < fraction2.numerator) && (fraction1.denominator > fraction2.denominator);
        }

        public static bool operator >(Fraction fraction1, Fraction fraction2) {
            return !(fraction1 < fraction2);
        }

        public static bool operator <=(Fraction fraction1, Fraction fraction2) {
            return (fraction1.numerator <= fraction2.numerator) && (fraction1.denominator >= fraction2.denominator);
        }

        public static bool operator >=(Fraction fraction1, Fraction fraction2) {
            return !(fraction1 <= fraction2);
        }

        public static bool operator ==(Fraction fraction1, Fraction fraction2) {
            return (fraction1.numerator == fraction2.numerator) && (fraction1.denominator == fraction2.denominator);
        }

        public static bool operator !=(Fraction fraction1, Fraction fraction2) {
            return !(fraction1 == fraction2);
        }


        //public override bool Equals(object? obj) {
        //    return this == ((Fraction)obj!);
        //}
        public override bool Equals([NotNullWhen(true)] object? obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }

}
