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

            Stopwatch sw = new Stopwatch();

            using (StreamWriter strw = new StreamWriter(@"Log\Log.txt"))
            {
                while (number > 0)
                {
                    Console.Write("Unesite velicinu table: ");

                    if (Int32.TryParse(Console.ReadLine(), out number))
                    {
                        if (number > 0)
                        {

                            /*sw.Reset();
                            sw.Start();

                            PositionTree tree = new PositionTree(number);

                            sw.Stop();

                            Console.WriteLine("Broj kombinacija:{0}(sacuvano stablo)", tree.numberOfSolutions);
                            Console.WriteLine("Proteklo vreme:{0}\n", sw.Elapsed);*/

                            strw.Write(Environment.NewLine + "Broj kraljica:" + number + Environment.NewLine);
                            /*strw.Write("Broj kombinacija:{0}(sacuvano stablo)" + Environment.NewLine, tree.numberOfSolutions);
                            strw.Write("Proteklo vreme:{0}\n" + Environment.NewLine + Environment.NewLine, sw.Elapsed);*/

                            sw.Reset();
                            sw.Start();

                            SolutionOnly solution = new SolutionOnly(number);

                            sw.Stop();

                            Console.WriteLine("Broj kombinacija:{0}", solution.numberOfSolutions);
                            Console.WriteLine("Proteklo vreme:{0}\n", sw.Elapsed);
                            strw.Write("Broj kombinacija:{0}" + Environment.NewLine, solution.numberOfSolutions);
                            strw.Write("Proteklo vreme:{0}\n" + Environment.NewLine + Environment.NewLine, sw.Elapsed);

                            //tree = null;
                            solution = null;
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
