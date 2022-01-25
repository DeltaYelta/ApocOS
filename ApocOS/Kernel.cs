using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using IO = System.IO;

namespace ApocOS
{
    public class Kernel : Sys.Kernel
    {
        public Sys.FileSystem.CosmosVFS fs;
        public string cudr = "0:\\";
        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            int build = 2
            ; Console.WriteLine("ApocOS Build " + build + " boot successful.");
        }

        protected override void Run()
        {
            Console.Write("] ");
            string input = Console.ReadLine();
            string com = input.Split(" ")[0];
            switch (com)
            {
                case "shutdown":
                    {
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
                case "cls":
                    {
                        Console.Clear();
                        break;
                    }
                case "diskstat":
                    {
                        long dssp = fs.GetAvailableFreeSpace(@"0:\");
                        string fstp = fs.GetFileSystemType(@"0:\");
                        Console.WriteLine("FS type: " + fstp);
                        Console.WriteLine("Free space: " + dssp);
                        break;
                    }
                case "dir":
                    {
                        Console.WriteLine(IO.Directory.GetFiles(cudr));
                        break;
                    }
                case "ls":
                    {
                        var cudrfl = IO.Directory.GetFiles(cudr);
                        try
                        {

                            foreach (var file in cudrfl)
                            {
                                //Console.WriteLine(file);
                                try
                                {
                                    string[] fnar = file.Split(".");
                                    string entry = fnar[^1] + "|\t" + fnar[0] + ":";
                                    Console.Write(entry);
                                    for (int i = 0; i < 20 - entry.Length; i++)
                                    {
                                        Console.Write(" ");
                                    }
                                    Console.Write(entry.Length + "\n");

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if(ex.ToString() == "FileNotFoundException")
                            {
                                Console.WriteLine("No files in current directory.");
                            } else
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                        break;
                    }
                case "help":
                    {
                        Console.WriteLine("Available commands:");
                        Console.WriteLine("shutdown: shutdown");
                        Console.WriteLine("cat: cat program");
                        Console.WriteLine("time: displays current time and date");
                        Console.WriteLine("calc: calculator program")
                        Console.WriteLine("help: this");
                        break;
                    }

                default:
                    Console.Write("ERR0x10\nBad command\n");
                    Console.Write("For a list of commands, type help.\n");
                    break;
            }
        }

        private void CAT()
        {
            string cat = Console.ReadLine();
            if (cat == "quit")
            {
                Run();
            }
            else { Console.WriteLine(cat); }
        }

        private void CALC()
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
}
