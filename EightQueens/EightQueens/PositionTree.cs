using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens
{
    class PositionTree
    {
        private class PositionNode
        {
            public int X { get; set; }
            public int Y { get; set; }
            public List<PositionNode> next { get; set; }
            public PositionNode prevous { get; set; }
            public PositionNode()
            {
                next = new List<PositionNode>();
                prevous = null;
            }
        }

        private PositionNode root { get; }
        public Int64 numberOfSolutions { get; set; }
        private int depth;
        private List<PositionNode> soFar;
        private StreamWriter sw;


        public PositionTree(int numblerOfQueens)
        {
            numberOfSolutions = 0;
            depth = numblerOfQueens;
            soFar = new List<PositionNode>();

            sw = new StreamWriter(@"Log\Queens_" + numblerOfQueens + "_" + DateTime.Now + ".txt");

            root = new PositionNode();
            root.X = root.Y = -1;

            for (int i = 1; i <= depth; i++)
            {
                PositionNode temp = new PositionNode();
                temp.Y = 1;
                temp.X = i;
                soFar = new List<PositionNode>();
                soFar.Add(temp);
                root.next.Add(temp);
                if (!(addNode(temp, 2)))
                {
                    root.next.RemoveAt(root.next.Count - 1);
                }

            }
            sw.Close();
        }

        private bool check(PositionNode a, PositionNode b)
        {
            if (a.X == b.X)
            {
                return false;
            }
            if (Math.Abs(a.X - b.X) == Math.Abs(a.Y - b.Y))
            {
                return false;
            }
            return true;
        }

        private bool addNode(PositionNode p, int level)
        {
            for (int i = 1; i <= depth; i++)
            {
                bool flag = false;
                PositionNode temp = new PositionNode();
                temp.X = i;
                temp.Y = level;
                foreach (PositionNode po in soFar)
                {
                    if (!(check(po, temp)))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    continue;
                }

                p.next.Add(temp);

                if (level == depth)
                {

                    sw.Write((++numberOfSolutions) + ":{");
                    foreach (PositionNode po in soFar)
                    {
                        sw.Write("(" + po.Y + "," + po.X + ") ; ");
                    }
                    sw.Write("("+p.Y+","+p.X+")}" + Environment.NewLine);
                    return true;
                }

                soFar.Add(temp);
                if (!(addNode(temp, level + 1)))
                {
                    p.next.RemoveAt(p.next.Count - 1);
                }
                soFar.RemoveAt(soFar.Count - 1);
            }
            if (p.next.Count == 0 && level != depth)
            {
                return false;
            }

            return true;
        }
    }
}
