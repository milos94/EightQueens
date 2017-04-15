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
            public char X { get; set; }
            public int Y { get; set; }
            public List<PositionNode> next { get; set; }
            public PositionNode()
            {
                next = new List<PositionNode>();
            }
        }

        private PositionNode root;
        public Int64 numberOfSolutions { get; internal set; }
        private int depth;
        private List<PositionNode> soFar;
        private StreamWriter sw;
        private bool saveTree;
        private bool writeToFile;

        public PositionTree(int numblerOfQueens, bool saveTree, bool writeToFile)
        {
            numberOfSolutions = 0;
            depth = numblerOfQueens;
            soFar = new List<PositionNode>();

            this.writeToFile = writeToFile;
            this.saveTree = saveTree;

            if (writeToFile)
            {
                sw = new StreamWriter(@"Log\Queens_" + numblerOfQueens + "_" + DateTime.Now + ".txt");
            }

            root = new PositionNode();
            root.X = (char)96; root.Y = -1;

            for (int i = 1; i <= depth; i++)
            {
                PositionNode temp = new PositionNode();
                temp.Y = 1;
                temp.X = (char) (96+i);
                soFar = new List<PositionNode>();
                soFar.Add(temp);
                root.next.Add(temp);
                if (!(addNode(temp, 2)))
                {
                    root.next.RemoveAt(root.next.Count - 1);
                }

            }
            if (writeToFile)
            {
                sw.Close();
            }
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
                temp.X = (char) (96+i);
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
                    ++numberOfSolutions;
                    if (writeToFile)
                    {
                        writePath(temp);
                    }

                    return true;
                }

                soFar.Add(temp);

                if ((addNode(temp, level + 1)) && saveTree)
                {
                    soFar.RemoveAt(soFar.Count - 1);
                    continue;
                }

                p.next.RemoveAt(p.next.Count - 1);

                soFar.RemoveAt(soFar.Count - 1);
            }
            if (p.next.Count == 0 && level != depth)
            {
                return false;
            }

            return true;
        }

        private void writePath(PositionNode p)
        {
            sw.Write((numberOfSolutions) + ":{");
            foreach (PositionNode po in soFar)
            {
                sw.Write( po.X + "" + po.Y + " ; ");
            }
            sw.Write( p.X + "" + p.Y + "}" + Environment.NewLine);
        }
    }
}
