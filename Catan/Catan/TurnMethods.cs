using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
//using Newtonsoft.Json;

namespace Catan
{
    public static class TurnMethods
    {

        public static List<T> Shuffle<T>(this IList<T> list, int seed)
        {
            List<T> copy = new List<T>(list);
            Random rng = new Random(seed);

            int n = copy.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = copy[k];
                copy[k] = copy[n];
                copy[n] = value;
            }

            return copy;
        }
        public static void NotSevenRolled(Map map, int diceRoll)
        {
            List<Hex> rolledHexes = map.Hexes.Where(hex => (hex.Number == diceRoll && hex.HasRobber == false)).ToList();

            if (!(rolledHexes.Count == 0))
            {
                foreach (Hex hex in rolledHexes)
                {
                    foreach (Node node in hex.SettledNeighborNodes(map))
                    {
                        switch (hex.Resource)
                        {
                            case Resources.Clay:
                                node.PlayerSettled.ClayQty += (node.SettlementType == Settlements.Village) ? 1 : 2;
                                Console.WriteLine("added clay");
                                break;
                            case Resources.Sheep:
                                node.PlayerSettled.SheepQty += (node.SettlementType == Settlements.Village) ? 1 : 2;
                                Console.WriteLine("added sheep");
                                break;
                            case Resources.Stone:
                                node.PlayerSettled.StoneQty += (node.SettlementType == Settlements.Village) ? 1 : 2;
                                Console.WriteLine("added stone");
                                break;
                            case Resources.Wheat:
                                node.PlayerSettled.WheatQty += (node.SettlementType == Settlements.Village) ? 1 : 2;
                                Console.WriteLine("added wheat");
                                break;
                            case Resources.Wood:
                                node.PlayerSettled.WoodQty += (node.SettlementType == Settlements.Village) ? 1 : 2;
                                Console.WriteLine("added wood");
                                break;
                        }
                    }
                }
            }
        }

        public static String CheckWinCondition(GameState gs, Player player)
        {
            //foreach (Player player in gs.Players)
            //{
            if (player.Points + player.HiddenPoints + player.LongestRoadPoints + player.LargestArmyPoints >= gs.WinningPoints)
                return player.Name;
            // }
            return "False";
        }

