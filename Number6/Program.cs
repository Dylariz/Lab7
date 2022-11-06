using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Number6
{
    /// <summary>
    /// Протокол соревнований по прыжкам в воду содержит список фамилий спортсменов и баллы, выставленные 5 судьями по результатам 2 прыжков.
    /// Получить итоговый протокол, содержащий фамилии и результаты, в порядке занятых спортсменами мест по результатам 2 прыжков. 
    /// </summary>
    internal class Program
    {
        public static void Main(string[] args)
        {
            int n = 15;
            string[] surnames =
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
            
            List<Sportsmen> sportsmens = new List<Sportsmen>();
            for (int i = 0; i < n; i++)
            {
                Jump jump1 = new Jump(surnames[new Random().Next(surnames.Length)], Support.GenerateArray(5, (0, 10)));
                Thread.Sleep(50);
                Jump jump2 = new Jump(surnames[new Random().Next(surnames.Length)], Support.GenerateArray(5, (0, 10)));
                sportsmens.Add(new Sportsmen(new []{jump1, jump2}));
            }
            
            sportsmens.Sort((x, y) => y.TotalScore.CompareTo(x.TotalScore));
            int place = 1;
            foreach (var t in sportsmens)
            {
                Console.WriteLine($"{place++}: {t.Surname} - {t.TotalScore}");
            }
        }
        
    }
    
    public class Jump
    {
        public string Surname { get; set; }
        public int[] Scores { get; set; }
        public int TotalScore { get; set; }
        
        public Jump(string name, int[] scores)
        {
            Surname = name;
            Scores = scores;
            TotalScore = 0;
            foreach (var t in Scores)
            {
                TotalScore += t;
            }
        }
    }

    public class Sportsmen
    {
        public string Surname { get; set; }
        public Jump[] Jumps { get; set; }
        public int TotalScore { get; set; }
        
        public Sportsmen(Jump[] jumps)
        {
            Surname = jumps[0].Surname;
            Jumps = jumps;
            TotalScore = 0;
            foreach (var t in Jumps)
            {
                TotalScore += t.TotalScore;
            }
        }
    }
    
    public static class Support
    {
        public static int[] GenerateArray(int n, (int, int) range)
        {
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(range.Item1, range.Item2 + 1);
            }

            return array;
        }
    }
}