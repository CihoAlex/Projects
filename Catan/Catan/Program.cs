using System;
using System.Collections.Generic;
using System.Linq;

namespace Catan
{
    public class Program
    {
        public static void ShowMap(GameState gs)
        {
            String outputInfo = "\n----------------------------------------------\n";
            foreach (Hex hex in gs.Map.Hexes)
            {
                outputInfo += hex.Id + ",Res:" + hex.Resource + ",Nr:" + hex.Number + ";  ";
            }
            outputInfo += "\n-----------------------------------------------";
            Console.WriteLine(outputInfo);
        }
        public static void ShowStats(GameState gs, Queue<Player> playerOrder, int tourCount)
        {
            String outputInfo = "\n----------------------------------------------";
            outputInfo += "\nTour:" + tourCount + "; Points:" + gs.WinningPoints + "; Players:" + gs.NumberOfPlayers + "; GameType:" + gs.GameType + ";";
            outputInfo += "\nCardsInGame:" + gs.CardsLeft + "; Knight:" + gs.KnightCardsLeft + "; Victory:" + gs.VictoryPointCardsLeft + "; Road:" + gs.RoadBuildingCardsLeft + "; Year:" + gs.YearOfPlentyCardsLeft + "; Monopoly:" + gs.MonopolyCardsLeft + ";";
            foreach (Hex hex in gs.Map.Hexes)
            {
                if (hex.HasRobber)
                    outputInfo += "\nRobber:" + hex.Id;
            }
            int order = 1;
            foreach (Player currentPlayer in playerOrder)
            {
                outputInfo += "\nPlayer " + order + "; Name:" + currentPlayer.Name + "(ID:" + currentPlayer.Id + ")" + "; Points:" + currentPlayer.Points + "; Hidden:" + currentPlayer.HiddenPoints + "; LongestRoadPoints:" + currentPlayer.LongestRoadPoints + "; LargestArmyPoints:" + currentPlayer.LargestArmyPoints + ";";
                outputInfo += "\n  WheatQty:" + currentPlayer.WheatQty + "; SheepQty:" + currentPlayer.SheepQty + "; ClayQty:" + currentPlayer.ClayQty + "; StoneQty:" + currentPlayer.StoneQty + "; WoodQty:" + currentPlayer.WoodQty;
                outputInfo += "\n    VillagesLeft:" + currentPlayer.VillagesLeft + "; CitiesLeft:" + currentPlayer.CitiesLeft + "; RoadsLeft:" + currentPlayer.RoadsLeft;
                outputInfo += "\n      KnightCards:" + currentPlayer.unusedKnightCardsLeft + "; VPCards:" + currentPlayer.VictoryPointCardsLeft + "; RoadBCards:" + currentPlayer.unusedRoadBuildingCardsLeft + "; YOPCards:" + currentPlayer.unusedYearOfPlentyCardsLeft + "; MonCards:" + currentPlayer.unusedMonopolyCardsLeft;
                outputInfo += "\n         KCardUsed:" + currentPlayer.KnightCardsUsed + "; HasLR:" + currentPlayer.HasLongestRoad + "; HasLA:" + currentPlayer.HasLargestArmy;
                order++;
            }
            outputInfo += "\n-----------------------------------------------";
            Console.WriteLine(outputInfo);
        }

