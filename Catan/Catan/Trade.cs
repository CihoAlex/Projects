using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Catan
{
    class Trade
    {
        public static void initTradeWithPlayers(Player activePlayer, GameState game)
        {
            if (activePlayer.WoodQty < 1 && activePlayer.WheatQty < 1 && activePlayer.ClayQty < 1 && activePlayer.SheepQty < 1 && activePlayer.StoneQty < 1)
            {
                return;
            }

            int[] resourcesW = { 0, 0, 0, 0, 0 };
            int[] resourcesG = { 0, 0, 0, 0, 0 };

            resourcesW = resourcesWanted(activePlayer);
            resourcesG = resourcesGiven(activePlayer);

            Player p;
            p = acceptTrade(activePlayer, game, resourcesW);
            /*
            wheat-0
            sheep-1
            wood-2
            clay-3
            stone-4
            */

            // activePlayer a refuzat trade-ul sau jucatorii nu au resurse
            if (p == null)
            {
                return;
            }

            //interschimbare resurse
            Console.WriteLine("\nResursele lui " + activePlayer.Name + " inainte de trade:");
            Console.WriteLine("Wheat:" + activePlayer.WheatQty + " Sheep:" + activePlayer.SheepQty + " Wood:" + activePlayer.WoodQty + " Clay:" + activePlayer.ClayQty + " Stone:" + activePlayer.StoneQty);
            Console.WriteLine("Resursele lui " + p.Name + " inainte de trade:");
            Console.WriteLine("Wheat:" + p.WheatQty + " Sheep:" + p.SheepQty + " Wood:" + p.WoodQty + " Clay:" + p.ClayQty + " Stone:" + p.StoneQty);

            activePlayer.WoodQty += resourcesW[2];
            activePlayer.WoodQty -= resourcesG[2];
            p.WoodQty += resourcesG[2];
            p.WoodQty -= resourcesW[2];

            activePlayer.WheatQty += resourcesW[0];
            activePlayer.WheatQty -= resourcesG[0];
            p.WheatQty += resourcesG[0];
            p.WheatQty -= resourcesW[0];

            activePlayer.SheepQty += resourcesW[1];
            activePlayer.SheepQty -= resourcesG[1];
            p.SheepQty += resourcesG[1];
            p.SheepQty -= resourcesW[1];

            activePlayer.ClayQty += resourcesW[3];
            activePlayer.ClayQty -= resourcesG[3];
            p.ClayQty += resourcesG[3];
            p.ClayQty -= resourcesW[3];

            activePlayer.StoneQty += resourcesW[4];
            activePlayer.StoneQty -= resourcesG[4];
            p.StoneQty += resourcesG[4];
            p.StoneQty -= resourcesW[4];

            Console.WriteLine("\nResursele lui " + activePlayer.Name + " dupa trade:");
            Console.WriteLine("Wheat:" + activePlayer.WheatQty + " Sheep:" + activePlayer.SheepQty + " Wood:" + activePlayer.WoodQty + " Clay:" + activePlayer.ClayQty + " Stone:" + activePlayer.StoneQty);
            Console.WriteLine("\nResursele lui " + p.Name + " dupa trade:");
            Console.WriteLine("Wheat:" + p.WheatQty + " Sheep:" + p.SheepQty + " Wood:" + p.WoodQty + " Clay:" + p.ClayQty + " Stone:" + p.StoneQty);
        }

        /*
         * 1. init trade (resursle pe care le da si alea pe care le vrea)
         * 2. accetTrade => jucatorul x cu care vrea sa faca (jucatorul x a acceptat trade-ul)
         * 3. verificam resursele lui x
         * 4. finalizam trade => interschimbam resursele
         * VIATZA E FRUMOAAAASAAAAAA
         */

        private static Player acceptTrade(Player activePlayer, GameState game, int[] resourcesWanted)
        {
            // iau toti jucatorii din gamestate in players
            List<Player> players = game.Players;
            List<Player> iAcceptTrade = new List<Player>();

            // player-ul este responsabil cu un raspuns dat de la tastatura
            String answer;

            // parcurg jucatorii din gamestate
            foreach (Player p in players)
            {
                // nu pot sa fac trade cu mine
                if (p == activePlayer)
                    continue;

                Console.WriteLine("Jucatorul " + p.Name + " accepta trade-ul cu tine? [y/n]");
                answer = Convert.ToString(Console.ReadLine());

                // daca raspunsul este da, am gasit jucatorul
                if (answer[0] == 'y')
                {
                    /*
                    wheat-0
                    sheep-1
                    wood-2
                    clay-3
                    stone-4
                    */

                    // verific daca are resursele necesare
                    if (p.WoodQty >= resourcesWanted[2] && p.WheatQty >= resourcesWanted[0] && p.ClayQty >= resourcesWanted[3] && p.SheepQty >= resourcesWanted[1] && p.StoneQty >= resourcesWanted[4])
                        iAcceptTrade.Add(p);
                    else
                        Console.WriteLine("Jucatorul " + p.Name + " nu are resursele necesare, deci nu accepta trade-ul.");
                }
            }

            // daca nici un jucator nu a acceptat trade-ul => nu fac trade
            if (iAcceptTrade.Count == 0)
            {
                Console.WriteLine("Niciun jucator nu a acceptat trade-ul sau nu au resursele necesare.");
                return null;
            }

            // cel putin un jucator a acceptat trade-ul:
            Console.WriteLine("Jucatori care au acceptat:");
            foreach (Player p in iAcceptTrade)
            {
                Console.WriteLine(p.Name + " id: " + p.Id);
            }

            // iau id-ul jucatorului de la consola
            Console.WriteLine("\nAlegeti jucatorul cu care doriti sa faceti trade. Introdu id: ");
            int idPlayer = Convert.ToInt32(Console.ReadLine());

            // referinta la player-ul care accepta trade-ul cu player-ul activePlayers
            Player playeracc = null;
            playeracc = iAcceptTrade.Find(player => idPlayer == player.Id);

            Console.WriteLine("\nConfirmi trade-ul? [y/n]");
            answer = Convert.ToString(Console.ReadLine());

            // am refuzat trade-ul ? deci nu fac cu nimeni trade => returnez null
            // am acceptat trade-ul => am jucatorul in playeracc
            if (answer[0] == 'y')
                return playeracc;
            else
            {
                Console.WriteLine("Ai refuzat trade-ul...");
                return null;
            }
        }

        public static void initTradeWithBank(Player activePlayer, Map m)
        {
            /*
             * player setllement pe un nod cu port => simple 3 : 1/specialized 2 : 1 (dai 2, iei 1)
             * banca: 4 : 1
               cu port universal 3 : 1
               cu port specializat 2:1 pt resursa specifica
             */

            /*
            wheat-0
            sheep-1
            wood-2
            clay-3
            stone-4
            */
            List<Node> playerNodesWithPorts = m.Nodes.Where(node => node.PlayerSettled == activePlayer && node.HasPort).ToList();
            int[] resourcesG = { 0, 0, 0, 0, 0 };
            int[] resourcesW = { 0, 0, 0, 0, 0 };
            int[] ratios = { 4, 4, 4, 4, 4 };
            int i;

            foreach (Node node in playerNodesWithPorts)
            {
                switch (node.PortType)
                {
                    case Ports.Simple:
                        {
                            for (i = 0; i < 5; i++)
                                if (ratios[i] == 4)
                                    ratios[i] = 3;
                            break;
                        }
                    case Ports.Wheat:
                        {
                            ratios[0] = 2;
                            break;
                        }
                    case Ports.Sheep:
                        {
                            ratios[1] = 2;
                            break;
                        }
                    case Ports.Wood:
                        {
                            ratios[2] = 2;
                            break;
                        }
                    case Ports.Clay:
                        {
                            ratios[3] = 2;
                            break;
                        }
                    case Ports.Stone:
                        {
                            ratios[4] = 2;
                            break;
                        }
                }
            }

            for (i = 0; i < 5; i++)
            {
                Console.WriteLine("Ratia " + i + " este " + ratios[i]);
            }

            Console.WriteLine("Jucatorul " + activePlayer.Name + " initiaza trade cu banca.");
            while (true)
            {
                resourcesW = resourcesWanted(activePlayer);
                int counterWanted = 0;
                int counterGiven = 0;
                for (i = 0; i < 5; i++)
                {
                    counterWanted = counterWanted + resourcesW[i] * ratios[i];
                }

                resourcesG = resourcesGiven(activePlayer);
                for (i = 0; i < 5; i++)
                {
                    counterGiven = counterGiven + resourcesG[i];
                }
                Console.WriteLine("counterWanted:" + counterWanted);
                Console.WriteLine("counterGiven:" + counterGiven);

                Console.WriteLine("Jucatorul " + activePlayer.Name + " vrea sa confirme trade-ul cu banca? [y/n]");
                String answer = Convert.ToString(Console.ReadLine());

                if (answer[0] == 'y')
                {
                    if (counterWanted == counterGiven)
                    {
                        Console.WriteLine("\nResursele lui " + activePlayer.Name + " inainte de trade cu banca:");
                        Console.WriteLine("Wheat:" + activePlayer.WheatQty + " Sheep:" + activePlayer.SheepQty + " Wood:" + activePlayer.WoodQty + " Clay:" + activePlayer.ClayQty + " Stone:" + activePlayer.StoneQty);

                        activePlayer.WheatQty += resourcesW[0];
                        activePlayer.WheatQty -= resourcesG[0];

                        activePlayer.SheepQty += resourcesW[1];
                        activePlayer.SheepQty -= resourcesG[1];

                        activePlayer.WoodQty += resourcesW[2];
                        activePlayer.WoodQty -= resourcesG[2];

                        activePlayer.ClayQty += resourcesW[3];
                        activePlayer.ClayQty -= resourcesG[3];

                        activePlayer.StoneQty += resourcesW[4];
                        activePlayer.StoneQty -= resourcesG[4];

                        Console.WriteLine("\nResursele lui " + activePlayer.Name + " dupa trade cu banca:");
                        Console.WriteLine("Wheat:" + activePlayer.WheatQty + " Sheep:" + activePlayer.SheepQty + " Wood:" + activePlayer.WoodQty + " Clay:" + activePlayer.ClayQty + " Stone:" + activePlayer.StoneQty);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("The sum of resources is incorrect");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("You've just closed the trade with the bank.");
                    break;
                }
            }
        }

        public static int[] resourcesWanted(Player activePlayer)
        {
            int[] resourcesW = { 0, 0, 0, 0, 0 };

            Console.WriteLine("Resursele tale" + "(" + activePlayer.Name + ") inainte de trade: ");
            Console.WriteLine("Wheat:" + activePlayer.WheatQty + " Sheep:" + activePlayer.SheepQty + " Wood:" + activePlayer.WoodQty + " Clay:" + activePlayer.ClayQty + " Stone:" + activePlayer.StoneQty);
            Console.WriteLine("Introduceti resursele dorite:");

            int value;
            while (true)
            {
                Console.Write("Wheat-urile: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    resourcesW[0] = value;
                    if (resourcesW[0] < 0)
                        Console.WriteLine("Wheat-uri invalide.");
                    else
                        break;
                }
                else
                    Console.WriteLine("Wheat-uri invalide.");
            }

            while (true)
            {
                Console.Write("Sheep-uri: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    resourcesW[1] = value;
                    if (resourcesW[1] < 0)
                        Console.WriteLine("Sheep-uri invalide.");
                    else
                        break;
                }
                else
                    Console.WriteLine("Sheep-uri invalide.");
            }

            while (true)
            {
                Console.Write("Wood-uri: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    resourcesW[2] = value;
                    if (resourcesW[2] < 0)
                        Console.WriteLine("Wood-uri invalide.");
                    else
                        break;
                }
                else
                    Console.WriteLine("Wood-uri invalide.");
            }

            while (true)
            {
                Console.Write("Clay-uri: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    resourcesW[3] = value;
                    if (resourcesW[3] < 0)
                        Console.WriteLine("Clay-uri invalide.");
                    else
                        break;
                }
                else
                    Console.WriteLine("Clay-uri invalide.");
            }

            while (true)
            {
                Console.Write("Stone-uri: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    resourcesW[4] = value;
                    if (resourcesW[4] < 0)
                        Console.WriteLine("Stone-uri invalide.");
                    else
                        break;
                }
                else
                    Console.WriteLine("Stone-uri invalide.");
            }

            return resourcesW;
        }

        //functia prin care se citesc de la tastatura resursele oferite ; este returnat vectorul care contine valorile oferite 

        public static int[] resourcesGiven(Player activePlayer)
        {
            int[] resourcesG = { 0, 0, 0, 0, 0 };
            int value;

            Console.WriteLine("Wheat:" + activePlayer.WheatQty + " Sheep:" + activePlayer.SheepQty + " Wood:" + activePlayer.WoodQty + " Clay:" + activePlayer.ClayQty + " Stone:" + activePlayer.StoneQty);
            Console.WriteLine("\nIntroduceti resursele oferite:");

            while (true)
            {
                Console.Write("Wheat-urile: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    resourcesG[0] = value;
                    if (activePlayer.WheatQty < resourcesG[0])
                        Console.WriteLine("Nu ai suficiente Wheat-uri.");
                    else if (resourcesG[0] < 0)
                        Console.WriteLine("Wheat-uri invalide.");
                    else break;
                }
                else
                    Console.WriteLine("Wheat-uri invalide.");
            }

            while (true)
            {
                Console.Write("Sheep-uri: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    resourcesG[1] = value;
                    if (activePlayer.SheepQty < resourcesG[1])
                        Console.WriteLine("Nu ai suficiente Sheep-uri.");
                    else if (resourcesG[1] < 0)
                        Console.WriteLine("Sheep-uri invalide.");
                    else break;
                }
                else
                    Console.WriteLine("Sheep-uri invalide.");
            }

            while (true)
            {
                Console.Write("Wood-urile: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    resourcesG[2] = value;
                    if (activePlayer.WoodQty < resourcesG[2])
                        Console.WriteLine("Nu ai suficiente Wood-uri.");
                    else if (resourcesG[2] < 0)
                        Console.WriteLine("Wood-uri invalide.");
                    else break;
                }
                else
                    Console.WriteLine("Wood-uri invalide.");
            }

            while (true)
            {
                Console.Write("Clay-urile: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    resourcesG[3] = value;
                    if (activePlayer.ClayQty < resourcesG[3])
                        Console.WriteLine("Nu ai suficiente Clay-uri.");
                    else if (resourcesG[3] < 0)
                        Console.WriteLine("Clay-uri invalide.");
                    else break;
                }
                else
                    Console.WriteLine("Clay-uri invalide.");
            }

            while (true)
            {
                Console.Write("Stone-uri: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    resourcesG[4] = value;
                    if (activePlayer.StoneQty < resourcesG[4])
                        Console.WriteLine("Nu ai suficiente Stone-uri.");
                    else if (resourcesG[4] < 0)
                        Console.WriteLine("Stone-uri invalide.");
                    else break;
                }
                else
                    Console.WriteLine("Stone-uri invalide.");
            }

            return resourcesG;
        }
    }
}

/*
 * PROGRAM.cs
 * 
 * 
 * 
 * 
 * 
                                    case 4:
                                    {
                                        foreach(Player p in playerOrder)
                                        {
                                            if (p == currentPlayer)
                                                continue;
                                            Console.WriteLine("Resursele lui " + p.Name + " inainte de trade:");
                                            Console.WriteLine("Wheat:" + p.WheatQty + " Sheep:" + p.SheepQty + " Wood:" + p.WoodQty + " Clay:" + p.ClayQty + " Stone:" + p.StoneQty + "\n");
                                        }
                                        Trade.initTradeWithPlayers(currentPlayer, gs);
                                        break;
                                    }
                                    case 5:
                                    {
                                        Trade.initTradeWithBank(currentPlayer, gs.Map);
                                        break;
                                    }
 *
 * 
 * 
 * 
 * 
 */
