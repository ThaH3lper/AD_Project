using Game1.Scene;
using Microsoft.Xna.Framework;
using Patrik.GameProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Datastructures.Algorithms
{
    public class PathFinder
    {
        private SimulationWorld world;

        public PathFinder(SimulationWorld world)
        {
            this.world = world;
        }

        public List<Point> Pathfind(Point start, Point end)
        {
            // nodes that have already been analyzed and have a path from the start to them
            var closedSet = new List<Point>();
            // nodes that have been identified as a neighbor of an analyzed node, but have 
            // yet to be fully analyzed
            var openSet = new List<Point> { start };
            // a dictionary identifying the optimal origin point to each node. this is used 
            // to back-track from the end to find the optimal path
            var cameFrom = new Dictionary<Point, Point>();
            // a dictionary indicating how far each analyzed node is from the start
            var currentDistance = new Dictionary<Point, int>();
            // a dictionary indicating how far it is expected to reach the end, if the path 
            // travels through the specified node. 
            var predictedDistance = new Dictionary<Point, float>();

            // Estimate the distance
            currentDistance.Add(start, 0);
            predictedDistance.Add(
                start,
                0 + +Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y)
            );

            // if there are any unanalyzed nodes, process them
            while (openSet.Count > 0)
            {
                // get the node with the lowest estimated cost to finish
                var current = (
                    from p in openSet orderby predictedDistance[p] ascending select p
                ).First();

                // if it is the finish, return the path
                if (current.X == end.X && current.Y == end.Y)
                {
                    // generate the found path
                    return ReconstructPath(cameFrom, end);
                }

                // move current node from open to closed
                openSet.Remove(current);
                closedSet.Add(current);

                // process each valid node around the current node
                foreach (var neighbor in GetNeighborNodes(current))
                {
                    var tempCurrentDistance = currentDistance[current] + 1;

                    // if we already know a faster way to this neighbor, use that route and 
                    // ignore this one
                    if (closedSet.Contains(neighbor)
                        && tempCurrentDistance >= currentDistance[neighbor])
                    {
                        continue;
                    }

                    // if we don't know a route to this neighbor, or if this is faster, 
                    // store this route
                    if (!closedSet.Contains(neighbor)
                        || tempCurrentDistance < currentDistance[neighbor])
                    {
                        if (cameFrom.Keys.Contains(neighbor))
                        {
                            cameFrom[neighbor] = current;
                        }
                        else
                        {
                            cameFrom.Add(neighbor, current);
                        }

                        currentDistance[neighbor] = tempCurrentDistance;
                        predictedDistance[neighbor] =
                            currentDistance[neighbor]
                            + Math.Abs(neighbor.X - end.X)
                            + Math.Abs(neighbor.Y - end.Y);

                        // if this is a new node, add it!
                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }

            // unable to figure out a path, abort.
            return new List<Point>();
        }

        /// <summary>
        /// Returns available nodes, blocked nodes will be excluded.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private System.Collections.Generic.IEnumerable<Point> GetNeighborNodes(Point node)
        {
            var nodes = new List<Point>();

            // up
            if (IsPassable(GetTileModulus(node.X, node.Y - 1)))
            {
                nodes.Add(GetModulusPoint(node.X, node.Y - 1));
            }

            // right
            if (IsPassable(GetTileModulus(node.X + 1, node.Y)))
            {
                nodes.Add(GetModulusPoint(node.X + 1, node.Y));
            }

            // down
            if (IsPassable(GetTileModulus(node.X, node.Y + 1)))
            {
                nodes.Add(GetModulusPoint(node.X, node.Y + 1));
            }

            // left
            if (IsPassable(GetTileModulus(node.X - 1, node.Y)))
            {
                nodes.Add(GetModulusPoint(node.X - 1, node.Y));
            }

            return nodes;
        }

        private bool IsPassable(Tile tile)
        {
            if (tile == null)
                return false;

            if (tile.GetTileType() != ETileType.WALL && tile.GetTileType() != ETileType.CRATE)
                return true;
            else
                return false;
        }

        public Tile GetTileModulus(int x, int y)
        {
            if (x < 0) return null;
            if (y < 0) return null;
            if (x >= world.Map.GetWidth()) return null;
            if (y >= world.Map.GetHeight()) return null;
            return world.Map.getTileMap()[x, y];
        }

        public Point GetModulusPoint(int x, int y)
        {
            //if (x < 0) x = 0;
            //if (y < 0) y = 0;
            return new Point(x, y);
        }

        private List<Point> ReconstructPath(Dictionary<Point, Point> cameFrom, Point current)
        {
            if (!cameFrom.Keys.Contains(current))
            {
                return new List<Point> { current };
            }

            var path = ReconstructPath(cameFrom, cameFrom[current]);
            path.Add(current);
            return path;
        }

    }
}
