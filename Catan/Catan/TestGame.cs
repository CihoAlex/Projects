using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Catan
{
    public static class TestGame
    {
        private static List<Resources> baseResourcesToHardCode = new List<Resources>() { Resources.Sheep, Resources.Wood, Resources.Sheep, Resources.Stone, Resources.Wheat, Resources.Wheat, Resources.Wood, Resources.Stone, Resources.Clay, Resources.Desert, Resources.Wood, Resources.Wheat, Resources.Wheat, Resources.Sheep, Resources.Sheep, Resources.Clay, Resources.Wood, Resources.Clay, Resources.Stone };
        private static int[] baseNumbersOnHexes = { 12, 11, 8, 10, 9, 10, 4, 9, 8, 7, 2, 11, 5, 3, 5, 3, 6, 4, 6 };
        public static void HardcodeBaseMap(GameState gs)
        {
            for (int i = 1; i <= 19; i++)
            {
                gs.Map.Hexes[i].Resource = baseResourcesToHardCode[i - 1];
                gs.Map.Hexes[i].Number = baseNumbersOnHexes[i - 1];
                gs.Map.Hexes[i].HasRobber = false;
            }
            gs.Map.Hexes[10].HasRobber = true;
        }

        private static int[] firstRoundBuilding = { 10, 20, 33, 43, 25, 36 }; //p1Settle, p1Road, p2Settle, p2Road, p3Settle, p3Road
        private static int[] secondRoundBuilding = { 35, 45, 50, 51, 19, 30 }; //p3Settle, p3Road, p2Settle, p2Road, p1Settle, p1Road
        public static void HardCodeFirstSettlements3Players(GameState gs, Queue<Player> playerOrder)
        {
            int order = 0;
            foreach (Player currentPlayer in playerOrder)
            {
                Build.BuildSettlement(currentPlayer, gs.Map.Nodes.FirstOrDefault(n => n.Id == firstRoundBuilding[order]), gs.Map, true);
                Build.BuildRoad(currentPlayer, gs.Map.Nodes.FirstOrDefault(n => n.Id == firstRoundBuilding[order]), gs.Map.Nodes.FirstOrDefault(n => n.Id == firstRoundBuilding[order + 1]), gs.Map, true);
                order = order + 2;
            }
            order = 0;
            foreach (Player currentPlayer in playerOrder.Reverse())
            {
                Build.BuildSettlement(currentPlayer, gs.Map.Nodes.FirstOrDefault(n => n.Id == secondRoundBuilding[order]), gs.Map, true);
                Build.BuildRoad(currentPlayer, gs.Map.Nodes.FirstOrDefault(n => n.Id == secondRoundBuilding[order]), gs.Map.Nodes.FirstOrDefault(n => n.Id == secondRoundBuilding[order + 1]), gs.Map, true);
                //foreach (Hex hex in gs.Map.HexNeighborsOfNodes[gs.Map.Nodes.FirstOrDefault(n => n.Id == secondRoundBuilding[order]).Id])
                foreach (int id in gs.Map.Nodes.FirstOrDefault(node => node.Id == secondRoundBuilding[order]).HexNeighborIds)
                {
                    switch (gs.Map.getHexById(id).Resource)
                    {
                        case Resources.Clay:
                            currentPlayer.ClayQty++;
                            break;
                        case Resources.Wheat:
                            currentPlayer.WheatQty++;
                            break;
                        case Resources.Wood:
                            currentPlayer.WoodQty++;
                            break;
                        case Resources.Stone:
                            currentPlayer.StoneQty++;
                            break;
                        case Resources.Sheep:
                            currentPlayer.SheepQty++;
                            break;

                    }
                }
                order = order + 2;
            }
        }
    }
}