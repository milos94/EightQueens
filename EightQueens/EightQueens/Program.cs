using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace EightQueens
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = 1;
            bool saveTree = true;
            bool writeToFile = false;


            Stopwatch sw = new Stopwatch();

            using (StreamWriter strw = new StreamWriter(@"Log\Log.txt"))
            {
                while (number > 0)
                {
                    Console.Write("Unesite velicinu table: ");

                    if (Int32.TryParse(Console.ReadLine(), out number))
                    {
                        Console.Write("Sacuvaj stablo (true/false): ");
                        Boolean.TryParse(Console.ReadLine(), out saveTree);
                        Console.Write("Sacuvaj kombinacije u fajl (true/false): ");
                        Boolean.TryParse(Console.ReadLine(), out writeToFile);

                        if (number > 0)
                        {
                            
                            sw.Reset();
                            sw.Start();

                            PositionTree tree = new PositionTree(number,saveTree,writeToFile);

                            sw.Stop();

                            Console.WriteLine("\nBroj kombinacija:{0}", tree.numberOfSolutions);
                            Console.WriteLine("Proteklo vreme:{0}", sw.Elapsed);
                            Console.WriteLine("Sacuvano stablo: {0}" , ((saveTree) ? "Da" : "Ne"));
                            Console.WriteLine("Upisano u fajl: {0}\n" , ((writeToFile) ? "Da" : "Ne"));

                            strw.WriteLine(Environment.NewLine + "Broj kraljica:" + number);
                            strw.WriteLine("Broj kombinacija:{0}(sacuvano stablo)" , tree.numberOfSolutions);
                            strw.WriteLine("Proteklo vreme:{0}\n", sw.Elapsed);
                            strw.WriteLine("Sacuvano stablo: {0}", ((saveTree) ? "Da" : "Ne"));
                            strw.WriteLine("Upisano u fajl: {0}", ((writeToFile) ? "Da" : "Ne"));
                            tree = null;
                            
                        }

                    }
                    else
                    {
                        number = -1;
                    }
                }
            }

        }
    }
}
