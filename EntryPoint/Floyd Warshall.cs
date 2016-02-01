using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    class Floyd_Warshall
    {
        // list with the road to the building
        public List<Vector2> roadToBuilding = new List<Vector2>();

        public int[,] CreateAdjMatrix(Dictionary<Vector2, int> adjList2, List<Tuple<Vector2, Vector2>> roads)
        {
            var adj = new int[adjList2.Count, adjList2.Count];

            foreach (var road in roads)
            {
                adj[adjList2[road.Item1], adjList2[road.Item2]] = (int)Vector2.Distance(road.Item1, road.Item2);
            }
            for (var u = 0; u < adj.GetLength(0); u++) // Vertices
            {
                for (var v = 0; v < adj.GetLength(0); v++) //Edge
                {
                    if (adj[u, v] == 0 && u != v)
                    {
                        adj[u, v] = 5000000; //Infinity
                    }
                }
            }
            return adj;
        }

        private int[,] MatrixOfPredecessors(int[,] matrix)
        {
            var loopCount = matrix.GetLength(0);
            var next = new int[loopCount, loopCount];

            for (var u = 0; u < loopCount; u++) // Vertices
            {
                for (var v = 0; v < loopCount; v++) //Edge
                {
                    if (matrix[u, v] != 5000000 && u != v)
                    {
                        next[u, v] = u;
                    }
                    else
                    {
                        next[u, v] = -1;
                    }
                }
            }
            return next;
        }


        public int[,] FloydWarshall(int[,] matrix)
        {
            var loopCount = matrix.GetLength(0);
            var next = MatrixOfPredecessors(matrix);

            for (var k = 0; k < loopCount; k++)
            {
                Console.WriteLine(k);
                for (var i = 0; i < loopCount; i++)
                {
                    for (var j = 0; j < loopCount; j++)
                    {
                        if (matrix[i, j] > matrix[i, k] + matrix[k, j])
                        {
                            matrix[i, j] = matrix[i, k] + matrix[k, j];
                            next[i, j] = next[k, j];
                        }
                    }
                }
            }
            return next;
        }


        public List<Vector2> PrintPath(int[,] next, Dictionary<Vector2, int> adjList2, int i, int j)
        {
            if (next[i, j] == -1)
            {
                Console.WriteLine("No Path Exists");
            }
            else
            {
                var pre = next[i, j];
                PrintPath(next, adjList2, i, pre);
                roadToBuilding.Add(adjList2.FirstOrDefault(x => x.Value == j).Key);
                Console.WriteLine(adjList2.FirstOrDefault(x => x.Value == j).Key);
            }

            return roadToBuilding;
        }
    }
}
