using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace ApocOS
{
    public class Kernel : Sys.Kernel
    {
        public Sys.FileSystem.CosmosVFS fs;
        public string current_directory = "0:\\";
        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            int build = 2
            ; Console.WriteLine("ApocOS Build " + build + " boot successful.");
        }

        protected override void Run()
        {
            Console.Write("]");
            string input = Console.ReadLine();
            string com = input.Split(" ")[0];
            switch (com)
            {
                case "quityes":
                    {
                        Console.Write("FAT0x00\nUser shutdown");
                        var timeg = new Random();
                        double time = timeg.Next(5000, 10000);
                        // Console.WriteLine(time);
                        for (int i = 0; i < time; i++)
                        {
                            Console.Write("");
                        }
                        throw new Exception();
                    }
                case "cat":
                    {
                        Console.Write("hi".Split(" ")[0]);
                        CAT();
                        break;
                    }

                case "time":
                    {
                        DateTime now = DateTime.UtcNow;
                        Console.WriteLine(now);
                        break;
                    }

                case "calc":
                    {
                        CALC();
                        break;
                    }
                case "echo":
                    {
                        Console.WriteLine(input.Remove(0, 5));
                        break;
                    }
                case "help":
                    {
                        Console.Write("Available commands:\nquityes: shutdown\ncat: cat program\ntime: displays current time and date\ncalc: calculator program\nhelp: this\n");
                        break;
                    }

                default:
                    Console.Write("ERR0x02\nBad command\n");
                    Console.Write("For a list of commands, type help.\n");
                    break;
            }
        }

        protected void CAT()
        {
            string cat = Console.ReadLine();
            if (cat == "quit")
            {
                Run();
            }
            else { Console.WriteLine(cat); }
        }

        protected void CALC()
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
                case "x":
                    calcans = x * y;
                    Console.WriteLine("value:" + calcans);
                    break;
                case "/":
                    if (y == 0)
                    {
                        Console.WriteLine("ERR0x04\nDivision by zero.");
                        break;
                    }
                    calcans = x / y;
                    Console.WriteLine("value:" + calcans);
                    break;
                default:
                    Console.WriteLine("ERR0x03\nUnknown operation.");
                    break;
            }
        }
    }
}