        public static void Main()
        {
            int gameType = 1, winningPoints = 10, numberOfPlayers = 3;
            List<Player> players = new List<Player>() { new Player("COSTEL"), new Player("MIREL"), new Player("GIGEL") };

            // setez id la jucatori:
            int idp = 1;
            foreach (Player p in players)
                p.Id = idp++;

            Console.WriteLine(players[1].Perm.AllowInitiateTrade);
            players[1].Perm.ChangePermissionCurrentPlayer(4);
            Console.WriteLine(players[1].Perm.AllowInitiateTrade);

            // TESTING
            foreach (Player player in players)
            {
                player.WheatQty = 30;
                player.WoodQty = 0;
                player.SheepQty = 30;
                player.StoneQty = 30;
                player.ClayQty = 0;
            }



            String[] devCardsName = new String[5] { "knight", "point", "road", "year", "monopoly" };
            String winnerName = "False";
            bool isResource = false;

            GameState gs = new GameState(numberOfPlayers, players, gameType, winningPoints, 1241251257);
            Console.WriteLine(gs.Map.getHexById(6).ToString());

            Console.WriteLine(gs.Map.ToString());
            TestGame.HardcodeBaseMap(gs);
            ShowMap(gs); //TESTING

            //randomizam lista de playeri si ii bagam intr-o coada
            players = TurnMethods.Shuffle(players, new Random().Next());

            Queue<Player> playerOrder = new Queue<Player>();

            foreach (Player player in players)
            {
                playerOrder.Enqueue(player);
            }

            //o sa parcurgem de doua ori coada, o data in ordine normala si o data in ordine inversa
            //la fiecare parcurgere jucatorii o sa-si aleaga pozitia settlement-urilor si a drumurilor
            Node node1 = null;

            bool testFirst2Rounds = true;
            if (testFirst2Rounds != true)
            {
                foreach (Player currentPlayer in playerOrder)
                {
                    if (!(Build.has2BuiltRoads(gs.Map.PlacedRoads, currentPlayer)))
                    {
                        bool validSettlement = false;
                        bool validRoad = false;

                        while (!validSettlement || !validRoad)
                        {
                            bool ignoreCost = true;
                            if (!validSettlement)
                            {
                                validSettlement = true;
                                Console.WriteLine("Jucatorul " + currentPlayer.Name + "(" + currentPlayer.Id + ")" + " alege asezarea.");
                                String _input = Console.ReadLine();
                                int intInput = Int32.Parse(_input);
                                node1 = gs.Map.Nodes.FirstOrDefault(n => n.Id == intInput);

                                if (!Build.BuildSettlement(currentPlayer, node1, gs.Map, ignoreCost))
                                {
                                    validSettlement = false;
                                    validRoad = true;
                                }
                                else
                                {
                                    validRoad = false;
                                }
                            }

                            if (!validRoad)
                            {
                                validRoad = true;

                                Console.WriteLine("Jucatorul " + currentPlayer.Name + "(" + currentPlayer.Id + ")" + " introduce nodul care sa formeze drum cu asezarea " + node1.Id);

                                String inputNod2 = Console.ReadLine();
                                int intInput2 = Int32.Parse(inputNod2);
                                Node node2 = gs.Map.Nodes.FirstOrDefault(n => n.Id == intInput2);


                                if (!Build.BuildRoad(currentPlayer, node1, node2, gs.Map, ignoreCost))
                                {
                                    validRoad = false;
                                }
                            }
                        }
                    }
                }

                foreach (Player currentPlayer in playerOrder.Reverse())
                {

                    if (!(Build.has2BuiltRoads(gs.Map.PlacedRoads, currentPlayer)))
                    {
                        bool validSettlement = false;
                        bool validRoad = false;

                        while (!validSettlement || !validRoad)
                        {
                            bool ignoreCost = true;

                            if (!validSettlement)
                            {
                                validSettlement = true;

                                Console.WriteLine("Jucatorul " + currentPlayer.Name + "(" + currentPlayer.Id + ")" + " alege asezarea.");
                                String _input = Console.ReadLine();
                                int intInput = Int32.Parse(_input);
                                node1 = gs.Map.Nodes.FirstOrDefault(n => n.Id == intInput);

                                if (!Build.BuildSettlement(currentPlayer, node1, gs.Map, ignoreCost))
                                {
                                    validSettlement = false;
                                    validRoad = true;
                                }
                                else
                                {
                                    validRoad = false;
                                }
                            }

                            foreach (int id in node1.HexNeighborIds)
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

                            if (!validRoad)
                            {
                                validRoad = true;

                                Console.WriteLine("Jucatorul " + currentPlayer.Name + "(" + currentPlayer.Id + ")" + " introduce nodul care sa formeze drum cu asezarea " + node1.Id);

                                String inputNod2 = Console.ReadLine();
                                int intInput2 = Int32.Parse(inputNod2);
                                Node node2 = gs.Map.Nodes.FirstOrDefault(n => n.Id == intInput2);


                                if (!Build.BuildRoad(currentPlayer, node1, node2, gs.Map, ignoreCost))
                                {
                                    validRoad = false;
                                }
                            }
                        }
                    }
                }
            }
            else TestGame.HardCodeFirstSettlements3Players(gs, playerOrder);
            // Console.WriteLine(Build.longestRoad(gs.Map.PlacedRoads, gs, gs.Map));

            //de aici incepe jocul efectiv
            int input = 0;
            int tourCount = 0;
            ShowStats(gs, playerOrder, tourCount); //TESTING

            while (winnerName == "False")
            {
                ++tourCount;
                foreach (Player currentPlayer in playerOrder)
                {
                    Console.WriteLine("ESTE TURA " + tourCount);
                    Console.WriteLine("ESTE TURA JUCATORULUI " + currentPlayer.Name + "(" + currentPlayer.Id + ")");
                    currentPlayer.PrintResources();

                    currentPlayer.KnightCardsLeft += currentPlayer.unusedKnightCardsLeft;
                    currentPlayer.MonopolyCardsLeft += currentPlayer.unusedMonopolyCardsLeft;
                    currentPlayer.YearOfPlentyCardsLeft += currentPlayer.unusedYearOfPlentyCardsLeft;
                    currentPlayer.RoadBuildingCardsLeft += currentPlayer.unusedRoadBuildingCardsLeft;

                    currentPlayer.unusedKnightCardsLeft = 0;
                    currentPlayer.unusedMonopolyCardsLeft = 0;
                    currentPlayer.unusedYearOfPlentyCardsLeft = 0;
                    currentPlayer.unusedRoadBuildingCardsLeft = 0;


                    do
                    {
                        Console.WriteLine("1.Roll dice");
                        Console.WriteLine("2.Use development Card");
                        Resources res = new Resources();
                        String resInput;
                        String stringInput = Console.ReadLine();
                        String inputNode1;
                        String inputNode2;
                        int intInputNode1;
                        int intInputNode2;
                        Node nodeRoad1;
                        Node nodeRoad2;
                        Node nodeToBeSettled;
                        String confirm;
                        String devInput = null;

                        input = Int32.Parse(stringInput);

                        switch (input)
                        {
                            case 1:
                                int[] diceNumber = TurnMethods.Roll();
                                int diceSum = diceNumber[0] + diceNumber[1];

                                Console.WriteLine($"Zarul indica: {diceSum}");
                                ShowStats(gs, playerOrder, tourCount); //TESTING


                                if (diceSum == 7)
                                {
                                    TurnMethods.SevenRolled(gs.Map, players, currentPlayer);
                                    ShowStats(gs, playerOrder, tourCount); //TESTING
                                }
                                else
                                {
                                    TurnMethods.NotSevenRolled(gs.Map, diceSum);
                                }

                                bool finishedRound = false;

                                while (finishedRound == false)
                                {
                                    Console.WriteLine("1.Build settlement");
                                    Console.WriteLine("2.Upgrade to city");
                                    Console.WriteLine("3.Build Road");
                                    Console.WriteLine("4.Buy dev card");
                                    Console.WriteLine("5.Use dev card");
                                    Console.WriteLine("6.Trade with players");
                                    Console.WriteLine("7.Trade with bank");
                                    Console.WriteLine("8.Finish");

                                    stringInput = Console.ReadLine();

                                    int input2 = Int32.Parse(stringInput);


                                    switch (input2)
                                    {
                                        case 1:

                                            bool ignoreCost = false;
                                            Console.WriteLine("Player-ul " + currentPlayer.Name + "(" + currentPlayer.Id + ")" + " va construi un settlement pe nodul: ");
                                            Console.WriteLine("***Type 0 if you want to exit.***");
                                            String inputNode = Console.ReadLine();

                                            int intInputNode = Int32.Parse(inputNode);
                                            if (intInputNode != 0)
                                            {
                                                nodeToBeSettled = gs.Map.Nodes.FirstOrDefault(n => n.Id == intInputNode);

                                                Build.BuildSettlement(currentPlayer, nodeToBeSettled, gs.Map, ignoreCost);

                                                ShowStats(gs, playerOrder, tourCount); //TESTING
                                            }
                                            break;

                                        case 2:

                                            ignoreCost = false;
                                            Console.WriteLine("Player-ul " + currentPlayer.Name + "(" + currentPlayer.Id + ")" + " va construi un settlement pe nodul: ");
                                            Console.WriteLine("***Type 0 if you want to exit.***");
                                            //String inputNode;
                                            inputNode = Console.ReadLine();

                                            intInputNode = Int32.Parse(inputNode);
                                            if (intInputNode != 0)
                                            {

                                                nodeToBeSettled = gs.Map.Nodes.FirstOrDefault(n => n.Id == intInputNode);

                                                Build.BuildSettlement(currentPlayer, nodeToBeSettled, gs.Map, ignoreCost);
                                                ShowStats(gs, playerOrder, tourCount); //TESTING
                                            }
                                            break;

                                        case 3:
                                            ignoreCost = false;

                                            Console.WriteLine("Player-ul " + currentPlayer.Name + "(" + currentPlayer.Id + ")" + " va construi un drum intre nodurile: ");
                                            Console.WriteLine("***Type 0 if you want to exit.***");
                                            inputNode1 = Console.ReadLine();

                                            if (inputNode1 != "0")
                                            {
                                                inputNode2 = Console.ReadLine();


                                                intInputNode1 = Int32.Parse(inputNode1);
                                                intInputNode2 = Int32.Parse(inputNode2);

                                                nodeRoad1 = gs.Map.Nodes.FirstOrDefault(n => n.Id == intInputNode1);
                                                nodeRoad2 = gs.Map.Nodes.FirstOrDefault(n => n.Id == intInputNode2);


                                                Build.BuildRoad(currentPlayer, nodeRoad1, nodeRoad2, gs.Map, ignoreCost);
                                                ShowStats(gs, playerOrder, tourCount); //TESTING
                                            }
                                            break;
                                        case 4:
                                            Console.WriteLine("***Do you confirm this purchase?[y/n]");
                                            confirm = Console.ReadLine();

                                            if (confirm == "y")
                                            {
                                                if (Cards.buyDevCard(gs, currentPlayer, gameType) != false)
                                                    Console.WriteLine("Jucatorul " + currentPlayer.Name + "(" + currentPlayer.Id + ")" + " a cumparat o carte de dezvoltare");
                                                ShowStats(gs, playerOrder, tourCount); //TESTING
                                            }
                                            break;
                                        case 5:
                                            currentPlayer.PrintDevCards();
                                            if (currentPlayer.KnightCardsLeft != 0)
                                            {
                                                Console.WriteLine("Type 'knight' to use the Knight Card");
                                            }

                                            if (currentPlayer.VictoryPointCardsLeft != 0)
                                            {
                                                Console.WriteLine("Type 'point' to use the Victory Card");
                                            }

                                            if (currentPlayer.RoadBuildingCardsLeft != 0)
                                            {
                                                Console.WriteLine("Type 'road' to use the RoadBuilding Card");
                                            }

                                            if (currentPlayer.YearOfPlentyCardsLeft != 0)
                                            {
                                                Console.WriteLine("Type 'year' to use the YearOfPlenty Card");
                                            }

                                            if (currentPlayer.MonopolyCardsLeft != 0)
                                            {
                                                Console.WriteLine("Type 'monopoly' to use the Monopoly Card");
                                            }

                                            isResource = false;
                                            while (!isResource)
                                            {
                                                devInput = Console.ReadLine();

                                                foreach (String s in devCardsName)
                                                {
                                                    if (s.Equals(devInput))
                                                    {
                                                        isResource = true;
                                                    }
                                                }

                                                if (!isResource)
                                                {
                                                    Console.WriteLine(devInput + " is not a resource.");
                                                }
                                            }

                                            switch (devInput)
                                            {
                                                case "knight":

                                                    Console.WriteLine("***Do you confirm using this card?[y/n]");
                                                    confirm = Console.ReadLine();

                                                    if (confirm == "y")
                                                    {
                                                        if (!Cards.UseKnightCard(players, currentPlayer, gs))
                                                        {
                                                            Console.WriteLine("Nu detii aceasta carte sau actiunea ta este invalida!");
                                                        }
                                                    }
                                                    break;


                                                case "road":
                                                    Console.WriteLine("***Do you confirm using this card?[y/n]");
                                                    confirm = Console.ReadLine();

                                                    if (confirm == "y")
                                                    {
                                                        Console.WriteLine("Player-ul " + currentPlayer.Name + "(" + currentPlayer.Id + ")" + " va construi un drum intre nodurile: ");

                                                        inputNode1 = Console.ReadLine();
                                                        inputNode2 = Console.ReadLine();


                                                        intInputNode1 = Int32.Parse(inputNode1);
                                                        intInputNode2 = Int32.Parse(inputNode2);

                                                        nodeRoad1 = gs.Map.Nodes.FirstOrDefault(n => n.Id == intInputNode1);
                                                        nodeRoad2 = gs.Map.Nodes.FirstOrDefault(n => n.Id == intInputNode2);
                                                        if (!Cards.UseRoadBuildingCard(currentPlayer, nodeRoad1, nodeRoad2, gs.Map))
                                                        {
                                                            Console.WriteLine("Nu detii aceasta carte sau actiunea ta este invalida!");
                                                        }
                                                        ShowStats(gs, playerOrder, tourCount); //TESTING
                                                    }
                                                    break;

                                                case "year":

                                                    Console.WriteLine("***Do you confirm using this card?[y/n]");
                                                    confirm = Console.ReadLine();

                                                    if (confirm == "y")
                                                    {
                                                        Console.WriteLine("Resursa pe care vrei sa o iei de la banca (litere mici): ");
                                                        resInput = Console.ReadLine();

                                                        switch (resInput)
                                                        {
                                                            case "wheat":
                                                                res = Resources.Wheat;
                                                                break;
                                                            case "stone":
                                                                res = Resources.Stone;
                                                                break;
                                                            case "clay":
                                                                res = Resources.Clay;
                                                                break;
                                                            case "wood":
                                                                res = Resources.Wood;
                                                                break;
                                                            case "sheep":
                                                                res = Resources.Sheep;
                                                                break;
                                                        }

                                                        if (!Cards.UseYearOfPlentyCard(currentPlayer, res))
                                                        {
                                                            Console.WriteLine("Nu detii aceasta carte sau actiunea ta este invalida!");
                                                        }
                                                        ShowStats(gs, playerOrder, tourCount); //TESTING
                                                    }
                                                    break;

                                                case "monopoly":

                                                    Console.WriteLine("***Do you confirm using this card?[y/n]");
                                                    confirm = Console.ReadLine();

                                                    if (confirm == "y")
                                                    {
                                                        Console.WriteLine("Resursa pe care vrei sa o furi(litere mici): ");
                                                        resInput = Console.ReadLine();

                                                        switch (resInput)
                                                        {
                                                            case "wheat":
                                                                res = Resources.Wheat;
                                                                break;
                                                            case "stone":
                                                                res = Resources.Stone;
                                                                break;
                                                            case "clay":
                                                                res = Resources.Clay;
                                                                break;
                                                            case "wood":
                                                                res = Resources.Wood;
                                                                break;
                                                            case "sheep":
                                                                res = Resources.Sheep;
                                                                break;
                                                        }

                                                        if (!Cards.UseMonopolyCard(currentPlayer, players, res))
                                                        {
                                                            Console.WriteLine("Nu detii aceasta carte sau actiunea ta este invalida!");
                                                        }
                                                        ShowStats(gs, playerOrder, tourCount); //TESTING
                                                    }
                                                    break;
                                            }
                                            break;

                                        case 6:
                                            {
                                                foreach (Player p in playerOrder)
                                                {
                                                    if (p == currentPlayer)
                                                        continue;

                                                    Console.WriteLine("Resursele lui " + p.Name + "(" + currentPlayer.Id + ")" + " inainte de trade:");
                                                    Console.WriteLine("Wheat:" + p.WheatQty + " Sheep:" + p.SheepQty + " Wood:" + p.WoodQty + " Clay:" + p.ClayQty + " Stone:" + p.StoneQty + "\n");

                                                }
                                                Trade.initTradeWithPlayers(currentPlayer, gs);
                                                ShowStats(gs, playerOrder, tourCount); //TESTING
                                                break;
                                            }
                                        case 7:
                                            {
                                                Trade.initTradeWithBank(currentPlayer, gs.Map);
                                                ShowStats(gs, playerOrder, tourCount); //TESTING
                                                break;
                                            }
                                        case 8:
                                            Console.WriteLine("***Do you confirm ending your turn?[y/n]");
                                            confirm = Console.ReadLine();

                                            if (confirm == "y")
                                            {
                                                finishedRound = true;
                                                input = 0;
                                            }
                                            break;
                                    }

                                    if ((winnerName = TurnMethods.CheckWinCondition(gs, currentPlayer)) != "False")
                                    {
                                        Console.WriteLine("Winner winner chicken dinner: " + winnerName + "(" + currentPlayer.Id + ")");
                                        break;
                                    }
                                }

                                break;

                            case 2:
                                currentPlayer.PrintDevCards();
                                if (currentPlayer.KnightCardsLeft != 0)
                                {
                                    Console.WriteLine("Type 'knight' to use the Knight Card");
                                }

                                if (currentPlayer.VictoryPointCardsLeft != 0)
                                {
                                    Console.WriteLine("Type 'point' to use the Knight Card");
                                }

                                if (currentPlayer.RoadBuildingCardsLeft != 0)
                                {
                                    Console.WriteLine("Type 'road' to use the Knight Card");
                                }

                                if (currentPlayer.YearOfPlentyCardsLeft != 0)
                                {
                                    Console.WriteLine("Type 'year' to use the Knight Card");
                                }

                                if (currentPlayer.MonopolyCardsLeft != 0)
                                {
                                    Console.WriteLine("Type 'monopoly' to use the Knight Card");
                                }

                                isResource = false;
                                while (!isResource)
                                {
                                    devInput = Console.ReadLine();

                                    foreach (String s in devCardsName)
                                    {
                                        if (s.Equals(devInput))
                                        {
                                            isResource = true;
                                        }
                                    }

                                    if (!isResource)
                                    {
                                        Console.WriteLine(devInput + " is not a resource.");
                                    }
                                }

                                switch (devInput)
                                {
                                    case "knight":
                                        if (!Cards.UseKnightCard(players, currentPlayer, gs))
                                        {
                                            Console.WriteLine("Nu detii aceasta carte sau actiunea ta este invalida!");

                                        }
                                        break;

                                    case "road":
                                        Console.WriteLine("Player-ul " + currentPlayer.Name + "(" + currentPlayer.Id + ")" + " v-a construi un drum intre nodurile: ");

                                        inputNode1 = Console.ReadLine();
                                        inputNode2 = Console.ReadLine();

                                        intInputNode1 = Int32.Parse(inputNode1);
                                        intInputNode2 = Int32.Parse(inputNode2);

                                        nodeRoad1 = gs.Map.Nodes.FirstOrDefault(n => n.Id == intInputNode1);
                                        nodeRoad2 = gs.Map.Nodes.FirstOrDefault(n => n.Id == intInputNode2);
                                        if (!Cards.UseRoadBuildingCard(currentPlayer, nodeRoad1, nodeRoad2, gs.Map))
                                        {

                                            Console.WriteLine("Nu detii aceasta carte sau actiunea ta este invalida!");
                                        }
                                        ShowStats(gs, playerOrder, tourCount + 1); //TESTING
                                        break;

                                    case "year":

                                        Console.WriteLine("Resursa pe care vrei sa o iei de la banca (litere mici): ");
                                        resInput = Console.ReadLine();

                                        switch (resInput)
                                        {
                                            case "wheat":
                                                res = Resources.Wheat;
                                                break;
                                            case "stone":
                                                res = Resources.Stone;
                                                break;
                                            case "clay":
                                                res = Resources.Clay;
                                                break;
                                            case "wood":
                                                res = Resources.Wood;
                                                break;
                                            case "sheep":
                                                res = Resources.Sheep;
                                                break;
                                        }

                                        if (!Cards.UseYearOfPlentyCard(currentPlayer, res))
                                        {
                                            Console.WriteLine("Nu detii aceasta carte sau actiunea ta este invalida!");
                                        }
                                        ShowStats(gs, playerOrder, tourCount); //TESTING
                                        break;

                                    case "monopoly":

                                        Console.WriteLine("Resursa pe care vrei sa o furi(litere mici): ");
                                        resInput = Console.ReadLine();

                                        switch (resInput)
                                        {
                                            case "wheat":
                                                res = Resources.Wheat;
                                                break;
                                            case "stone":
                                                res = Resources.Stone;
                                                break;
                                            case "clay":
                                                res = Resources.Clay;
                                                break;
                                            case "wood":
                                                res = Resources.Wood;
                                                break;
                                            case "sheep":
                                                res = Resources.Sheep;
                                                break;
                                        }

                                        if (!Cards.UseMonopolyCard(currentPlayer, players, res))
                                        {
                                            Console.WriteLine("Nu detii aceasta carte sau actiunea ta este invalida!");
                                        }
                                        ShowStats(gs, playerOrder, tourCount); //TESTING

                                        break;
                                }
                                input = 1;

                                //ShowStats(gs, playerOrder, tourCount); //TESTING
                                break;
                        }

                        if (winnerName != "False")
                        {
                            break;
                        }

                    } while (input != 0);

                }

            }

        }







        /*foreach (Player player in gs.Players)
        {
            Console.WriteLine(player.ToString());
        }*/

        /*JsonSerializer serializer = new JsonSerializer();
        serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        serializer.Formatting = Formatting.Indented;
        serializer.MaxDepth = 4;
        using (StreamWriter sw = new StreamWriter(@"d:\gamestatejson.txt"))
        using (JsonWriter writer= new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, gs);
        }*/

        /*gs.Map.Nodes[10].SettlePlayer(players[0], Settlements.Village);
        gs.Map.Nodes[10].SettlePlayer(players[0], Settlements.City);
        gs.Map.Nodes[11].SettlePlayer(players[1], Settlements.Village);
        gs.Map.Hexes[4].Number = 5;
        gs.Map.Hexes[4].Resource = Resources.Wheat;
        gs.Map.Hexes[1].Number = 5;
        foreach (Hex hex in gs.Map.HexNeighborsOfNodes[10])
        {
            Console.WriteLine(hex.ToString());
        }
        TurnMethods.NotSevenRolled(gs.Map, 5);
        players[0].PrintResources();
        players[1].PrintResources();
        Console.WriteLine(gs.Map.Nodes[10].ToString());*/
    }
}