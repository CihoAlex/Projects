namespace Catan
{
    class PlayerLongestPath
    {
        private readonly int playerId;
        private int longestPath = 2;
        private Graph g;
        public PlayerLongestPath(Player player)  //longest path Constructor ,called at the start of the game for each player
        {
            playerId = player.Id;
            g = new Graph();
        }
        public void AddRoad(Road road)
        {
            if (road.Owner.Id == playerId)
            {

                if (road.Node1.HasSettlement && road.Node1.PlayerSettled.Id != playerId)
                    g.addEdge(road.Node2.Id, road.Node1.Id);                            //if the road gets cut off by another player's settlement
                else                                                                    //make the edge one directional so that the algorithm stops
                {                                                                       //at the opponent's settlement
                    if (road.Node2.HasSettlement && road.Node2.PlayerSettled.Id != playerId)
                        g.addEdge(road.Node1.Id, road.Node2.Id);
                    else
                    {
                        g.addEdge(road.Node1.Id, road.Node2.Id);        //add edges both ways if path is clear
                        g.addEdge(road.Node2.Id, road.Node1.Id);
                    }
                }
            }
        }
        public void ComputeLongestRoad()
        {
            for (int i = 0; i < g.Vertices; i++)
            {
                if (g.AdjList[i].Count != 2)                        //do all paths from i to j ,each i or j node must 1 or 3 neighbours
                    for (int j = 0; j < g.Vertices; j++)
                        if (g.AdjList[i].Count != 2 && i != j)

                            if (longestPath < g.printAllPaths(i, j))
                                longestPath = g.printAllPaths(i, j);

            }

        }
        public int LongestPath { get { return this.longestPath; } }
    }
}