using RationalNumbers;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace RationalNumbers.Tests
{
    [TestClass]
    public class Rational
    {
        public Rational()
        {
            // Публичный конструктор по умолчанию
        }
        private int denominator;
        private int numerator;
        public Rational(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be zero.");
            }
            // Упрощение дроби
            int gcd = GCD(numerator, denominator);
            this.numerator = numerator / gcd;
            this.denominator = denominator / gcd;

            // Приведение к стандартному виду: знаменатель положительный
            if (this.denominator < 0)
            {
                this.numerator = -this.numerator;
                this.denominator = -this.denominator;
            }
        }

        public static Rational operator +(Rational r1, Rational r2)
        {
            int newNumerator = r1.numerator * r2.denominator + r2.numerator * r1.denominator; // Новый числитель
            int newDenominator = r1.denominator * r2.denominator; // Новый знаменатель

            return new Rational(newNumerator, newDenominator); // Возвращаем новый объект Rational
        }
        public static Rational operator -(Rational r1, Rational r2)
        {
            return new Rational(r1.numerator * r2.denominator - r2.numerator * r1.denominator,
                                r1.denominator * r2.denominator);
        }
        public static Rational operator *(Rational r1, Rational r2)
        {
            return new Rational(r1.numerator * r2.numerator, r1.denominator * r2.denominator);
        }
        public static Rational operator /(Rational r1, Rational r2)
        {
            if (r2.numerator == 0)
            {
                throw new DivideByZeroException("Attempted to divide by zero");
            }
            return new Rational(r1.numerator * r2.denominator, r1.denominator * r2.numerator);
        }
        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int t = b;
                b = a % b;
                a = t;
            }
            return Math.Abs(a);
        }
        public override string ToString()
        {
            return $"{numerator}/{denominator}";
        }
        public static bool operator ==(Rational r1, Rational r2)
        {
            return r1.numerator == r2.numerator && r1.denominator == r2.denominator;
        }
        public static bool operator !=(Rational r1, Rational r2)
        {
            return !(r1 == r2);
        }

        public static bool operator >(Rational r1, Rational r2)
        {
            return r1.numerator * r2.denominator > r2.numerator * r1.denominator;
        }
        public static bool operator <(Rational r1, Rational r2)
        {
            return r1.numerator * r2.denominator < r2.numerator * r1.denominator;
        }
        [TestMethod]
        public void TestAddition()
        {
            Rational r1 = new Rational(5, -10);
            Rational r2 = new Rational(-3, 4);
            Rational result = r1 + r2;
            Assert.AreEqual("-5/4", result.ToString());
        }

        [TestMethod]
        public void TestSubtraction()
        {
            Rational r4 = new Rational(-12, 24);
            Rational r2 = new Rational(-3, 4);
            Rational result = r4 - r2;
            Assert.AreEqual("1/4", result.ToString());

        }
        [TestMethod]
        public void TestMultiplication()
        {
            Rational r1 = new Rational(5, -10);
            Rational r4 = new Rational(-12, 24);
            Rational result = r1 * r4;
            Assert.AreEqual("1/4", result.ToString());
        }

        [TestMethod]
        public void TestDivision()
        {
            Rational r1 = new Rational(5, 10);
            Rational r2 = new Rational(1, 2);
            Rational result = r1 / r2;
            Assert.AreEqual("1/1", result.ToString());
        }
        [TestMethod]
        public void TestEquality()
        {
            Rational r1 = new Rational(5, -10);
            Rational r2 = new Rational(-5, 10);
            Assert.IsTrue(r1 == r2);
        }

        [TestMethod]
        public void TestInequality()
        {
            Rational r1 = new Rational(5, -10);
            Rational r2 = new Rational(-3, 4);
            Assert.IsTrue(r1 != r2);
        }
        [TestMethod]

        public void TestGreaterThan()
        {
            Rational r1 = new Rational(-1, 2); // -1/2
            Rational r2 = new Rational(-3, 4); // -3/4
            Assert.IsTrue(r1 > r2); // -1/2 > -3/4
            Assert.IsFalse(r2 > r1); // -3/4 !> -1/2
        }

        [TestMethod]
        public void TestLessThan()
        {
            Rational r1 = new Rational(1, 3);
            Rational r2 = new Rational(2, 3);
            Assert.IsTrue(r1 < r2);              // 1/3 < 2/3
        }
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestDivisionByZero()
        {
            Rational r1 = new Rational(1, 2); // Например, 1/2
            Rational r2 = new Rational(0, 1); // Дробь, у которой числитель 0
                                              // Этот вызов должен выбросить исключение DivideByZeroException
        Rational result = r1 / r2;
        }
    }
}
