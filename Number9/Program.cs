using System;
using System.Collections.Generic;
using System.Threading;

namespace Number9
{
    /// <summary>
    /// Результаты соревнований фигуристов по одному из видов многоборья представлены оценками 7 судей в баллах (от 0,0 до 6,0).
    /// По результатам оценок судьи определяется место каждого участника у этого судьи.
    /// Места участников определяются далее по сумме мест, которые каждый участник занял у всех судей.
    /// Составить программу, определяющую по исходной таблице оценок фамилии и сумму мест участников в порядке занятых ими мест.
    /// </summary>
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] sportmensSurnames =
            {
                "Рокоссовский",
                "Пермяков",
                "Горемыкин",
                "Степанков",
                "Чиграков",
                "Чукчов",
                "Моренов",
                "Черняков",
                "Сусоев",
                "Львов",
                "Боголюбов",
                "Трусов",
                "Денисов",
                "Любов",
                "Куклачёв",
                "Кораблин",
                "Шульга",
                "Изюмов",
                "Дёмин",
                "Сутулин"
            };

            Sportsman[] sportsmens = new Sportsman[sportmensSurnames.Length];
            for (int i = 0; i < sportsmens.Length; i++)
            {
                sportsmens[i] = new Sportsman(sportmensSurnames[i]);
                Thread.Sleep(10);
            }

            List<Judge> judges = new List<Judge>();
            for (int i = 0; i < 7; i++)
            {
                judges.Add(new Judge(ref sportsmens));
            }
            
            Array.Sort(sportsmens, (x, y) => y.TotalScore.CompareTo(x.TotalScore));
            int place = 1;
            foreach (var t in sportsmens)
            {
                Console.WriteLine($"{place++}: {t.Surname} - {Math.Round(t.TotalScore, 2)}");
            }
        }
    }

    public class Human
    {
        public int Id { get; internal set; }
        public string Surname { get; internal set; }

        public Human(string surname)
        {
            Surname = surname;
        }
    }

    public class Sportsman : Human
    {
        private static int idContainer = 0;
        public double TotalScore { get; private set; }

        public Sportsman(string surname) : base(surname)
        {
            Id = idContainer++;
            Surname = surname;
            TotalScore = 0;
        }

        public void ChangeScore(double score)
        {
            TotalScore += score;
        }
    }


    public class Judge : Human
    {
        private static int idContainer = 0;
        public Sportsman[] Sportsmens { get; }
        public double[] Scores { get; }
        public Judge(ref Sportsman[] sportmens) : base($"Mr. #{idContainer}")
        {
            Id = idContainer++;
            Sportsmens = sportmens;
            Random random = new Random();
            Scores = new double[sportmens.Length];
            
            for (int i = 0; i < Scores.Length; i++)
            {
                Scores[i] = random.Next(0, 5) + random.NextDouble();
                Sportsmens[i].ChangeScore(random.Next(0, 5) + random.NextDouble());
            }
        }
    }
}