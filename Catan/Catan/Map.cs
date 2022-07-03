using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
//using UnityEngine;


namespace Catan
{
    public class Map
    {
        private List<Node> nodes = new List<Node>();
        private List<Hex> hexes = new List<Hex>();
        private List<Road> placedRoads = new List<Road>();
        private static List<Resources> resourceTilesBase = new List<Resources>() { Resources.Desert, Resources.Clay, Resources.Clay, Resources.Clay, Resources.Wood, Resources.Wood, Resources.Wood, Resources.Wood, Resources.Stone, Resources.Stone, Resources.Stone, Resources.Sheep, Resources.Sheep, Resources.Sheep, Resources.Sheep, Resources.Wheat, Resources.Wheat, Resources.Wheat, Resources.Wheat };
        private static List<Resources> resourceTilesEx = new List<Resources>() { Resources.Desert, Resources.Desert, Resources.Clay, Resources.Clay, Resources.Clay, Resources.Clay, Resources.Clay, Resources.Wood, Resources.Wood, Resources.Wood, Resources.Wood, Resources.Wood, Resources.Wood, Resources.Stone, Resources.Stone, Resources.Stone, Resources.Stone, Resources.Stone, Resources.Sheep, Resources.Sheep, Resources.Sheep, Resources.Sheep, Resources.Sheep, Resources.Sheep, Resources.Wheat, Resources.Wheat, Resources.Wheat, Resources.Wheat, Resources.Wheat, Resources.Wheat };

        //private Dictionary<int, List<Hex>> hexNeighborsOfHexes = new Dictionary<int, List<Hex>>();  //key: id of hex | value: hex neighbors of hex of that id
        //private Dictionary<int, List<Node>> nodeNeighborsOfHexes = new Dictionary<int, List<Node>>(); //key: id of hex | value: node neighbors of hex of that id
        //private Dictionary<int, List<Hex>> hexNeighborsOfNodes = new Dictionary<int, List<Hex>>(); //key: id of node | value : hex neighbors of node of that id
        //private Dictionary<int, List<Node>> nodeNeighborsOfNodes = new Dictionary<int, List<Node>>(); //key: id of node | value : node neighbors of node of that id

        public Hex getHexById(int id)
        {
            return hexes.FirstOrDefault(hex => hex.Id == id);
        }

        public Node getNodeById(int id)
        {
            return nodes.FirstOrDefault(node => node.Id == id);
        }

        public List<Node> getNodeListByIdList(List<int> ids)
        {
            return nodes.Where(node => ids.Contains(node.Id)).ToList();
        }

        // CONSTRUCTOR
        public Map(int gameType, int seed)
        {
            initHexes(gameType); // CREATES and ADDS to list HEXES
            initNodes(gameType); //nodes.RemoveAt(0);

            addHexNeighborsToHexes(gameType);  //connect hexes to other hexes
            addNodeNeighborsToHexes(gameType); //connect hexes to nodes
            generateHexDetails(gameType, seed);      //generate resources and dice numbers of each hex
            //foreach (Hex hex in hexes) { Console.WriteLine(hex.ToString()); }

            addHexNeighborsToNodes(gameType);  //connect nodes to hexes
            addNodeNeighborsToNodes(gameType); //connect nodes to nodes
            addPortsToNodes(gameType);         //add ports
            //foreach (Node node in nodes) { Console.WriteLine(node.ToString()); }

            //frontBuild(gameType);

        }


        private void initNodes(int gameType)
        {
            //loop 1..54/80 generare cu constructorul doar cu id, restul se intampla in connect
            int nrNodes;
            if (gameType == 1) nrNodes = 54;
            else nrNodes = 80;

            nodes.Add(new Node(-1000));
            for (int i = 1; i <= nrNodes; i++)
            {
                Node item = new Node(i);
                nodes.Add(item);
            }
        }
        private void initHexes(int gameType)
        {
            int normalHex, negativeHex;
            if (gameType == 1) { normalHex = 19; negativeHex = 18; }
            else { normalHex = 30; negativeHex = 22; }

            hexes.Add(new Hex(-1000));
            for (int i = 1; i <= normalHex; i++)
            {
                Hex hex = new Hex(i);
                hexes.Add(hex);
            }
            for (int i = 1; i <= negativeHex; i++)
            {
                Hex hex = new Hex(0 - i);
                hexes.Add(hex);

            }

            if (gameType == 1)
            {
                int hexId = 1, hexesOnRow = 0;
                for (int i = 2; i <= 6; i++)
                {
                    if (i == 2 || i == 6) hexesOnRow = 3;
                    else if (i == 3 || i == 5) hexesOnRow = 4;
                    else if (i == 4) hexesOnRow = 5;

                    for (int j = 2; j < 2 + hexesOnRow; j++)
                    {
                        hexes[hexId].Row = i;
                        hexes[hexId].PosOnRow = j;
                        hexId++;
                    }
                }

                int hexId1 = -4, hexId2 = -1; hexesOnRow = 0;
                for (int i = 1; i <= 7; i++)
                {
                    if (i == 1 || i == 7) hexesOnRow = 4;
                    else if (i == 2 || i == 6) hexesOnRow = 5;
                    else if (i == 3 || i == 5) hexesOnRow = 6;
                    else if (i == 4) hexesOnRow = 7;

                    hexes[Math.Abs(hexId1) + 19].Row = i;
                    hexes[Math.Abs(hexId1) + 19].PosOnRow = hexesOnRow;

                    hexes[Math.Abs(hexId2) + 19].Row = i;
                    hexes[Math.Abs(hexId2) + 19].PosOnRow = 1;

                    hexId1--;
                    hexId2 = (hexId2 == -1) ? -18 : (Math.Abs(hexId2) - 1);
                }

                hexes[21].Row = 1; hexes[21].PosOnRow = 2;
                hexes[22].Row = 1; hexes[22].PosOnRow = 3;
                hexes[31].Row = 7; hexes[31].PosOnRow = 2;
                hexes[30].Row = 7; hexes[30].PosOnRow = 3;
            }

            else
            {
                int hexId = 1, hexesOnRow = 0;
                for (int i = 2; i <= 8; i++)
                {
                    if (i == 2 || i == 8) hexesOnRow = 3;
                    else if (i == 3 || i == 7) hexesOnRow = 4;
                    else if (i == 4 || i == 6) hexesOnRow = 5;
                    else if (i == 5) hexesOnRow = 6;

                    for (int j = 2; j < 2 + hexesOnRow; j++)
                    {
                        hexes[hexId].Row = i;
                        hexes[hexId].PosOnRow = j;
                        hexId++;
                    }
                }

                int hexId1 = -4, hexId2 = -1; hexesOnRow = 0;
                for (int i = 1; i <= 9; i++)
                {
                    if (i == 1 || i == 9) hexesOnRow = 4;
                    else if (i == 2 || i == 8) hexesOnRow = 5;
                    else if (i == 3 || i == 7) hexesOnRow = 6;
                    else if (i == 4 || i == 6) hexesOnRow = 7;
                    else if (i == 5) hexesOnRow = 8;

                    hexes[Math.Abs(hexId1) + 30].Row = i;
                    hexes[Math.Abs(hexId1) + 30].PosOnRow = hexesOnRow;

                    hexes[Math.Abs(hexId2) + 30].Row = i;
                    hexes[Math.Abs(hexId2) + 30].PosOnRow = 1;

                    hexId1--;
                    hexId2 = (hexId2 == -1) ? -22 : (Math.Abs(hexId2) - 1);
                }

                hexes[32].Row = 1; hexes[32].PosOnRow = 2;
                hexes[33].Row = 1; hexes[33].PosOnRow = 3;
                hexes[44].Row = 9; hexes[44].PosOnRow = 2;
                hexes[43].Row = 9; hexes[43].PosOnRow = 3;
            }
        }

