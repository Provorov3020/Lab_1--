using System;
namespace RationalNumbers
{
    public class Rational
    {
        private int numerator;
        private int denominator;
        private int v;

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
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return new Rational(r1.numerator * r2.denominator, r1.denominator * r2.numerator);
        }

        public override string ToString()
        {
            return $"{numerator}/{denominator}";
        }

        public static bool operator ==(Rational r1, Rational r2)
        {
            return r1.Equals(r2);
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

        public override bool Equals(object obj)
        {
            if (obj is Rational)
            {
                return this == (Rational)obj;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (numerator, denominator).GetHashCode();
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
              
    }
    public class Program
    {
        public static void Main()
        {
            try
            {
                Rational r1 = new Rational(5, -10); // 5/-10 = -1/2
                Rational r2 = new Rational(-3, 4); // -3/4
                Rational result = r1 + r2; // Операция сложения
                Rational r3 = new Rational(6, 6); // 6/6 = 1
                Rational r4 = new Rational(-12, 24); // -12/24 = -1/2
                Rational result1 = r4 - r2; // Операция вычитания
                Rational result2 = r1 * r4; // Операция умножения
                Rational result3 = r3 / r2; // Операция деления          
                Console.WriteLine($"Вывод дроби: {r1}"); // Дробь r1
                Console.WriteLine($"Вывод дроби: {r2}"); // Дробь r2
                Console.WriteLine($"Результат сложения: {result}"); // Вывод результата -5/4
                Console.WriteLine($"Вывод целой дроби: {r3}"); // Дробь r3
                Console.WriteLine($"Вывод дроби: {r4}"); // Дробь r4
                Console.WriteLine($"Результат вычитания: {result1}"); // Вывод результата 1/4
                Console.WriteLine($"Результат умножения: {result2}"); // Вывод результата 1/4
                Console.WriteLine($"Результат деления: {result3}"); // Вывод результата -4/3
            }
            catch (Exception ex)

            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
    



