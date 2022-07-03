// C# program to print all  
// paths from a source to  
// destination.  
using System;
using System.Collections.Generic;

// A directed graph using  
// adjacency list representation  
// C# program to print all  
// paths from a source to  
// destination.  


// A directed graph using  
// adjacency list representation  
namespace Catan
{
    public class Graph
    {

        // No. of vertices in graph  
        private int vertices;

        // adjacency list  
        public List<int>[] adjList;
        public List<int> intersectList;
        private int maxLength;

        // Constructor  

        public Graph()
        {

            // initialise vertex count  
            this.vertices = 80;

            // initialise adjacency list  
            initAdjList();
        }

        // utility method to initialise  
        // adjacency list  
        public void initAdjList()
        {
            adjList = new List<int>[vertices];

            for (int i = 0; i < vertices; i++)
            {
                adjList[i] = new List<int>();
            }

            //for each player get roads, for each road add x,y and y,x edge

        }

        // add edge from u to v  
        public void addEdge(int u, int v)
        {
            // Add v to u's list.  
            adjList[u].Add(v);
        }


        //stores all nodes with 3 neighbours
        public void fillIntersections(List<int>[] adjList)
        {
            intersectList = new List<int>();
            for (int i = 0; i < vertices; i++)
                if (adjList[i].Count == 3)
                    intersectList.Add(i);

        }




        // Prints all paths from  
        // 's' to 'd'  
        public int printAllPaths(int s, int d)
        {
            fillIntersections(adjList);
            bool[] isTrulyVisited = new bool[vertices];
            bool[] isVisited = new bool[vertices];
            List<int> pathList = new List<int>();


            // add source to path[]  
            pathList.Add(s);

            // Call recursive utility  
            printAllPathsUtil(s, d, isVisited, pathList);

            return maxLength;

        }

        // A recursive function to print  
        // all paths from 'u' to 'd'.  
        // isVisited[] keeps track of  
        // vertices in current path.  
        // localPathList<> stores actual  
        // vertices in the current path 

        private void printAllPathsUtil(int u, int d, bool[] isVisited, List<int> localPathList)
        {

            // Mark the current node  
            isVisited[u] = true;
            if (u.Equals(d))
            {
                //Console.WriteLine(string.Join(" ", localPathList));
                bool andAnotherOne = false;
                foreach (int i in adjList[u])
                {   //magic
                    if (isVisited[i] && intersectList.Contains(i) && localPathList.Contains(i) && localPathList[localPathList.Count - 1] != i)
                    { andAnotherOne = true; localPathList.Add(i); }
                    if (andAnotherOne)
                    {
                        if (localPathList.Count - 1 > maxLength)
                            maxLength = localPathList.Count - 1;
                        localPathList.Remove(i);
                    }
                    else
                        Console.WriteLine(localPathList.Count - 1);

                }
                if (localPathList.Count - 1 > maxLength)
                    maxLength = localPathList.Count - 1;
                // Console.WriteLine(localPathList.Count-1);

                // if match found then no need  
                // to traverse more till depth  
                isVisited[u] = false;
                return;
            }

            // Recur for all the vertices  
            // adjacent to current vertex  
            foreach (int i in adjList[u])
            {

                if (!isVisited[i])
                {
                    // store current node  
                    // in path[]  
                    localPathList.Add(i);

                    printAllPathsUtil(i, d, isVisited, localPathList);

                    // remove current node  
                    // in path[]  
                    localPathList.Remove(i);
                }
            }

            // Mark the current node  
            isVisited[u] = false;
        }

        public int Vertices { get { return this.vertices; } set { this.vertices = value; } }
        public List<int>[] AdjList { get { return this.adjList; } set { this.adjList = value; } }
        public int MaxLength { get { return this.maxLength; } set { this.maxLength = value; } }


    }

}