        private void generateHexDetails(int gameType, int seed)
        {
            //List<Resources> resourceTiles = new List<Resources>() {Resources.Desert, Resources.Clay, Resources.Clay, Resources.Clay, Resources.Wood, Resources.Wood, Resources.Wood, Resources.Wood, Resources.Stone, Resources.Stone, Resources.Stone, Resources.Sheep, Resources.Sheep, Resources.Sheep, Resources.Sheep, Resources.Wheat, Resources.Wheat, Resources.Wheat, Resources.Wheat};
            List<int> numberPieces;
            int normalHexes, allHexes;
            List<int> toPlace;
            bool robberPlaced = false;
            List<Resources> shuffledTiles;

            Random rng = new Random(seed);

            if (gameType == 1)
            {
                numberPieces = new List<int>() { 2, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12 };
                normalHexes = 19; allHexes = 37;
                toPlace = new List<int>() { 6, 6, 8, 8 };
                shuffledTiles = TurnMethods.Shuffle(resourceTilesBase, seed);
                shuffledTiles = TurnMethods.Shuffle(shuffledTiles, seed);

            }
            else
            {
                numberPieces = new List<int>() { 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 8, 8, 8, 9, 9, 9, 10, 10, 10, 11, 11, 12, 12, 12 };
                normalHexes = 30; allHexes = 52;
                toPlace = new List<int>() { 6, 6, 6, 8, 8, 8 };
                shuffledTiles = TurnMethods.Shuffle(resourceTilesEx, seed);
                shuffledTiles = TurnMethods.Shuffle(shuffledTiles, seed);
            }

            //oceans
            for (int i = normalHexes + 1; i <= allHexes; i++)
            {
                hexes[i].setDetails(Resources.Ocean, -1);
            }

            foreach (Resources tile in shuffledTiles)
            {
                //Console.WriteLine(tile.ToString());
            }
            List<int> shuffledNumberPiecesNo68 = TurnMethods.Shuffle(numberPieces, seed).FindAll(e => e != 6 && e != 8);
            for (int i = 0; i < shuffledNumberPiecesNo68.Count; i++)
            {
                //Console.Write(shuffledNumberPiecesNo68[i].ToString() + " ");
            }
            //Console.WriteLine();

            //tiles that are not deserts will have numbers on them
            List<Hex> possibilities = new List<Hex>();

            //randomize the resource tiles on the map
            for (int i = 0; i < normalHexes; i++)
            {
                hexes[i + 1].Resource = shuffledTiles[i];
                hexes[i + 1].Number = 0;

                if (hexes[i + 1].Resource != Resources.Desert)
                {
                    //tiles that are not deserts will have numbers on them
                    possibilities.Add(hexes[i + 1]);
                }
                else
                {
                    hexes[i + 1].Number = 7;
                    if (robberPlaced == false)
                    {
                        hexes[i + 1].HasRobber = true;
                        robberPlaced = true;
                    }
                }
            }

            for (int i = 0; i < toPlace.Count; i++)
            {
                Hex pos = possibilities[rng.Next(possibilities.Count)];
                pos.Number = toPlace[i];
                //Console.WriteLine(pos.ToString());
                //possibilities = possibilities.FindAll(hex => !(hex.HexNeighbors.Contains(hex)) || !(hex == pos)  !(hex.Resource == Resources.Desert));
                possibilities = possibilities.Where(hex => !(hex.HexNeighborIds.Contains(pos.Id)))
                                             .Where(hex => hex != pos)
                                             .ToList();
            }

            for (int i = 0; i < normalHexes; i++)
            {
                if (hexes[i + 1].Resource != Resources.Desert && hexes[i + 1].Number == 0)
                {
                    hexes[i + 1].Number = shuffledNumberPiecesNo68[0];
                    shuffledNumberPiecesNo68.RemoveAt(0);
                }
            }

        }
        private void addPortsToNodes(int gameType)
        {
            if (gameType == 1)
            {
                int[] permutation = { 3, 4, 6, 7, 16, 26, 37, 47, 53, 54, 50, 51, 39, 40, 28, 17, 8, 9 };
                for (int i = 0; i < permutation.Count(); i++)
                {
                    nodes[permutation[i]].HasPort = true;
                    nodes[permutation[i]].PortType = Ports.Simple;
                }
                nodes[16].PortType = nodes[26].PortType = Ports.Clay;
                nodes[37].PortType = nodes[47].PortType = Ports.Wood;
                nodes[50].PortType = nodes[51].PortType = Ports.Wheat;
                nodes[39].PortType = nodes[40].PortType = Ports.Stone;
                nodes[8].PortType = nodes[9].PortType = Ports.Sheep;
            }
            else
            {
                int[] permutation = { 3, 4, 6, 7, 16, 26, 39, 40, 52, 64, 79, 80, 76, 77, 65, 66, 41, 28, 17, 18, 9, 1 };
                for (int i = 0; i < permutation.Count(); i++)
                {
                    nodes[permutation[i]].HasPort = true;
                    nodes[permutation[i]].PortType = Ports.Simple;
                }
                nodes[3].PortType = nodes[4].PortType = Ports.Wood;
                nodes[16].PortType = nodes[26].PortType = Ports.Wheat;
                nodes[39].PortType = nodes[40].PortType = Ports.Sheep;
                nodes[52].PortType = nodes[64].PortType = Ports.Stone;
                nodes[76].PortType = nodes[77].PortType = Ports.Sheep;
                nodes[17].PortType = nodes[18].PortType = Ports.Clay;
            }

        }