        public static void SevenRolled(Map map, List<Player> allPlayers, Player currentPlayer)
        {
            String[] names = new String[5] { "wheat", "stone", "wood", "clay", "sheep" };
            int discardedItems = 0;
            int totalResources;
            int number;
            String outputInfo;
            String resourceInput;
            bool isResource;

            //TODO
            foreach (Player player in allPlayers)
            {
                discardedItems = 0;
                List<Resources> discarded = new List<Resources>();
                if (player.WheatQty + player.WoodQty + player.StoneQty + player.SheepQty + player.ClayQty > 7)
                {
                    Console.WriteLine(player.Name + " are mai mult de 7 carti");
                    outputInfo = "\nWheatQty:" + player.WheatQty + "; SheepQty:" + player.SheepQty + "; ClayQty:" + player.ClayQty + "; StoneQty:" + player.StoneQty + "; WoodQty:" + player.WoodQty;
                    Console.WriteLine(outputInfo);

                    totalResources = (player.WheatQty + player.WoodQty + player.SheepQty + player.ClayQty + player.StoneQty) / 2;
                    do
                    {
                        isResource = false;
                        Console.WriteLine("\nType the resource you want to discard");
                        resourceInput = Console.ReadLine();

                        foreach (String s in names)
                        {
                            if (s.Equals(resourceInput))
                            {
                                isResource = true;
                            }
                        }

                        if (!isResource)
                        {
                            Console.WriteLine(resourceInput + " is not a resource.");
                            continue;
                        }

                        if (resourceInput == "wheat")
                        {
                            if (player.WheatQty == 0)
                            {
                                Console.WriteLine("Player has 0 wheat. Try another resource.");
                                continue;
                            }
                            else Console.WriteLine("Player has " + player.WheatQty + " wheat resources left");
                        }
                        else if (resourceInput == "sheep")
                        {
                            if (player.SheepQty == 0)
                            {
                                Console.WriteLine("Player has 0 sheep. Try another resource.");
                                continue;
                            }
                            else Console.WriteLine("Player has " + player.SheepQty + " sheep resources left");
                        }
                        else if (resourceInput == "wood")
                        {
                            if (player.WoodQty == 0)
                            {
                                Console.WriteLine("Player has 0 wood. Try another resource.");
                                continue;
                            }
                            else Console.WriteLine("Player has " + player.WoodQty + " wood resources left");
                        }
                        else if (resourceInput == "clay")
                        {
                            if (player.ClayQty == 0)
                            {
                                Console.WriteLine("Player has 0 clay. Try another resource.");
                                continue;
                            }
                            else Console.WriteLine("Player has " + player.ClayQty + " clay resources left");
                        }
                        else if (resourceInput == "stone")
                        {
                            if (player.StoneQty == 0)
                            {
                                Console.WriteLine("Player has 0 stone. Try another resource.");
                                continue;
                            }
                            else Console.WriteLine("Player has " + player.StoneQty + " stone resources left");
                        }

                        Console.WriteLine("You still need to offer " + (totalResources - discardedItems) + " resources");

                        while (true)
                        {
                            Console.WriteLine("\nType how many cards of that resource you want to discard");
                            var input = Console.ReadLine();

                            if (int.TryParse(input, out number))
                            {
                                if (number < 0) Console.WriteLine("Invalid number.");
                                else break;
                            }
                            else
                                Console.WriteLine("Invalid number.");
                        }

                        switch (resourceInput)
                        {
                            case "wheat":

                                if (number > player.WheatQty)
                                {
                                    Console.WriteLine(number + " wheat was requested, but the player has " + player.WheatQty + " so the number of wheat taken is " + player.WheatQty);
                                    number = player.WheatQty;
                                }
                                if (discardedItems + number > totalResources)
                                {
                                    number = totalResources - discardedItems;
                                    Console.WriteLine(" The required resources sum has been exeeded");
                                }
                                for (int i = 0; i < number; i++)
                                {
                                    discarded.Add(Resources.Wheat);
                                }
                                discardedItems = discardedItems + number;
                                player.Discard(discarded);
                                discarded.Clear();
                                break;
                            case "stone":
                                if (number > player.StoneQty)
                                {

                                    Console.WriteLine(number + " stone was requested, but the player has " + player.StoneQty + " so the number of stone taken is " + player.StoneQty);
                                    number = player.StoneQty;
                                }
                                if (discardedItems + number > totalResources)
                                {
                                    number = totalResources - discardedItems;
                                    Console.WriteLine(" The required resources sum has been exeeded");
                                }
                                for (int i = 0; i < number; i++)
                                {
                                    discarded.Add(Resources.Stone);
                                }
                                discardedItems = discardedItems + number;
                                player.Discard(discarded);
                                discarded.Clear();
                                break;
                            case "clay":
                                if (number > player.ClayQty)
                                {

                                    Console.WriteLine(number + " clay was requested, but the player has " + player.ClayQty + " so the number of clay taken is " + player.ClayQty);
                                    number = player.ClayQty;
                                }

                                if (discardedItems + number > totalResources)
                                {
                                    number = totalResources - discardedItems;
                                    Console.WriteLine(" The required resources sum has been exeeded");
                                }
                                for (int i = 0; i < number; i++)
                                {
                                    discarded.Add(Resources.Clay);
                                }
                                discardedItems = discardedItems + number;
                                player.Discard(discarded);
                                discarded.Clear();
                                break;
                            case "wood":
                                if (number > player.WoodQty)
                                {

                                    Console.WriteLine(number + " wood was requested, but the player has " + player.WoodQty + " so the number of wood taken is " + player.WoodQty);
                                    number = player.WoodQty;

                                }
                                if (discardedItems + number > totalResources)
                                {
                                    number = totalResources - discardedItems;
                                    Console.WriteLine(" The required resources sum has been exeeded");
                                }
                                for (int i = 0; i < number; i++)
                                {
                                    discarded.Add(Resources.Wood);
                                }
                                discardedItems = discardedItems + number;
                                player.Discard(discarded);
                                discarded.Clear();
                                break;
                            case "sheep":
                                if (number > player.SheepQty)
                                {
                                    Console.WriteLine(number + " sheep was requested, but the player has " + player.SheepQty + " so the number of wheat taken is " + player.SheepQty);
                                    number = player.SheepQty;
                                }
                                if (discardedItems + number > totalResources)
                                {
                                    number = totalResources - discardedItems;
                                    Console.WriteLine(" The required resources sum has been exeeded");
                                }
                                for (int i = 0; i < number; i++)
                                {
                                    discarded.Add(Resources.Sheep);
                                }
                                discardedItems = discardedItems + number;
                                player.Discard(discarded);
                                discarded.Clear();
                                break;
                        }
                        // BUG: == becomes <
                    } while (discardedItems < totalResources);
                }
            }
            // Choose where to put the robber and who to steal from
            while (true)
            {
                Console.Write("\nType the hex ID where you want to put the robber: range [1, 19]: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out number))
                {
                    if (map.Hexes.Count == 37)
                    {
                        if (number < 1 || number > 19) Console.WriteLine("Invalid number.");
                        else break;
                    }
                    else
                    {
                        if (number < 1 || number > 30) Console.WriteLine("Invalid number.");
                        else break;
                    }
                }
                else
                    Console.WriteLine("Invalid number.");
            }

            Hex newRobberHex = map.Hexes[number];
            map.MoveRobber(newRobberHex);

            List<Node> settledNodes = newRobberHex.SettledNeighborNodes(map).Where(n => n.PlayerSettled != currentPlayer).ToList();
            if (settledNodes.Count() == 0)
            {
                Console.WriteLine("You moved the robber, but there are no settlements to steal resources from...");
                return;
            }

            Player playerToStealFrom = new Player();
            Console.WriteLine(" You can steal from the players: ");
            foreach (Node node in settledNodes)
            {
                Console.WriteLine(node.PlayerSettled.Name + "  id: " + node.PlayerSettled.Id);
            }

            while (true)
            {
                Console.Write("\nInsert the id of the player you choose: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out number))
                {
                    if (number < 0 || settledNodes.Where(n => n.PlayerSettled.Id == number).Count() == 0) Console.WriteLine("Invalid number.");
                    else break;
                }
                else
                    Console.WriteLine("Invalid number.");
            }

            playerToStealFrom = allPlayers.Find(player1 => player1.Id == number);
            TurnMethods.Steal(currentPlayer, playerToStealFrom);
        }

        public static void Steal(Player currentPlayer, Player playerToStealFrom)
        {
            List<Resources> resourcesAvailable = new List<Resources>();

            if (playerToStealFrom.WheatQty > 0) resourcesAvailable.Add(Resources.Wheat);
            if (playerToStealFrom.ClayQty > 0) resourcesAvailable.Add(Resources.Clay);
            if (playerToStealFrom.SheepQty > 0) resourcesAvailable.Add(Resources.Sheep);
            if (playerToStealFrom.StoneQty > 0) resourcesAvailable.Add(Resources.Stone);
            if (playerToStealFrom.WoodQty > 0) resourcesAvailable.Add(Resources.Wood);

            Random random = new Random();
            int index = random.Next(resourcesAvailable.Count);

            switch (resourcesAvailable[index])
            {
                case Resources.Wheat:
                    currentPlayer.WheatQty++;
                    playerToStealFrom.WheatQty--;
                    break;

                case Resources.Clay:
                    currentPlayer.WheatQty++;
                    playerToStealFrom.ClayQty--;
                    break;

                case Resources.Sheep:
                    currentPlayer.SheepQty++;
                    playerToStealFrom.SheepQty--;
                    break;

                case Resources.Stone:
                    currentPlayer.StoneQty++;
                    playerToStealFrom.StoneQty--;
                    break;

                case Resources.Wood:
                    currentPlayer.WoodQty++;
                    playerToStealFrom.WoodQty--;
                    break;
            }
            Console.WriteLine("Player " + currentPlayer.Name + " stole " + resourcesAvailable[index].ToString() + " from player " + playerToStealFrom.Name);
        }

        public static int[] Roll()
        {
            int[] dice = new int[2];
            Random rand = new Random();
            Random rand1 = new Random();
            dice[0] = rand.Next(1, 7);
            dice[1] = rand1.Next(1, 7);
            return dice;
        }
    }
}