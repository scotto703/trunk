/*
 * Copyright (c) 2009, openmetaverse.org 
 * All rights reserved.
 *
 * - Redistribution and use in source and binary forms, with or without
 *   modification, are permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this
 *   list of conditions, the donated by lines below and the following disclaimer.
 * - Neither the name of the Second Life Reverse Engineering Team nor the names
 *   of its contributors may be used to endorse or promote products derived from
 *   this software without specific prior written permission.
 * 
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 * 
 * DONATED TO OPENMETAVERSE BY Dimentox Travanti Of DCS2.
 * 
 */
using OpenMetaverse;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AStar
{

    #region Static pathfinding functions

    public class Pathfinding
    {
        private class PathNode
        {
            private Vector3 _Position;

            /// <summary>
            /// A container for a node position which includes details for calculating movement cost
            /// </summary>
            /// <param name="position"></param>
            /// <param name="parent"></param>
            public PathNode(Vector3 position)
            {
                _Position = position;
            }

            /// <summary>
            /// The coordinates of this node
            /// </summary>
            public Vector3 Position
            {
                get { return _Position; }
            }

            /// <summary>
            /// The parent of this node
            /// </summary>
            public Vector3 Parent;

            /// <summary>
            /// The cost of moving from to this node from its parent (10 if adjacent, 14 if diagonal)
            /// </summary>
            public int G;

            /// <summary>
            /// The "Manhattan method" score for this node in relation to the target position
            /// </summary>
            public int H;

            /// <summary>
            /// The total cost of chosing this node
            /// </summary>
            public int F;
        }

        /// <summary>
        /// Checks the specified location on the wander map to see if we are permitted to go there
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true if we are allowed to go there, false if we are not</returns>
        public static bool MapCheckPosition(Bitmap map, Vector3 position)
        {
            //map = (Bitmap)map.Clone();

            int x = (int)position.X;
            int y = 255 - (int)position.Y;
            //int z = 0; //2D

            if (map == null || x > 255 || y > 255 || x < 0 || y < 0) return false;

            List<Color> pixels = new List<Color>();
            pixels.Add(map.GetPixel(x, y));
            //if (y + 1 <= 255) pixels.Add(map.GetPixel(x, y + 1));
            //if (y - 1 >= 0) pixels.Add(map.GetPixel(x, y - 1));
            //etc... add all 9

            foreach (Color c in pixels)
            {
                if (c.R != 00 || c.B != 00 || c.G != 00) return false;
            }
            return true;
        }

        /// <summary>
        /// Gets a random point that is walkable and returns it.
        /// </summary>
        /// <returns>Vector3</returns>
        public static Vector3 GetRandomPoint(Bitmap map, Vector3 currentPosition)
        {
            Vector3 target = new Vector3();
            int count = 0;
            while (count < 65536)
            {
                Random rand = new Random();
                target.X = (int)rand.Next(256);
                target.Y = (int)rand.Next(256);
                target.Z = currentPosition.Z; //2D
                if (AStar.Pathfinding.MapCheckPosition(map, target)) break;
                count++;
            }
            if (count >= 256)
            {
                throw new Exception("Could Not find a point within a decent time"); // Do not want a never ending loop
            }
            return target;
        }

        /// <summary>
        /// The closest node to the specified position
        /// </summary>
        /// <param name="position"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static Vector3 ClosestNode(Vector3 position, List<Vector3> nodes)
        {
            if (nodes.Count == 0) return position;
            float best = -1;
            Vector3 closest = Vector3.Zero;
            foreach (Vector3 node in nodes)
            {
                float dist = Vector3.Distance(position, node);
                if (best < 0 || dist < best)
                {
                    best = dist;
                    closest = node;
                }
            }
            return closest;
        }

        /// <summary>
        /// Heuristics - contributed by z4ppy
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static int GetHeuristics(Vector3 startPoint, Vector3 endPoint)
        {
            int xDistance;
            int yDistance;
            int H;

            xDistance = (int)Math.Abs(startPoint.X - endPoint.X);
            yDistance = (int)Math.Abs(startPoint.Y - endPoint.Y);

            if (xDistance > yDistance)
                H = 14 * yDistance + 10 * (xDistance - yDistance);
            else
                H = 14 * xDistance + 10 * (yDistance - xDistance);

            return H;

        }

        /// <summary>
        /// Returns an efficient path from pointA to pointB
        /// </summary>
        /// <param name="target"></param>
        /// <returns>A list of nodes, or null if no path is available</returns>
        public static List<Vector3> AStarPath(Bitmap map, Vector3 pointA, Vector3 pointB)
        {
            pointA = new Vector3((int)pointA.X, (int)pointA.Y, 0); //2D
            pointB = new Vector3((int)pointB.X, (int)pointB.Y, 0); //2D
            List<Vector3> path = new List<Vector3>();
            if (!MapCheckPosition(map, pointA) || !MapCheckPosition(map, pointB)) return path;

            Dictionary<Vector3, PathNode> open = new Dictionary<Vector3, PathNode>();
            Dictionary<Vector3, PathNode> closed = new Dictionary<Vector3, PathNode>();

            //Add the starting square (or node) to the open list.
            PathNode current = new PathNode(pointA);
            current.Parent = pointA; //the start parent is itself (as a marker)
            current.G = 0;
            current.H = GetHeuristics(pointA, pointB);
            //current.H = (int)Math.Abs(pointB.X - pointA.X) + (int)Math.Abs(pointB.Y - pointA.Y);
            current.F = current.G + current.H;
            open.Add(current.Position, current);

            do
            {
                //Look for the lowest F cost square on the open list.
                //We refer to this as the current square.
                Nullable<int> lowest = null;
                foreach (PathNode node in open.Values)
                {
                    if (lowest == null || node.F < lowest)
                    {
                        lowest = node.F;
                        current = node;
                    }
                }
                if (lowest == null) throw new Exception("No available path was detected.");
                PathNode parent = current;

                //Switch it to the closed list.
                open.Remove(current.Position);
                closed.Add(current.Position, current);

                if (current.Position == pointB) break;

                //For each of the 8 squares adjacent to this current square...
                int x = (int)current.Position.X;
                int y = (int)current.Position.Y;
                int z = 0; //2D

                List<Vector3> adjacent = new List<Vector3>();
                adjacent.Add(new Vector3(x + 1, y + 1, z));
                adjacent.Add(new Vector3(x - 1, y - 1, z));
                adjacent.Add(new Vector3(x + 1, y - 1, z));
                adjacent.Add(new Vector3(x - 1, y + 1, z));
                adjacent.Add(new Vector3(x + 1, y, z));
                adjacent.Add(new Vector3(x - 1, y, z));
                adjacent.Add(new Vector3(x, y + 1, z));
                adjacent.Add(new Vector3(x, y - 1, z));
                foreach (Vector3 position in adjacent)
                {
                    //If it is not walkable or if it is on the closed list, ignore it.
                    if (!MapCheckPosition(map, position) || closed.ContainsKey(position)) continue;
                    //Otherwise do the following.
                    //If it isn�t on the open list, add it to the open list.
                    if (!open.ContainsKey(position))
                    {
                        open.Add(position, new PathNode(position));
                        //Make the current square the parent of this square.
                        open[position].Parent = current.Position;
                        //Record the F, G, and H costs of the square. 
                        if (position.X == x || position.Y == y) open[position].G = parent.G + 10; //adjacent
                        else open[position].G = parent.G + 14; //diagonal
                        open[position].H = GetHeuristics(position, pointB);
                        //open[position].H = (int)Math.Abs(position.X - x) + (int)Math.Abs(position.Y - y);
                        open[position].F = open[position].G + open[position].H;
                    }
                    //If so, change the parent of the square to the current square, and
                    //recalculate the G and F scores of the square.
                    else
                    {
                        open[position].Parent = current.Position;
                        if (position.X == x || position.Y == y) open[position].G = parent.G + 10; //adjacent
                        else open[position].G = parent.G + 14; //diagonal

                        open[position].F = open[position].G + open[position].H;
                    }
                }
            }
            while (open.Count > 0);

            //No path available
            if (current.Position != pointB) return path; //partial path

            //Backtrack from the end to the beginning
            do
            {
                path.Insert(0, current.Position);
                current = closed[current.Parent];
            }
            while (current.Parent != closed[current.Parent].Parent); //the start parent is itself

            return path;
        }
    }

    #endregion

    #region "Brain" class for libsl bots

    public class AStarBrain
    {
        public int THINK_INTERVAL = 1000;
        private GridClient Client;
        private Timer _ThinkTimer = new System.Timers.Timer();
        private Bitmap _Map = new Bitmap(256, 256);
        private int _PathIndex = -1;
        private List<Vector3> _CurrentPath = new List<Vector3>();

        /// <summary>
        /// Basic "brain" class providing instant access to the features in the AStar library
        /// </summary>
        /// <param name="client"></param>
        public AStarBrain(GridClient client)
        {
            InitializeMap();
            _ThinkTimer.Interval = THINK_INTERVAL;
            _ThinkTimer.Elapsed += new System.Timers.ElapsedEventHandler(_ThinkTimer_Elapsed);
            Client = client;
            Client.Network.OnDisconnected += new NetworkManager.DisconnectedCallback(Network_OnDisconnected);
            Client.Network.OnCurrentSimChanged += new NetworkManager.CurrentSimChangedCallback(Network_OnCurrentSimChanged);
            Client.Self.OnTeleport += new AgentManager.TeleportCallback(Self_OnTeleport);
        }

        /// <summary>
        /// The index of the current node along the active path
        /// </summary>
        public int PathIndex
        {
            get { return _PathIndex; }
        }

        /// <summary>
        /// The list of nodes along the active path
        /// </summary>
        public List<Vector3> CurrentPath
        {
            get
            {
                lock (_CurrentPath)
                {
                    List<Vector3> path = new List<Vector3>();
                    foreach (Vector3 node in _CurrentPath) path.Add(node);
                    return path;
                }
            }
        }

        /// <summary>
        /// The currently-loaded bitmap used to define walkable space
        /// </summary>
        public Bitmap Map
        {
            get { return (Bitmap)_Map.Clone(); }
        }

        /// <summary>
        /// Returns the current node along the path, or null if not moving
        /// </summary>
        public Nullable<Vector3> CurrentNode
        {
            get
            {
                if (!Moving) return null;
                else
                {
                    Vector3 index = new Vector3();
                    index.X = CurrentPath[PathIndex].X;
                    index.Y = CurrentPath[PathIndex].Y;
                    index.Z = 0; //2D
                    return index;
                }
            }
        }

        /// <summary>
        /// Returns the destination of the current path, or null if not moving
        /// </summary>
        public Nullable<Vector3> CurrentTarget
        {
            get
            {
                if (!Moving) return null;
                else return CurrentPath[CurrentPath.Count - 1];
            }
        }

        /// <summary>
        /// Returns true if currently moving along a path, or false if not
        /// </summary>
        public bool Moving
        {
            get
            {
                if (PathIndex > -1 && CurrentPath.Count > 0) return true;
                else return false;
            }
        }

        /// <summary>
        /// Clears the current path and stops moving
        /// </summary>
        public void StopMoving()
        {
            _CurrentPath.Clear();
            _PathIndex = -1;
        }

        /// <summary>
        /// Sets a new path to the specified target
        /// </summary>
        /// <param name="target"></param>
        public bool MoveTo(Vector3 target)
        {
            _PathIndex = 0;
            target.Z = 0; //2D
            _CurrentPath = Pathfinding.AStarPath(Map, Client.Self.SimPosition, target);
            if (CurrentPath.Count == 0) return false;
            _ThinkTimer.Start();
            return true;
        }

        /// <summary>
        /// Moves to a random walkable target
        /// </summary>
        public bool MoveToRandom()
        {
            Vector3 target = Pathfinding.GetRandomPoint(Map, Client.Self.SimPosition);
            target.X = 0;
            return MoveTo(target);
        }

        /// <summary>
        /// Initializes the map with the default base color
        /// </summary>
        private void InitializeMap()
        {
            Graphics g = Graphics.FromImage(_Map);
            g.FillRectangle(Brushes.Black, 0f, 0f, 256f, 256f);
            g.DrawImage(_Map, new Point(0, 0));
            g.Dispose();
        }

        /// <summary>
        /// Loads the specified map file
        /// </summary>
        /// <param name="mapFile"></param>
        /// <returns>true if the map loaded successfully, false if not</returns>
        public bool LoadMap(string mapFile)
        {
            Bitmap bmp;
            try
            {
                bmp = new Bitmap(mapFile);
            }
            catch
            {
                return false;
            }
            if (bmp.Width != 256 || bmp.Height != 256)
            {
                return false;
            }
            else
            {
                _Map = bmp;
                return true;
            }
        }

        /// <summary>
        /// Unloads the current map file
        /// </summary>
        public void UnloadMap()
        {
            _Map.Dispose();
            _Map = null;
        }

        private void NextMove()
        {
            float precision = 10.0f;
            if (Moving)
            {
                Vector3 node = CurrentPath[PathIndex];
                node.Z = Client.Self.SimPosition.Z; //2D
                float dist = Vector3.Distance(Client.Self.SimPosition, node);
                if (dist <= precision)
                {
                    _PathIndex++;
                    if (PathIndex == CurrentPath.Count)
                    {
                        Console.WriteLine("Arrived at target.");
                        _PathIndex = -1;
                        Client.Self.AutoPilotCancel();
                        return;
                    }
                }
                Console.WriteLine("Moving to node #{0} ({1}m)...", PathIndex, dist);
                Client.Self.Movement.TurnToward(node);
                Client.Self.AutoPilotLocal((int)node.X, (int)node.Y, node.Z);
            }
        }

        void _ThinkTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            NextMove();
        }

        void Network_OnDisconnected(NetworkManager.DisconnectType reason, string message)
        {
            StopMoving();
        }

        void Network_OnCurrentSimChanged(Simulator PreviousSimulator)
        {
            _PathIndex = -1;
            CurrentPath.Clear();
            _ThinkTimer.Stop();
        }

        /*void Self_TeleportProgress(object sender, TeleportEventArgs e)
        {
            if (Moving) StopMoving();
        }*/

        void Self_OnTeleport(string message, TeleportStatus status, TeleportFlags flags)
        {
            if (Moving) StopMoving();
        }


    }

    #endregion

}