        /*void frontBuild(int gameType)
        {
            GameObject MapHandler = GameObject.Find("MapBuilder");
            MapHandler.GetComponent<MapGen>().gen(gameType, this);
        }*/

        private void addHexNeighborsToHexes(int gameType)
        {
            if (gameType == 1)
            {
                getHexById(-1000).HexNeighborIds = new List<int>(); //ghost hex
                hexes[1].HexNeighborIds = new List<int>() { -1, -2, -18, 2, 5, 4 };
                hexes[2].HexNeighborIds = new List<int>() { -2, -3, 1, 5, 6, 3 };
                hexes[3].HexNeighborIds = new List<int>() { -3, -4, -5, 6, 7, 2 };
                hexes[4].HexNeighborIds = new List<int>() { -17, -18, 5, 8, 1, 9 };
                hexes[5].HexNeighborIds = new List<int>() { 1, 2, 6, 10, 9, 4 };
                hexes[6].HexNeighborIds = new List<int>() { 2, 3, 5, 7, 10, 11 };
                hexes[7].HexNeighborIds = new List<int>() { 3, 6, 11, 12, -5, -6 };
                hexes[8].HexNeighborIds = new List<int>() { -15, -16, -17, 4, 9, 13 };
                hexes[9].HexNeighborIds = new List<int>() { 4, 5, 8, 10, 13, 14 };
                hexes[10].HexNeighborIds = new List<int>() { 5, 6, 11, 9, 14, 15 };
                hexes[11].HexNeighborIds = new List<int>() { 6, 7, 12, 16, 15, 10 };
                hexes[12].HexNeighborIds = new List<int>() { -6, -7, -8, 7, 11, 16 };
                hexes[13].HexNeighborIds = new List<int>() { -14, -15, 8, 9, 14, 17 };
                hexes[14].HexNeighborIds = new List<int>() { 9, 10, 15, 18, 17, 13 };
                hexes[15].HexNeighborIds = new List<int>() { 10, 11, 16, 19, 18, 14 };
                hexes[16].HexNeighborIds = new List<int>() { -8, -9, 11, 12, 15, 19 };
                hexes[17].HexNeighborIds = new List<int>() { -13, -14, -12, 13, 14, 18 };
                hexes[18].HexNeighborIds = new List<int>() { -11, -12, 17, 14, 15, 19 };
                hexes[19].HexNeighborIds = new List<int>() { -9, -10, -11, 15, 16, 18 };
            }
            else
            {
                int x = 30;
                getHexById(-1000).HexNeighborIds = new List<int>(); //ghost hex
                hexes[1].HexNeighborIds = new List<int>() { -1, -2, 2, 5, 4, -22 };
                hexes[2].HexNeighborIds = new List<int>() { -2, -3, 3, 6, 5, 1 };
                hexes[3].HexNeighborIds = new List<int>() { -3, -4, -5, 7, 6, 2 };
                hexes[4].HexNeighborIds = new List<int>() { -22, 1, 5, 9, 8, -21 };
                hexes[5].HexNeighborIds = new List<int>() { 1, 2, 6, 10, 9, 4 };
                hexes[6].HexNeighborIds = new List<int>() { 5, 2, 3, 7, 11, 10 };
                hexes[7].HexNeighborIds = new List<int>() { 3, x - 5, x - 6, 12, 11, 6 };
                hexes[8].HexNeighborIds = new List<int>() { -20, -21, 4, 9, 14, 13 };
                hexes[9].HexNeighborIds = new List<int>() { 4, 5, 8, 10, 14, 15 };
                hexes[10].HexNeighborIds = new List<int>() { 9, 11, 5, 6, 15, 16 };
                hexes[11].HexNeighborIds = new List<int>() { 6, 7, 10, 12, 16, 17 };
                hexes[12].HexNeighborIds = new List<int>() { 7, -6, -7, 17, 18, 11 };
                hexes[13].HexNeighborIds = new List<int>() { -20, 8, 14, 19, -18, -19 };
                hexes[14].HexNeighborIds = new List<int>() { 8, 9, 13, 15, 19, 20 };
                hexes[15].HexNeighborIds = new List<int>() { 9, 10, 14, 16, 20, 21 };
                hexes[16].HexNeighborIds = new List<int>() { 10, 11, 15, 17, 21, 22 };
                hexes[17].HexNeighborIds = new List<int>() { 16, 18, 11, 12, 22, 23 };
                hexes[18].HexNeighborIds = new List<int>() { 12, 17, 23, -7, -8, -9 };
                hexes[19].HexNeighborIds = new List<int>() { -18, -17, 13, 14, 20, 24 };
                hexes[20].HexNeighborIds = new List<int>() { 19, 21, 14, 15, 24, 25 };
                hexes[21].HexNeighborIds = new List<int>() { 20, 22, 15, 16, 25, 26 };
                hexes[22].HexNeighborIds = new List<int>() { 21, 23, 16, 17, 26, 27 };
                hexes[23].HexNeighborIds = new List<int>() { 17, 18, 22, 27, -9, -10 };
                hexes[24].HexNeighborIds = new List<int>() { -17, -16, 19, 20, 25, 28 };
                hexes[25].HexNeighborIds = new List<int>() { 20, 21, 24, 26, 28, 29 };
                hexes[26].HexNeighborIds = new List<int>() { 21, 22, 29, 30, 25, 27 };
                hexes[27].HexNeighborIds = new List<int>() { 22, 23, 26, 30, -10, -11 };
                hexes[28].HexNeighborIds = new List<int>() { 24, 25, 29, -14, -15, -16 };
                hexes[29].HexNeighborIds = new List<int>() { 28, 25, 26, 30, -14, -13 };
                hexes[30].HexNeighborIds = new List<int>() { 29, 26, 27, -11, -12, -13 };

            }
        }
        private void addNodeNeighborsToHexes(int gameType)
        {
            if (gameType == 1)
            {
                getHexById(-1000).NodeNeighborIds = new List<int>(); //ghost hex
                getHexById(-1).NodeNeighborIds = new List<int>() { 1, 2 };
                getHexById(-2).NodeNeighborIds = new List<int>() { 2, 3, 4 };
                getHexById(-3).NodeNeighborIds = new List<int>() { 4, 5, 6 };
                getHexById(-4).NodeNeighborIds = new List<int>() { 6, 7 };
                getHexById(-5).NodeNeighborIds = new List<int>() { 7, 15, 16 };
                getHexById(-6).NodeNeighborIds = new List<int>() { 16, 26, 27 };
                getHexById(-7).NodeNeighborIds = new List<int>() { 27, 38 };
                getHexById(-8).NodeNeighborIds = new List<int>() { 38, 37, 47 };
                getHexById(-9).NodeNeighborIds = new List<int>() { 47, 46, 54 };
                getHexById(-10).NodeNeighborIds = new List<int>() { 53, 54 };
                getHexById(-11).NodeNeighborIds = new List<int>() { 51, 52, 53 };
                getHexById(-12).NodeNeighborIds = new List<int>() { 51, 50, 49 };
                getHexById(-13).NodeNeighborIds = new List<int>() { 48, 49 };
                getHexById(-14).NodeNeighborIds = new List<int>() { 39, 40, 48 };
                getHexById(-15).NodeNeighborIds = new List<int>() { 28, 29, 39 };
                getHexById(-16).NodeNeighborIds = new List<int>() { 17, 28 };
                getHexById(-17).NodeNeighborIds = new List<int>() { 8, 18, 17 };
                getHexById(-18).NodeNeighborIds = new List<int>() { 1, 9, 8 };
                hexes[1].NodeNeighborIds = new List<int>() { 1, 2, 3, 9, 10, 11 };
                hexes[2].NodeNeighborIds = new List<int>() { 3, 4, 5, 11, 12, 13 };
                hexes[3].NodeNeighborIds = new List<int>() { 5, 6, 7, 13, 14, 15 };
                hexes[4].NodeNeighborIds = new List<int>() { 8, 9, 10, 18, 19, 20 };
                hexes[5].NodeNeighborIds = new List<int>() { 10, 11, 12, 20, 21, 22 };
                hexes[6].NodeNeighborIds = new List<int>() { 12, 13, 14, 22, 23, 24 };
                hexes[7].NodeNeighborIds = new List<int>() { 14, 15, 16, 24, 25, 26 };
                hexes[8].NodeNeighborIds = new List<int>() { 17, 18, 19, 28, 29, 30 };
                hexes[9].NodeNeighborIds = new List<int>() { 19, 20, 21, 30, 31, 32 };
                hexes[10].NodeNeighborIds = new List<int>() { 21, 22, 23, 32, 33, 34 };
                hexes[11].NodeNeighborIds = new List<int>() { 23, 24, 25, 34, 35, 36 };
                hexes[12].NodeNeighborIds = new List<int>() { 25, 26, 27, 36, 37, 38 };
                hexes[13].NodeNeighborIds = new List<int>() { 29, 30, 31, 39, 40, 41 };
                hexes[14].NodeNeighborIds = new List<int>() { 31, 32, 33, 41, 42, 43 };
                hexes[15].NodeNeighborIds = new List<int>() { 33, 34, 35, 43, 44, 45 };
                hexes[16].NodeNeighborIds = new List<int>() { 35, 36, 37, 45, 46, 47 };
                hexes[17].NodeNeighborIds = new List<int>() { 40, 41, 42, 48, 49, 50 };
                hexes[18].NodeNeighborIds = new List<int>() { 42, 43, 44, 50, 51, 52 };
                hexes[19].NodeNeighborIds = new List<int>() { 44, 45, 46, 52, 53, 54 };
            }
            else
            {
                getHexById(-1000).NodeNeighborIds = new List<int>(); //ghost hex
                getHexById(-1).NodeNeighborIds = new List<int>() { 1, 2 };
                getHexById(-2).NodeNeighborIds = new List<int>() { 2, 3, 4 };
                getHexById(-3).NodeNeighborIds = new List<int>() { 4, 5, 6 };
                getHexById(-4).NodeNeighborIds = new List<int>() { 6, 7 };
                getHexById(-5).NodeNeighborIds = new List<int>() { 7, 15, 16 };
                getHexById(-6).NodeNeighborIds = new List<int>() { 16, 26, 27 };
                getHexById(-7).NodeNeighborIds = new List<int>() { 27, 39, 40 };
                getHexById(-8).NodeNeighborIds = new List<int>() { 40, 53 };
                getHexById(-9).NodeNeighborIds = new List<int>() { 53, 52, 64 };
                getHexById(-10).NodeNeighborIds = new List<int>() { 64, 63, 73 };
                getHexById(-11).NodeNeighborIds = new List<int>() { 73, 72, 80 };
                getHexById(-12).NodeNeighborIds = new List<int>() { 79, 80 };
                getHexById(-13).NodeNeighborIds = new List<int>() { 77, 78, 79 };
                getHexById(-14).NodeNeighborIds = new List<int>() { 75, 76, 77 };
                getHexById(-15).NodeNeighborIds = new List<int>() { 74, 75 };
                getHexById(-16).NodeNeighborIds = new List<int>() { 65, 66, 74 };
                getHexById(-17).NodeNeighborIds = new List<int>() { 54, 55, 65 };
                getHexById(-18).NodeNeighborIds = new List<int>() { 41, 42, 54 };
                getHexById(-19).NodeNeighborIds = new List<int>() { 28, 41 };
                getHexById(-20).NodeNeighborIds = new List<int>() { 17, 29, 28 };
                getHexById(-21).NodeNeighborIds = new List<int>() { 8, 18, 17 };
                getHexById(-22).NodeNeighborIds = new List<int>() { 1, 9, 8 };
                hexes[1].NodeNeighborIds = new List<int>() { 1, 2, 3, 9, 10, 11 };
                hexes[2].NodeNeighborIds = new List<int>() { 3, 4, 5, 11, 12, 13 };
                hexes[3].NodeNeighborIds = new List<int>() { 5, 6, 7, 13, 14, 15 };
                hexes[4].NodeNeighborIds = new List<int>() { 8, 9, 10, 18, 19, 20 };
                hexes[5].NodeNeighborIds = new List<int>() { 10, 11, 12, 20, 21, 22 };
                hexes[6].NodeNeighborIds = new List<int>() { 12, 13, 14, 22, 23, 24 };
                hexes[7].NodeNeighborIds = new List<int>() { 14, 15, 16, 24, 25, 26 };
                hexes[8].NodeNeighborIds = new List<int>() { 17, 18, 19, 29, 30, 31 };
                hexes[9].NodeNeighborIds = new List<int>() { 19, 20, 21, 31, 32, 33 };
                hexes[10].NodeNeighborIds = new List<int>() { 21, 22, 23, 33, 34, 35 };
                hexes[11].NodeNeighborIds = new List<int>() { 23, 24, 25, 35, 36, 37 };
                hexes[12].NodeNeighborIds = new List<int>() { 25, 26, 27, 37, 38, 39 };
                hexes[13].NodeNeighborIds = new List<int>() { 28, 29, 30, 41, 42, 43 };
                hexes[14].NodeNeighborIds = new List<int>() { 30, 31, 32, 43, 44, 45 };
                hexes[15].NodeNeighborIds = new List<int>() { 32, 33, 34, 45, 46, 47 };
                hexes[16].NodeNeighborIds = new List<int>() { 34, 35, 36, 47, 48, 49 };
                hexes[17].NodeNeighborIds = new List<int>() { 36, 37, 38, 49, 50, 51 };
                hexes[18].NodeNeighborIds = new List<int>() { 38, 39, 40, 51, 52, 53 };
                hexes[19].NodeNeighborIds = new List<int>() { 42, 43, 44, 54, 55, 56 };
                hexes[20].NodeNeighborIds = new List<int>() { 44, 45, 46, 56, 57, 58 };
                hexes[21].NodeNeighborIds = new List<int>() { 46, 47, 48, 58, 59, 60 };
                hexes[22].NodeNeighborIds = new List<int>() { 48, 49, 50, 60, 61, 62 };
                hexes[23].NodeNeighborIds = new List<int>() { 50, 51, 52, 62, 63, 64 };
                hexes[24].NodeNeighborIds = new List<int>() { 55, 56, 57, 65, 66, 67 };
                hexes[25].NodeNeighborIds = new List<int>() { 57, 58, 59, 67, 68, 69 };
                hexes[26].NodeNeighborIds = new List<int>() { 59, 60, 61, 69, 70, 71 };
                hexes[27].NodeNeighborIds = new List<int>() { 61, 62, 63, 71, 72, 73 };
                hexes[28].NodeNeighborIds = new List<int>() { 66, 67, 68, 74, 75, 76 };
                hexes[29].NodeNeighborIds = new List<int>() { 68, 69, 70, 76, 77, 78 };
                hexes[30].NodeNeighborIds = new List<int>() { 70, 71, 72, 78, 79, 80 };

            }
        }

