using System;
using System.Collections.Generic;

namespace Hospital
{
    public abstract class Worker
    {
        public string Name { get; set; }

        public abstract double TotalReward { get; }

        public abstract double Hours { get; }

        public double Salary => TotalReward / Hours;

        public List<string> Tools { get; set; }

        public double CalculateSalary(double money, double hour)
        {
            return Math.Round(money / hour, 2);
        }
    }

    public class Plumber : Worker
    {
        public Guid Id { get; } = Guid.NewGuid();

        public Plumber()
        {
            //模拟创建对象比较耗时
            System.Threading.Thread.Sleep(2000);

            Tools = new List<string>()
            {
                "螺丝刀",
                "扳子",
                "钳子"
            };
        }

        public override double TotalReward => 200;

        public override double Hours => 3;
    }

    public class Programmer : Worker
    {
        public override double TotalReward => 1000;

        public override double Hours => 3.5;
    }
}
