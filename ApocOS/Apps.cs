using System;

namespace Apps
{
    public class Apps
    {
        
    }

    public class Util
    {
        public void CAT()
        {
            string cat = Console.ReadLine();
            if (cat == "quit")
            { return; }
            else { Console.WriteLine(cat); }
        }

        public void CALC()
        {
            string eq = Console.ReadLine();
            string[] eqa = eq.Split(" ");
            double x = double.Parse(eqa[0].ToString());
            string op = eqa[1].ToString();
            double y = double.Parse(eqa[2].ToString());
            Console.WriteLine(op);
            double calcans;
            switch (op)
            {
                case "+":
                    calcans = x + y;
                    Console.WriteLine("value:" + calcans);
                    break;
                case "-":
                    calcans = x - y;
                    Console.WriteLine("value:" + calcans);
                    break;
                case "x" or "*":
                    calcans = x * y;
                    Console.WriteLine("value:" + calcans);
                    break;
                case "/":
                    if (y == 0)
                    {
                        Console.WriteLine("ERR0x12\nDivision by zero.");
                        break;
                    }
                    calcans = x / y;
                    Console.WriteLine("value:" + calcans);
                    break;
                default:
                    Console.WriteLine("ERR0x14\nUnknown operation.");
                    break;
            }
        }
    }
    public class NEF
    {
        
    }
}