        private void addHexNeighborsToNodes(int gameType)
        {
            if (gameType == 1)
            {
                int x = 19;
                getNodeById(-1000).HexNeighborIds = new List<int>(); //ghost node
                nodes[1].HexNeighborIds = new List<int>() { -18, -1, 1 };
                nodes[2].HexNeighborIds = new List<int>() { -1, 1, -2 };
                nodes[3].HexNeighborIds = new List<int>() { 1, -2, 2 };
                nodes[4].HexNeighborIds = new List<int>() { -2, 2, -3 };
                nodes[5].HexNeighborIds = new List<int>() { 2, -3, 3 };
                nodes[6].HexNeighborIds = new List<int>() { -3, 3, -4 };
                nodes[7].HexNeighborIds = new List<int>() { 3, -4, -5 };
                nodes[8].HexNeighborIds = new List<int>() { -17, -18, 4 };
                nodes[9].HexNeighborIds = new List<int>() { -18, 4, 1 };
                nodes[10].HexNeighborIds = new List<int>() { 4, 1, 5 };
                nodes[11].HexNeighborIds = new List<int>() { 1, 5, 2 };
                nodes[12].HexNeighborIds = new List<int>() { 5, 2, 6 };
                nodes[13].HexNeighborIds = new List<int>() { 2, 6, 3 };
                nodes[14].HexNeighborIds = new List<int>() { 6, 3, 7 };
                nodes[15].HexNeighborIds = new List<int>() { 3, 7, -5 };
                nodes[16].HexNeighborIds = new List<int>() { 7, -5, -6 };
                nodes[17].HexNeighborIds = new List<int>() { -16, -17, 8 };
                nodes[18].HexNeighborIds = new List<int>() { -17, 8, 4 };
                nodes[19].HexNeighborIds = new List<int>() { 8, 4, 9 };
                nodes[20].HexNeighborIds = new List<int>() { 4, 9, 5 };
                nodes[21].HexNeighborIds = new List<int>() { 9, 5, 10 };
                nodes[22].HexNeighborIds = new List<int>() { 5, 10, 6 };
                nodes[23].HexNeighborIds = new List<int>() { 10, 6, 11 };
                nodes[24].HexNeighborIds = new List<int>() { 6, 11, 7 };
                nodes[25].HexNeighborIds = new List<int>() { 11, 7, 12 };
                nodes[26].HexNeighborIds = new List<int>() { 7, 12, -6 };
                nodes[27].HexNeighborIds = new List<int>() { 12, -6, -7 };
                nodes[28].HexNeighborIds = new List<int>() { -16, -15, 8 };
                nodes[29].HexNeighborIds = new List<int>() { -15, 8, 13 };
                nodes[30].HexNeighborIds = new List<int>() { 8, 13, 9 };
                nodes[31].HexNeighborIds = new List<int>() { 13, 9, 14 };
                nodes[32].HexNeighborIds = new List<int>() { 9, 14, 10 };
                nodes[33].HexNeighborIds = new List<int>() { 14, 10, 15 };
                nodes[34].HexNeighborIds = new List<int>() { 10, 15, 11 };
                nodes[35].HexNeighborIds = new List<int>() { 15, 11, 16 };
                nodes[36].HexNeighborIds = new List<int>() { 11, 16, 12 };
                nodes[37].HexNeighborIds = new List<int>() { 16, 12, -8 };
                nodes[38].HexNeighborIds = new List<int>() { 12, -8, -7 };
                nodes[39].HexNeighborIds = new List<int>() { -15, -14, 13 };
                nodes[40].HexNeighborIds = new List<int>() { -14, 13, 17 };
                nodes[41].HexNeighborIds = new List<int>() { 13, 17, 14 };
                nodes[42].HexNeighborIds = new List<int>() { 17, 14, 18 };
                nodes[43].HexNeighborIds = new List<int>() { 14, 18, 15 };
                nodes[44].HexNeighborIds = new List<int>() { 18, 15, 19 };
                nodes[45].HexNeighborIds = new List<int>() { 15, 19, 16 };
                nodes[46].HexNeighborIds = new List<int>() { 19, 16, -9 };
                nodes[47].HexNeighborIds = new List<int>() { 16, -9, -8 };
                nodes[48].HexNeighborIds = new List<int>() { -14, -13, 17 };
                nodes[49].HexNeighborIds = new List<int>() { -13, 17, -12 };
                nodes[50].HexNeighborIds = new List<int>() { 17, -12, 18 };
                nodes[51].HexNeighborIds = new List<int>() { -12, 18, -11 };
                nodes[52].HexNeighborIds = new List<int>() { 18, -11, 19 };
                nodes[53].HexNeighborIds = new List<int>() { -11, 19, -10 };
                nodes[54].HexNeighborIds = new List<int>() { 19, -10, -9 };
            }
            else
            {
                int x = 30;
                getNodeById(-1000).HexNeighborIds = new List<int>(); //ghost node
                nodes[1].HexNeighborIds = new List<int>() { -1, -22, 1 };
                nodes[2].HexNeighborIds = new List<int>() { -1, -2, 1 };
                nodes[3].HexNeighborIds = new List<int>() { 1, 2, -2 };
                nodes[4].HexNeighborIds = new List<int>() { -2, -3, 2 };
                nodes[5].HexNeighborIds = new List<int>() { 2, 3, -3 };
                nodes[6].HexNeighborIds = new List<int>() { 3, -3, -4 };
                nodes[7].HexNeighborIds = new List<int>() { 3, -4, -5 };
                nodes[8].HexNeighborIds = new List<int>() { -21, -22, 4 };
                nodes[9].HexNeighborIds = new List<int>() { -22, 4, 1 };
                nodes[10].HexNeighborIds = new List<int>() { 1, 4, 5 };
                nodes[11].HexNeighborIds = new List<int>() { 1, 2, 5 };
                nodes[12].HexNeighborIds = new List<int>() { 2, 5, 6 };
                nodes[13].HexNeighborIds = new List<int>() { 2, 6, 3 };
                nodes[14].HexNeighborIds = new List<int>() { 6, 7, 3 };
                nodes[15].HexNeighborIds = new List<int>() { 3, 7, -5 };
                nodes[16].HexNeighborIds = new List<int>() { 7, -5, -6 };
                nodes[17].HexNeighborIds = new List<int>() { -20, -21, 8 };
                nodes[18].HexNeighborIds = new List<int>() { -21, 8, 4 };
                nodes[19].HexNeighborIds = new List<int>() { 8, 4, 9 };
                nodes[20].HexNeighborIds = new List<int>() { 4, 5, 9 };
                nodes[21].HexNeighborIds = new List<int>() { 9, 5, 10 };
                nodes[22].HexNeighborIds = new List<int>() { 5, 6, 10 };
                nodes[23].HexNeighborIds = new List<int>() { 10, 11, 6 };
                nodes[24].HexNeighborIds = new List<int>() { 6, 7, 11 };
                nodes[25].HexNeighborIds = new List<int>() { 11, 7, 12 };
                nodes[26].HexNeighborIds = new List<int>() { 7, 12, -6 };
                nodes[27].HexNeighborIds = new List<int>() { 12, -6, -7 };
                nodes[28].HexNeighborIds = new List<int>() { -19, -20, 13 };
                nodes[29].HexNeighborIds = new List<int>() { -20, 13, 8 };
                nodes[30].HexNeighborIds = new List<int>() { 13, 14, 8 };
                nodes[31].HexNeighborIds = new List<int>() { 8, 9, 14 };
                nodes[32].HexNeighborIds = new List<int>() { 14, 15, 9 };
                nodes[33].HexNeighborIds = new List<int>() { 9, 15, 10 };
                nodes[34].HexNeighborIds = new List<int>() { 15, 10, 16 };
                nodes[35].HexNeighborIds = new List<int>() { 10, 16, 11 };
                nodes[36].HexNeighborIds = new List<int>() { 16, 11, 17 };
                nodes[37].HexNeighborIds = new List<int>() { 11, 17, 12 };
                nodes[38].HexNeighborIds = new List<int>() { 17, 12, 18 };
                nodes[39].HexNeighborIds = new List<int>() { 12, 18, -7 };
                nodes[40].HexNeighborIds = new List<int>() { 18, -7, -8 };
                nodes[41].HexNeighborIds = new List<int>() { -19, -18, 13 };
                nodes[42].HexNeighborIds = new List<int>() { -18, 13, 19 };
                nodes[43].HexNeighborIds = new List<int>() { 13, 19, 14 };
                nodes[44].HexNeighborIds = new List<int>() { 19, 14, 20 };
                nodes[45].HexNeighborIds = new List<int>() { 14, 20, 15 };
                nodes[46].HexNeighborIds = new List<int>() { 20, 15, 21 };
                nodes[47].HexNeighborIds = new List<int>() { 15, 21, 16 };
                nodes[48].HexNeighborIds = new List<int>() { 21, 16, 22 };
                nodes[49].HexNeighborIds = new List<int>() { 16, 22, 17 };
                nodes[50].HexNeighborIds = new List<int>() { 22, 17, 23 };
                nodes[51].HexNeighborIds = new List<int>() { 17, 23, 18 };
                nodes[52].HexNeighborIds = new List<int>() { 23, 18, -9 };
                nodes[53].HexNeighborIds = new List<int>() { 18, -9, -8 };
                nodes[54].HexNeighborIds = new List<int>() { -18, -17, 19 };
                nodes[55].HexNeighborIds = new List<int>() { -17, 19, 24 };
                nodes[56].HexNeighborIds = new List<int>() { 19, 24, 20 };
                nodes[57].HexNeighborIds = new List<int>() { 24, 20, 25 };
                nodes[58].HexNeighborIds = new List<int>() { 20, 25, 21 };
                nodes[59].HexNeighborIds = new List<int>() { 25, 21, 26 };
                nodes[60].HexNeighborIds = new List<int>() { 21, 26, 22 };
                nodes[61].HexNeighborIds = new List<int>() { 26, 22, 27 };
                nodes[62].HexNeighborIds = new List<int>() { 22, 27, 23 };
                nodes[63].HexNeighborIds = new List<int>() { 27, 23, -10 };
                nodes[64].HexNeighborIds = new List<int>() { 23, -10, -9 };
                nodes[65].HexNeighborIds = new List<int>() { -17, -16, 24 };
                nodes[66].HexNeighborIds = new List<int>() { -16, 24, 28 };
                nodes[67].HexNeighborIds = new List<int>() { 24, 28, 25 };
                nodes[68].HexNeighborIds = new List<int>() { 28, 25, 29 };
                nodes[69].HexNeighborIds = new List<int>() { 25, 29, 26 };
                nodes[70].HexNeighborIds = new List<int>() { 29, 26, 30 };
                nodes[71].HexNeighborIds = new List<int>() { 26, 30, 27 };
                nodes[72].HexNeighborIds = new List<int>() { 30, 27, -11 };
                nodes[73].HexNeighborIds = new List<int>() { 27, -11, -10 };
                nodes[74].HexNeighborIds = new List<int>() { -16, -15, 28 };
                nodes[75].HexNeighborIds = new List<int>() { -15, 28, -14 };
                nodes[76].HexNeighborIds = new List<int>() { 28, -14, 29 };
                nodes[77].HexNeighborIds = new List<int>() { -14, 29, -13 };
                nodes[78].HexNeighborIds = new List<int>() { 29, -13, 30 };
                nodes[79].HexNeighborIds = new List<int>() { -13, 30, -12 };
                nodes[80].HexNeighborIds = new List<int>() { 30, -12, -11 };

            }
        }

