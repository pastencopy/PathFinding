using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Star_Algorithm
{
    class Path
    {
        public enum TYPE
        {
            ROAD,  //이동 가능한 길
            WALL,  //이동 불가 벽
            START, //시작점
            PATH,  //최단경로 길
            END,   //목표지점
        }

        public int X;
        public int Y;

        public int Size;
        
        public TYPE pathType;

        public float h = float.MaxValue; //A star hScore
        public float g = float.MaxValue; //A star gScore

        public float dist = float.MaxValue; //Dijkstra Distance

        public List<Path> neighbors = new List<Path>();

        public Path Before;
        
        public Path(int x, int y, int Size, TYPE pathType)
        {
            this.X = x;
            this.Y = y;
            this.Size = Size;
            this.pathType = pathType;
        }

        //이웃 노드 추가
        public void AddNeghibor(Path neighb)
        {
            if (neighb.pathType == TYPE.WALL) return;
            neighbors.Add(neighb);
        }

        public void Draw(Graphics g)
        {
            Brush b = Brushes.White;
            if (pathType == TYPE.START)
            {
                b = Brushes.DeepSkyBlue;
            }
            else if (pathType == TYPE.END)
            {
                b = Brushes.GreenYellow;
            }
            else if (pathType == TYPE.WALL)
            {
                b = Brushes.Black;
            }
            else if (pathType == TYPE.PATH)
            {
                b = Brushes.Red;
            }

            g.FillRectangle(b, X * Size, Y * Size, Size, Size);
        }

        public void Draw(Graphics g, Color color)
        {
            g.FillRectangle(new SolidBrush(color) , X * Size, Y * Size, Size, Size);
        }

    }
}
