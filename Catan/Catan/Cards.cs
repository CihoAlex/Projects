using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace Catan
{
    public static class Cards
    {
        //numarul total de carti la inceputul jocului;

        //comparatorii pentru metoda giveDevCard();

        static int kcComp;  // knight card comparator

        static int vpComp1; // victory point comparator
        static int vpComp2; // victory point comparator

        static int rbComp1; // road building comparator
        static int rbComp2; // road building comparator

        static int ypComp1; // year of plenty comparator
        static int ypComp2; // year of plenty comparator

        static int mcComp1; // monopoly comparator
        static int mcComp2; // monopoly comparator

        //private TurnMethods MethodAccess = new TurnMethods();

        public static void initializeComp(int gameType)
        {
            if (gameType == 1)
            {
                kcComp = 15;

                vpComp1 = 14;
                vpComp2 = 20;

                rbComp1 = 19;
                rbComp2 = 22;

                ypComp1 = 21;
                ypComp2 = 24;

                mcComp1 = 23;
                mcComp2 = 26;
            }
            else
            {
                kcComp = 21; //+6 knightcards

                vpComp1 = 20;
                vpComp2 = 26;

                rbComp1 = 26; //+1
                rbComp2 = 29;

                ypComp1 = 29; //+1
                ypComp2 = 32;

                mcComp1 = 32; //+1
                mcComp2 = 35;
            }
        }

        public static bool buyDevCard(GameState gs, Player currentPlayer, int gameType)
        {
            if (currentPlayer.WheatQty > 0)
                if (currentPlayer.StoneQty > 0)
                    if (currentPlayer.SheepQty > 0)
                    {
                        currentPlayer.WheatQty = currentPlayer.WheatQty - 1;
                        currentPlayer.StoneQty = currentPlayer.StoneQty - 1;
                        currentPlayer.SheepQty = currentPlayer.SheepQty - 1;
                        giveDevCard(gs, currentPlayer, gameType);
                        return true;
                    }
                    else
                    {
                        notEnoughResources(Resources.Sheep, currentPlayer);
                        return false;
                    }
                else
                {
                    notEnoughResources(Resources.Stone, currentPlayer);
                    return false;
                }
            else
            {
                notEnoughResources(Resources.Wheat, currentPlayer);
                return false;
            }
        }
        public static void giveDevCard(GameState gs, Player currentPlayer, int gameType)
        {
            /* algoritmul alege un nr random si il memoreaza in variabila card;
			 * in pachet sunt 14 KC, 5 VP, 2 RB, 2 YP si 2 MC si le vom compara folosind comparatorii(initializati mai sus);
			 * daca de exemplu card = 13 => comparatorul lui KC sa va decrementa impreuna cu ceilalti comparatori si nr de carti va fi 24;
			 * */
            //int cardsLeft = getCardsLeft(gm);
            Random rand = new Random();
            int card = rand.Next(1, gs.CardsLeft);
            if (gameType == 1)
            {
                if (card < kcComp)
                {
                    kcComp--; vpComp1--; vpComp2--; rbComp1--; rbComp2--; ypComp1--; ypComp2--; mcComp1--; mcComp2--; gs.CardsLeft--; gs.KnightCardsLeft--;
                    currentPlayer.unusedKnightCardsLeft = currentPlayer.unusedKnightCardsLeft + 1;
                }

                else if (card > vpComp1 && card < vpComp2)
                {
                    vpComp2--; rbComp1--; rbComp2--; ypComp1--; ypComp2--; mcComp1--; mcComp2--; gs.CardsLeft--; gs.VictoryPointCardsLeft--;
                    currentPlayer.VictoryPointCardsLeft = currentPlayer.VictoryPointCardsLeft + 1;
                    UseVictoryCard(currentPlayer);
                }

                else if (card > rbComp1 && card < rbComp2)
                {
                    rbComp2--; ypComp1--; ypComp2--; mcComp1--; mcComp2--; gs.CardsLeft--; gs.RoadBuildingCardsLeft--;
                    currentPlayer.unusedRoadBuildingCardsLeft = currentPlayer.unusedRoadBuildingCardsLeft + 1;
                }

                else if (card > ypComp1 && card < ypComp2)
                {
                    ypComp2--; mcComp1--; mcComp2--; gs.CardsLeft--; gs.YearOfPlentyCardsLeft--;
                    currentPlayer.unusedYearOfPlentyCardsLeft = currentPlayer.unusedYearOfPlentyCardsLeft + 1;
                }

                else if (card > mcComp1 && card < mcComp2)
                {
                    mcComp2--; gs.CardsLeft--; gs.MonopolyCardsLeft--;
                    currentPlayer.unusedMonopolyCardsLeft = currentPlayer.unusedMonopolyCardsLeft + 1;
                }
            }
            else
            {
                if (card < kcComp)
                {
                    kcComp--; vpComp1--; vpComp2--; rbComp1--; rbComp2--; ypComp1--; ypComp2--; mcComp1--; mcComp2--; gs.CardsLeft--; gs.KnightCardsLeft--;
                    currentPlayer.unusedKnightCardsLeft = currentPlayer.unusedKnightCardsLeft + 1;
                }

                else if (card > vpComp1 && card < vpComp2)
                {
                    vpComp2--; rbComp1--; rbComp2--; ypComp1--; ypComp2--; mcComp1--; mcComp2--; gs.CardsLeft--; gs.VictoryPointCardsLeft--;
                    currentPlayer.VictoryPointCardsLeft = currentPlayer.VictoryPointCardsLeft + 1;
                }

                else if (card > rbComp1 && card < rbComp2)
                {
                    rbComp2--; ypComp1--; ypComp2--; mcComp1--; mcComp2--; gs.CardsLeft--; gs.RoadBuildingCardsLeft--;
                    currentPlayer.unusedRoadBuildingCardsLeft = currentPlayer.unusedRoadBuildingCardsLeft + 1;
                }

                else if (card > ypComp1 && card < ypComp2)
                {
                    ypComp2--; mcComp1--; mcComp2--; gs.CardsLeft--; gs.YearOfPlentyCardsLeft--;
                    currentPlayer.unusedYearOfPlentyCardsLeft = currentPlayer.unusedYearOfPlentyCardsLeft + 1;
                }

                else if (card > mcComp1 && card < mcComp2)
                {
                    mcComp2--; gs.CardsLeft--; gs.MonopolyCardsLeft--;
                    currentPlayer.unusedMonopolyCardsLeft = currentPlayer.unusedMonopolyCardsLeft + 1;
                }
            }
        }

        public static bool UseKnightCard(List<Player> allPlayers, Player activePlayer, GameState gs)
        {
            if (activePlayer.KnightCardsLeft == 0)
                return false;

            activePlayer.KnightCardsUsed++;

            if (activePlayer.KnightCardsUsed >= 3 && activePlayer.HasLargestArmy == false)
            {
                bool hasLargestArmy = true;

                foreach (Player player in allPlayers)
                {
                    if (player != activePlayer && activePlayer.KnightCardsUsed <= player.KnightCardsUsed)
                    {
                        hasLargestArmy = false;
                        break;
                    }

                    if (hasLargestArmy == true)
                    {
                        Player currentLargestArmyPlayer = allPlayers.FirstOrDefault(player1 => player1.HasLargestArmy == true);

                        if (currentLargestArmyPlayer != null)
                        {
                            currentLargestArmyPlayer.HasLargestArmy = false;
                            currentLargestArmyPlayer.LargestArmyPoints = 0;
                        }

                        activePlayer.LargestArmyPoints = 2;
                        activePlayer.HasLargestArmy = true;
                    }
                }
            }

            int number;
            while (true)
            {
                Console.Write("\nType the hex ID where you want to put the robber: range [1, 19]: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out number))
                {
                    if (gs.Map.Hexes.Count == 37)
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

            Hex newRobberHex = gs.Map.Hexes[number];
            gs.Map.MoveRobber(newRobberHex);

            List<Node> settledNodes = newRobberHex.SettledNeighborNodes(gs.Map).Where(n => n.PlayerSettled != activePlayer).ToList();
            if (settledNodes.Count() == 0)
            {
                Console.WriteLine("You moved the robber, but there are no settlements to steal resources from...");
                return false;
            }

            Player playerToStealFrom = new Player();
            Console.WriteLine("You can steal from the players: ");
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

            TurnMethods.Steal(activePlayer, playerToStealFrom);
            activePlayer.KnightCardsLeft--;
            return true;
        }

        public static bool UseRoadBuildingCard(Player activePlayer, Node n1, Node n2, Map map) //use twice
        {
            if (activePlayer.RoadBuildingCardsLeft == 0) return false;
            //int r1n1, r1n2, r2n1, r2n2;

            // BuildRoad(Player p, Node n1, Node n2, Map m)
            activePlayer.RoadBuildingCardsLeft--;
            return Build.BuildRoad(activePlayer, n1, n2, map, true);
        }

        public static bool UseYearOfPlentyCard(Player activePlayer, Resources resource)
        {
            if (activePlayer.YearOfPlentyCardsLeft == 0) return false;
            switch (resource)
            {
                case Resources.Wheat:
                    activePlayer.WheatQty += 2;
                    break;

                case Resources.Clay:
                    activePlayer.ClayQty += 2;
                    break;

                case Resources.Sheep:
                    activePlayer.SheepQty += 2;
                    break;

                case Resources.Stone:
                    activePlayer.StoneQty += 2;
                    break;

                case Resources.Wood:
                    activePlayer.WoodQty += 2;
                    break;
            }
            activePlayer.YearOfPlentyCardsLeft--;
            return true;
        }

        public static bool UseMonopolyCard(Player activePlayer, List<Player> allPlayers, Resources resource)
        {
            if (activePlayer.MonopolyCardsLeft == 0) return false;
            List<Player> nonActivePlayers = allPlayers.Where(player => player != activePlayer).ToList();
            foreach (Player player in nonActivePlayers)
            {
                switch (resource)
                {
                    case Resources.Wheat:
                        activePlayer.WheatQty += player.WheatQty;
                        player.WheatQty = 0;

                        break;

                    case Resources.Clay:
                        activePlayer.ClayQty += player.ClayQty;
                        player.ClayQty = 0;

                        break;

                    case Resources.Sheep:
                        activePlayer.SheepQty += player.SheepQty;
                        player.SheepQty = 0;

                        break;

                    case Resources.Stone:
                        activePlayer.StoneQty += player.StoneQty;
                        player.StoneQty = 0;
                        break;

                    case Resources.Wood:
                        activePlayer.WoodQty += player.WoodQty;
                        player.WoodQty = 0;
                        break;
                }
            }
            activePlayer.MonopolyCardsLeft--;
            return true;
        }

        public static bool UseVictoryCard(Player currentPlayer)
        {
            if (currentPlayer.VictoryPointCardsLeft == 0) return false;
            currentPlayer.HiddenPoints += 1;
            return true;
        }

        public static void notEnoughResources(Resources resource, Player currentPlayer)
        {
            if (currentPlayer.WheatQty < 1)
            {
                Console.WriteLine("Player lacks wheat");
            }

            if (currentPlayer.StoneQty < 1)
            {
                Console.WriteLine("Player lacks stone");
            }

            if (currentPlayer.SheepQty < 1)
            {
                Console.WriteLine("Player lacks sheep");
            }
        }

    }
}