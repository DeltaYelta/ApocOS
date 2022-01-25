using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using IO = System.IO;
using Apps;

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
            Util util = new Util();
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
                        util.CAT();
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
                        util.CALC();
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
                        Console.WriteLine("calc: calculator program");
                        Console.WriteLine("echo: echoes user input to the console");
                        Console.WriteLine("cls: clears screen");
                        Console.WriteLine("diskstat: disk status (WIP)");
                        Console.WriteLine("dir: smaller ls, just lists files");
                        Console.WriteLine("ls: bigger dir, lists extensions and sizes");
                        Console.WriteLine("help: this");
                        break;
                    }

                default:
                    Console.Write("ERR0x10\nBad command\n");
                    Console.Write("For a list of commands, type help.\n");
                    break;
            }
        }
    }
}