        private void addNodeNeighborsToNodes(int gameType)
        {
            if (gameType == 1)
            {
                getNodeById(-1000).NodeNeighborIds = new List<int>(); //ghost node
                nodes[1].NodeNeighborIds = new List<int>() { 2, 9 };
                nodes[2].NodeNeighborIds = new List<int>() { 1, 3 };
                nodes[3].NodeNeighborIds = new List<int>() { 2, 4, 11 };
                nodes[4].NodeNeighborIds = new List<int>() { 5, 3 };
                nodes[5].NodeNeighborIds = new List<int>() { 6, 13, 4 };
                nodes[6].NodeNeighborIds = new List<int>() { 5, 7 };
                nodes[7].NodeNeighborIds = new List<int>() { 6, 15 };
                nodes[8].NodeNeighborIds = new List<int>() { 9, 18 };
                nodes[9].NodeNeighborIds = new List<int>() { 1, 10, 8 };
                nodes[10].NodeNeighborIds = new List<int>() { 9, 11, 20 };
                nodes[11].NodeNeighborIds = new List<int>() { 3, 12, 10 };
                nodes[12].NodeNeighborIds = new List<int>() { 13, 22, 11 };
                nodes[13].NodeNeighborIds = new List<int>() { 5, 12, 14 };
                nodes[14].NodeNeighborIds = new List<int>() { 15, 13, 24 };
                nodes[15].NodeNeighborIds = new List<int>() { 7, 16, 14 };
                nodes[16].NodeNeighborIds = new List<int>() { 15, 26 };
                nodes[17].NodeNeighborIds = new List<int>() { 18, 28 };
                nodes[18].NodeNeighborIds = new List<int>() { 8, 17, 19 };
                nodes[19].NodeNeighborIds = new List<int>() { 18, 20, 30 };
                nodes[20].NodeNeighborIds = new List<int>() { 10, 21, 19 };
                nodes[21].NodeNeighborIds = new List<int>() { 20, 22, 32 };
                nodes[22].NodeNeighborIds = new List<int>() { 12, 23, 21 };
                nodes[23].NodeNeighborIds = new List<int>() { 22, 24, 34 };
                nodes[24].NodeNeighborIds = new List<int>() { 14, 25, 23 };
                nodes[25].NodeNeighborIds = new List<int>() { 24, 26, 36 };
                nodes[26].NodeNeighborIds = new List<int>() { 16, 27, 25 };
                nodes[27].NodeNeighborIds = new List<int>() { 26, 38 };
                nodes[28].NodeNeighborIds = new List<int>() { 17, 29 };
                nodes[29].NodeNeighborIds = new List<int>() { 28, 39, 30 };
                nodes[30].NodeNeighborIds = new List<int>() { 19, 31, 29 };
                nodes[31].NodeNeighborIds = new List<int>() { 32, 30, 41 };
                nodes[32].NodeNeighborIds = new List<int>() { 21, 31, 33 };
                nodes[33].NodeNeighborIds = new List<int>() { 32, 34, 43 };
                nodes[34].NodeNeighborIds = new List<int>() { 35, 23, 33 };
                nodes[35].NodeNeighborIds = new List<int>() { 36, 34, 45 };
                nodes[36].NodeNeighborIds = new List<int>() { 37, 35, 25 };
                nodes[37].NodeNeighborIds = new List<int>() { 38, 36, 47 };
                nodes[38].NodeNeighborIds = new List<int>() { 27, 37 };
                nodes[39].NodeNeighborIds = new List<int>() { 29, 40 };
                nodes[40].NodeNeighborIds = new List<int>() { 39, 41, 48 };
                nodes[41].NodeNeighborIds = new List<int>() { 31, 42, 40 };
                nodes[42].NodeNeighborIds = new List<int>() { 41, 43, 50 };
                nodes[43].NodeNeighborIds = new List<int>() { 42, 44, 33 };
                nodes[44].NodeNeighborIds = new List<int>() { 43, 45, 52 };
                nodes[45].NodeNeighborIds = new List<int>() { 44, 46, 35 };
                nodes[46].NodeNeighborIds = new List<int>() { 45, 47, 54 };
                nodes[47].NodeNeighborIds = new List<int>() { 46, 37 };
                nodes[48].NodeNeighborIds = new List<int>() { 40, 49 };
                nodes[49].NodeNeighborIds = new List<int>() { 48, 50 };
                nodes[50].NodeNeighborIds = new List<int>() { 49, 51, 42 };
                nodes[51].NodeNeighborIds = new List<int>() { 50, 52 };
                nodes[52].NodeNeighborIds = new List<int>() { 51, 53, 44 };
                nodes[53].NodeNeighborIds = new List<int>() { 52, 54 };
                nodes[54].NodeNeighborIds = new List<int>() { 46, 53 };
            }
            else
            {
                getNodeById(-1000).NodeNeighborIds = new List<int>(); //ghost node
                nodes[1].NodeNeighborIds = new List<int>() { 2, 9 };
                nodes[2].NodeNeighborIds = new List<int>() { 1, 3 };
                nodes[3].NodeNeighborIds = new List<int>() { 2, 4, 11 };
                nodes[4].NodeNeighborIds = new List<int>() { 5, 3 };
                nodes[5].NodeNeighborIds = new List<int>() { 6, 13, 4 };
                nodes[6].NodeNeighborIds = new List<int>() { 5, 7 };
                nodes[7].NodeNeighborIds = new List<int>() { 6, 15 };
                nodes[8].NodeNeighborIds = new List<int>() { 9, 18 };
                nodes[9].NodeNeighborIds = new List<int>() { 1, 10, 8 };
                nodes[10].NodeNeighborIds = new List<int>() { 9, 11, 20 };
                nodes[11].NodeNeighborIds = new List<int>() { 3, 12, 10 };
                nodes[12].NodeNeighborIds = new List<int>() { 13, 22, 11 };
                nodes[13].NodeNeighborIds = new List<int>() { 5, 12, 14 };
                nodes[14].NodeNeighborIds = new List<int>() { 15, 13, 24 };
                nodes[15].NodeNeighborIds = new List<int>() { 7, 16, 14 };
                nodes[16].NodeNeighborIds = new List<int>() { 15, 26 };
                nodes[17].NodeNeighborIds = new List<int>() { 18, 29 };
                nodes[18].NodeNeighborIds = new List<int>() { 8, 17, 19 };
                nodes[19].NodeNeighborIds = new List<int>() { 18, 20, 31 };
                nodes[20].NodeNeighborIds = new List<int>() { 10, 19, 21 };
                nodes[21].NodeNeighborIds = new List<int>() { 20, 22, 33 };
                nodes[22].NodeNeighborIds = new List<int>() { 21, 23, 12 };
                nodes[23].NodeNeighborIds = new List<int>() { 22, 24, 35 };
                nodes[24].NodeNeighborIds = new List<int>() { 23, 25, 14 };
                nodes[25].NodeNeighborIds = new List<int>() { 24, 26, 37 };
                nodes[26].NodeNeighborIds = new List<int>() { 25, 27, 16 };
                nodes[27].NodeNeighborIds = new List<int>() { 26, 39 };
                nodes[28].NodeNeighborIds = new List<int>() { 29, 41 };
                nodes[29].NodeNeighborIds = new List<int>() { 28, 30, 17 };
                nodes[30].NodeNeighborIds = new List<int>() { 29, 31, 43 };
                nodes[31].NodeNeighborIds = new List<int>() { 30, 32, 19 };
                nodes[32].NodeNeighborIds = new List<int>() { 31, 33, 45 };
                nodes[33].NodeNeighborIds = new List<int>() { 32, 34, 21 };
                nodes[34].NodeNeighborIds = new List<int>() { 33, 35, 47 };
                nodes[35].NodeNeighborIds = new List<int>() { 34, 36, 23 };
                nodes[36].NodeNeighborIds = new List<int>() { 35, 37, 49 };
                nodes[37].NodeNeighborIds = new List<int>() { 36, 38, 25 };
                nodes[38].NodeNeighborIds = new List<int>() { 37, 39, 51 };
                nodes[39].NodeNeighborIds = new List<int>() { 38, 40, 27 };
                nodes[40].NodeNeighborIds = new List<int>() { 39, 53 };
                nodes[41].NodeNeighborIds = new List<int>() { 28, 42 };
                nodes[42].NodeNeighborIds = new List<int>() { 41, 43, 54 };
                nodes[43].NodeNeighborIds = new List<int>() { 42, 44, 30 };
                nodes[44].NodeNeighborIds = new List<int>() { 43, 45, 56 };
                nodes[45].NodeNeighborIds = new List<int>() { 44, 46, 32 };
                nodes[46].NodeNeighborIds = new List<int>() { 45, 47, 58 };
                nodes[47].NodeNeighborIds = new List<int>() { 46, 48, 34 };
                nodes[48].NodeNeighborIds = new List<int>() { 47, 49, 60 };
                nodes[49].NodeNeighborIds = new List<int>() { 48, 50, 36 };
                nodes[50].NodeNeighborIds = new List<int>() { 49, 51, 62 };
                nodes[51].NodeNeighborIds = new List<int>() { 50, 52, 38 };
                nodes[52].NodeNeighborIds = new List<int>() { 51, 53, 64 };
                nodes[53].NodeNeighborIds = new List<int>() { 52, 40 };
                nodes[54].NodeNeighborIds = new List<int>() { 55, 42 };
                nodes[55].NodeNeighborIds = new List<int>() { 54, 56, 65 };
                nodes[56].NodeNeighborIds = new List<int>() { 55, 57, 44 };
                nodes[57].NodeNeighborIds = new List<int>() { 56, 58, 67 };
                nodes[58].NodeNeighborIds = new List<int>() { 57, 59, 46 };
                nodes[59].NodeNeighborIds = new List<int>() { 58, 60, 69 };
                nodes[60].NodeNeighborIds = new List<int>() { 59, 61, 48 };
                nodes[61].NodeNeighborIds = new List<int>() { 60, 62, 71 };
                nodes[62].NodeNeighborIds = new List<int>() { 61, 63, 50 };
                nodes[63].NodeNeighborIds = new List<int>() { 62, 64, 73 };
                nodes[64].NodeNeighborIds = new List<int>() { 63, 52 };
                nodes[65].NodeNeighborIds = new List<int>() { 66, 55 };
                nodes[66].NodeNeighborIds = new List<int>() { 65, 67, 74 };
                nodes[67].NodeNeighborIds = new List<int>() { 66, 68, 57 };
                nodes[68].NodeNeighborIds = new List<int>() { 67, 69, 76 };
                nodes[69].NodeNeighborIds = new List<int>() { 68, 70, 59 };
                nodes[70].NodeNeighborIds = new List<int>() { 69, 71, 78 };
                nodes[71].NodeNeighborIds = new List<int>() { 70, 72, 61 };
                nodes[72].NodeNeighborIds = new List<int>() { 71, 73, 80 };
                nodes[73].NodeNeighborIds = new List<int>() { 72, 63 };
                nodes[74].NodeNeighborIds = new List<int>() { 75, 66 };
                nodes[75].NodeNeighborIds = new List<int>() { 74, 76 };
                nodes[76].NodeNeighborIds = new List<int>() { 75, 77, 68 };
                nodes[77].NodeNeighborIds = new List<int>() { 76, 78 };
                nodes[78].NodeNeighborIds = new List<int>() { 77, 79, 70 };
                nodes[79].NodeNeighborIds = new List<int>() { 78, 80 };
                nodes[80].NodeNeighborIds = new List<int>() { 79, 72 };
            }
        }

