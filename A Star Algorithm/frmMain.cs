﻿/*
 *  A star Algorithm
 * 
 * 2020-02-03 Coded by GwangSu Lee 
 * 
 * 
 * 
 * void FindAStarPath(Path start, Path goal, Func<Path,Path,float> heuristic)
 * 
 * 결과    Success : goal 부터 백트래킹 하여 Path.pathType 속성을 PATH로 바꿈,
 *                   Goal에서 부터 Path.Before을 백트래킹 하여 추적하면 최단경로가 됨.
 *         Fail : Not Found 메시지 박스
 *         
 *         
 *         
 * void Dijkstra(Path start, Path goal)
 * 
 * 결과    Success : goal 부터 백트래킹 하여 Path.pathType 속성을 PATH로 바꿈,
 *                   Goal에서 부터 Path.Before을 백트래킹 하여 추적하면 최단경로가 됨.
 *         Fail : Not Found 메시지 박스
 * 
 * 
 * A Star reference : https://en.wikipedia.org/wiki/A*_search_algorithm
 * Dijkstra reference : https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A_Star_Algorithm
{
    public partial class frmMain : Form
    {
        Random rnd = new Random();
        Bitmap drawImage;

        int map_width;
        int map_height;

        List<Path> world = new List<Path>();

        List<Path> open_set = new List<Path>();
        List<Path> closed_set = new List<Path>();

        Path start, end;

        enum RUN_MODE
        {
            ANIMATED,
            SOLUTION
        }

        RUN_MODE run_mode = RUN_MODE.ANIMATED;

        public frmMain()
        {
            InitializeComponent();

            drawImage = new Bitmap(picCanvas.Width, picCanvas.Height);
            Graphics.FromImage(drawImage).Clear(Color.White);

            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);

            map_width = picCanvas.Width / trackSize.Value;
            map_height = picCanvas.Height / trackSize.Value;

            MakeMap();



            //Fill Start and End Position 
            start = world[map_width];
            start.pathType = Path.TYPE.START;
            end = world[map_width * map_height - map_width - 1];
            end.pathType = Path.TYPE.END;
        }

        private Path GetRandomPath(int x, int y, int size)
        {
            if (rnd.NextDouble() <= 0.1)
            {
                return new Path(x, y, size, Path.TYPE.WALL);
            }
            else
            {
                return new Path(x, y, size, Path.TYPE.ROAD);
            }
        }

        private void MakeNeighbors()
        {
            //Setting Neighbors
            for (int i = 0; i < world.Count; i++)
            {
                if (i - 1 >= 0)
                    world[i].AddNeghibor(world[i - 1]);
                if (i + 1 < world.Count)
                    world[i].AddNeghibor(world[i + 1]);
                if (i - map_width >= 0)
                    world[i].AddNeghibor(world[i - map_width]);
                if (i + map_width < world.Count)
                    world[i].AddNeghibor(world[i + map_width]);
            }
        }
        private void MakeMap()
        {
            int size = (Math.Max(picCanvas.Width, picCanvas.Height) / map_width);

            //Randomize Road And Wall
            for(int i = 0; i < map_height; i++)
            {
                for(int j = 0; j < map_width; j++)
                {
                    world.Add(GetRandomPath(j, i, size));
                }
            }

            //Fill Boundary(Draw Big Rectangle)
            for (int i = 0; i < map_width; i++)
            {
                world[i].pathType = Path.TYPE.WALL;
                world[((map_height - 1)*map_width) + i].pathType = Path.TYPE.WALL;
            }
            for (int i = 1; i < map_height; i++)
            {
                world[i*map_width].pathType = Path.TYPE.WALL;
                world[i*map_width - 1].pathType = Path.TYPE.WALL;
            }


            //Making Big Wall
            for (int k = 0; k < 4; k++)
            {
                int rndWallX = rnd.Next(4, map_width / 2);
                int rndWallY = rnd.Next(4, map_height / 2);
                int rndWallWidth = rndWallX + rnd.Next(4, map_width / 2);
                int rndWallHeight = rndWallY + rnd.Next(4, map_height / 2);
                for (int i = 0; i < map_width; i++)
                {
                    for (int j = 0; j < map_height; j++)
                    {
                        if (i >= rndWallX && j > rndWallY && rndWallWidth > i && rndWallHeight > j)
                            world[i + j * map_width].pathType = Path.TYPE.WALL;
                    }
                }
            }


            Graphics g = Graphics.FromImage(drawImage);
            foreach (Path path in world)
            {
                path.Draw(g);
            }
        }

        private float CostDistance(Path current, Path target)
        {
            int dx = (target.X - current.X);
            int dy = (target.Y - current.Y);

            return ((dx * dx) + (dy * dy));
        }
        private float Distance(int x1, int y1, int x2, int y2)
        {
            int dx = (x2 - x1);
            int dy = (y2 - y1);

            return ((dx * dx) + (dy * dy));
        }


        private void BacktrackingPath(Path start)
        {
            Path curr = start.Before;
            while(! (curr.Before is null) )
            {
                curr.pathType = Path.TYPE.PATH;
                curr = curr.Before;
            }
        }
        private void tmrASTARAnimate_Tick(object sender, EventArgs e)
        {
            //Drawing
            Graphics g = Graphics.FromImage(drawImage);
            bool isFound = false;
            
            //while(OpenSet > 0)
            //한프레임당 1번씩 수행
            if (open_set.Count > 0)
            {
                //lowst Openset Node by hScore
                open_set.Sort(delegate (Path a, Path b) { return a.h.CompareTo(b.h); });
                Path current = open_set[0];

                if (current.pathType == Path.TYPE.END)
                {
                    tmrASTARAnimate.Enabled = false;
                    isFound = true;
                    //we Found!
                    BacktrackingPath(current);
                    
                    foreach (Path path in world)
                    {
                        path.Draw(g);
                    }
                    picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);
                    return;
                }

                open_set.Remove(current);
                closed_set.Add(current);

                foreach (Path neighb in current.neighbors)
                {
                    if (closed_set.Contains(neighb)) continue;

                    float best_neighb_g = current.g + Distance(current.X, current.Y, neighb.X, neighb.Y);

                    if (best_neighb_g < neighb.g)
                    {
                        neighb.Before = current;
                        neighb.g = best_neighb_g;

                        neighb.h = neighb.g + CostDistance(neighb, end);

                        if (open_set.Contains(neighb) == false)
                        {
                            open_set.Add(neighb);
                        }
                    }
                }
            }

            foreach (Path path in world)
            {
                path.Draw(g);
            }

            foreach (Path path in open_set)
            {
                if (path.pathType == Path.TYPE.ROAD)
                    path.Draw(Graphics.FromImage(drawImage),Color.Aqua);
            }

            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);

            if (!isFound && open_set.Count == 0)
            {
                tmrASTARAnimate.Enabled = false;
                MessageBox.Show("No Solution!");
            }
        }

        private void Dijkstra(Path start, Path goal)
        {
            List<Path> openSet = new List<Path>();

            foreach(Path path in world)
            {
                if (path.pathType != Path.TYPE.WALL)
                {
                    path.dist = float.MaxValue;
                    path.Before = null;
                    openSet.Add(path);
                }
            }
            start.dist = 0;

            bool isFound = false;

            while(openSet.Count > 0)
            {
                openSet.Sort((Path a, Path b) => { return a.dist.CompareTo(b.dist); });
                Path current = openSet[0];

                openSet.Remove(current);

                if (current.pathType == Path.TYPE.END)
                {
                    //도착
                    isFound = true;
                    BacktrackingPath(current);
                    break;
                }

                foreach (Path neighb in current.neighbors)
                {
                    float bestDistance = current.dist + Distance(current.X, current.Y, neighb.X, neighb.Y);

                    if (bestDistance < neighb.dist)
                    {
                        neighb.Before = current;
                        neighb.dist = bestDistance;
                    }
                }
            }


            Graphics g = Graphics.FromImage(drawImage);
            foreach (Path path in world)
            {
                path.Draw(g);
            }
            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);

            if (isFound == false)
            {
                MessageBox.Show("No Solution!");
            }

        }

        private void FindAStarPath(Path start, Path goal, Func<Path,Path,float> heuristic)
        {
            List<Path> openSet = new List<Path>();
            List<Path> closedSet = new List<Path>();

            //Astar Preload
            start.g = 0;
            start.h = heuristic(start, end);
            openSet.Add(start);

            bool isFound = false;

            while(openSet.Count > 0)
            {
                //lowst Openset Node by hScore
                openSet.Sort(delegate (Path a, Path b) { return a.h.CompareTo(b.h); });
                Path current = openSet[0];

                if (current.pathType == Path.TYPE.END)
                {
                    isFound = true;
                    BacktrackingPath(current);
                    break;
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (Path neighb in current.neighbors)
                {
                    if (closedSet.Contains(neighb)) continue;

                    float best_neighb_g = current.g + Distance(current.X, current.Y, neighb.X, neighb.Y);

                    if (best_neighb_g < neighb.g)
                    {
                        neighb.Before = current;
                        neighb.g = best_neighb_g;

                        neighb.h = neighb.g + heuristic(neighb, end);

                        if (openSet.Contains(neighb) == false)
                        {
                            openSet.Add(neighb);
                        }
                    }
                }
            }

            
            Graphics g = Graphics.FromImage(drawImage);
            foreach (Path path in world)
            {
                path.Draw(g);
            }
            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);

            if (isFound == false)
            {
                MessageBox.Show("No Solution!");
            }
        }

        private void btnAStarSolutionOnly_Click(object sender, EventArgs e)
        {
            DisableButtons();
            
            //Setting Neighbors
            MakeNeighbors();
            //Astar Start
            FindAStarPath(start, end, CostDistance);

            EnableButtons();
        }

        private void trackSize_Scroll(object sender, EventArgs e)
        {
            DisableButtons();

            map_width = picCanvas.Width / trackSize.Value;
            map_height = picCanvas.Height / trackSize.Value;

            world.Clear();
            MakeMap();

            Graphics g = Graphics.FromImage(drawImage);
            foreach (Path path in world)
            {
                path.Draw(g);
            }
            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);

            EnableButtons();
        }

        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);
        }

        
        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            int x = (int)(e.X / trackSize.Value);
            int y = (int)(e.Y / trackSize.Value);

            Path curr = world[x + y *map_width ];

            if (e.Button == MouseButtons.Left)
            {
                start.pathType = Path.TYPE.WALL;
                start = curr;
                start.pathType = Path.TYPE.START;
            }
            else if (e.Button == MouseButtons.Right)
            {
                end.pathType = Path.TYPE.WALL;
                end = curr;
                end.pathType = Path.TYPE.END;
            }

            Graphics g = Graphics.FromImage(drawImage);
            foreach (Path path in world)
            {
                path.Draw(g);
            }
            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            DisableButtons();

            map_width = picCanvas.Width / trackSize.Value;
            map_height = picCanvas.Height / trackSize.Value;

            world.Clear();
            MakeMap();

            Graphics g = Graphics.FromImage(drawImage);
            foreach (Path path in world)
            {
                path.Draw(g);
            }
            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);

            EnableButtons();
        }

        private void btnDijkstaStart_Click(object sender, EventArgs e)
        {
            DisableButtons();

            //Setting Neighbors
            MakeNeighbors();
            Dijkstra(start, end);

            EnableButtons();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            DisableButtons();

            //Setting Neighbors
            MakeNeighbors();
            //Astar Preload
            open_set.Clear();
            closed_set.Clear();
            start.g = 0;
            start.h = CostDistance(start, end);
            open_set.Add(start);

            Graphics g = Graphics.FromImage(drawImage);
            foreach (Path path in world)
            {
                path.Draw(g);
            }
            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);
            tmrASTARAnimate.Enabled = true; //Astar Start!
        }


        private void btnDijkstraStartAnimate_Click(object sender, EventArgs e)
        {
            DisableButtons();

            //Dijkstra Preload
            MakeNeighbors();
            open_set.Clear();
            foreach (Path path in world)
            {
                if (path.pathType != Path.TYPE.WALL)
                {
                    path.dist = float.MaxValue;
                    path.Before = null;
                    open_set.Add(path);
                }
            }
            start.dist = 0;
            tmrDijkstraAnimate.Enabled = true;
        }

        private void tmrDijkstraAnimate_Tick(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(drawImage);

            bool isFound = false;

            if (open_set.Count > 0)
            {
                open_set.Sort((Path a, Path b) => { return a.dist.CompareTo(b.dist); });
                Path current = open_set[0];

                open_set.Remove(current);

                if (current.pathType == Path.TYPE.END)
                {
                    //도착
                    tmrDijkstraAnimate.Enabled = false;
                    isFound = true;
                    BacktrackingPath(current);

                    foreach (Path path in world)
                    {
                        path.Draw(g);
                    }


                    picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);

                    return;
                }

                foreach (Path neighb in current.neighbors)
                {
                    float bestDistance = current.dist + Distance(current.X, current.Y, neighb.X, neighb.Y);

                    if (bestDistance < neighb.dist)
                    {
                        neighb.Before = current;
                        neighb.dist = bestDistance;
                    }
                }
            }

            foreach (Path path in world)
            {
                path.Draw(g);
            }

            foreach (Path path in open_set)
            {
                if (path.pathType == Path.TYPE.ROAD)
                    path.Draw(Graphics.FromImage(drawImage), Color.Aqua);
            }



            //시작지점과 종료지점프린팅

            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);

            if (!isFound && open_set.Count == 0)
            {
                tmrDijkstraAnimate.Enabled = false;
                MessageBox.Show("No Solution!");
            }
        }
        private void DisableButtons()
        {
            btnDijkstraStartAnimate.Enabled = false;
            btnDijkstaStart.Enabled = false;
            tmrDijkstraAnimate.Enabled = false;
            btnAStarSolutionOnly.Enabled = false;
            btnStart.Enabled = false;
            tmrASTARAnimate.Enabled = false;
        }
        private void EnableButtons()
        {
            btnDijkstraStartAnimate.Enabled = true;
            btnDijkstaStart.Enabled = true;
            btnAStarSolutionOnly.Enabled = true;
            btnStart.Enabled = true;
        }
    }
}