        public void MoveRobber(Hex hex)
        {
            foreach (Hex ihex in hexes)
            {
                if (ihex.HasRobber == true)
                {
                    ihex.HasRobber = false;
                    break;
                }
            }

            hex.HasRobber = true;
        }

        public override string ToString()
        {
            StringBuilder final = new StringBuilder();

            int hexNormal;
            int gType = 1;//basegame
            if (gType == 1) hexNormal = 19;
            else hexNormal = 30;

            for (int i = 1; i <= hexNormal; i++)
            {
                Console.Write(i + " ");
                final.Append(hexes[i].ToString() + ", neighbors: ");
                foreach (int id in getHexById(i).HexNeighborIds)
                {
                    final.Append(getHexById(id).ToString() + " || ");
                }
                final.Append("\n");
            }
            return final.ToString();
        }

        //populate lists of neighbors in each

        // GETTERS AND SETTERS
        public List<Node> Nodes { get { return this.nodes; } set { this.nodes = value; } }
        public List<Hex> Hexes { get { return this.hexes; } set { this.hexes = value; } }
        public List<Road> PlacedRoads { get { return this.placedRoads; } }

        //public Dictionary<int, List<Hex>> HexNeighborsOfHexes {get {return hexNeighborsOfHexes;} }
        //public Dictionary<int, List<Node>> NodeNeighborsOfHexes {get {return nodeNeighborsOfHexes;} }
        //public Dictionary<int, List<Hex>> HexNeighborsOfNodes {get {return hexNeighborsOfNodes;} }
        //public Dictionary<int, List<Node>> NodeNeighborsOfNodes { get { return nodeNeighborsOfNodes; } }
    }
}