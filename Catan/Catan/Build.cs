using Catan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    /*
     * in functie de parametrul ignoreCost se decide tura:
     *** daca ignoreCost este pe true (ceea ce se traduce prin: plasez settlement fara sa consum resurse)
     * inseamna ca suntem in primele 2 runde si am voie sa plasez oriunde vreau pe harta
     *** daca ignoreCost este pe false (ceea ce se traduce prin: plasez settlement dar consum din resurse)
     * inseamna ca nu ma aflu in primele 2 runde si sunt obligat sa il leg de un drum de al meu
     */
    public static class Build
    {
        public static bool BuildRoad(Player p, Node n1, Node n2, Map m, bool ignoreCost)
        {
            if (!ignoreCost)
            {
                // daca nu avem resursele necesare iesim din functia de build
                if (p.WoodQty < 1 || p.ClayQty < 1)
                {
                    Console.WriteLine("Nu avem resursele necesare.");
                    return false;
                }
            }

            // daca n1 sau n2 sunt capete de drumuri care apartin playerului p-> vom construi drum
            if ((!n1.HasSettlement && !n2.HasSettlement && ignoreCost) || validateNodes(n1, n2, m, p))
            {
                // actualizari in player resources
                if (!ignoreCost)
                {
                    p.WoodQty--;
                    p.ClayQty--;
                }

                // push la road in lista
                Road r = new Road();
                r.Node1 = n1;
                r.Node2 = n2;
                r.Owner = p;
                m.PlacedRoads.Add(r);
                Console.WriteLine("Am construit drum de la " + n1.Id + " la " + n2.Id);
                return true;
            }
            else
            {
                Console.WriteLine("Conditiile pentru crearea drumului nu sunt indeplinite");
                return false;
            }
        }


        public static int longestRoad(List<Road> roads, GameState g, Map m)
        {
            int longest = 0, playerRoadsLength = 0;
            List<Player> players = g.Players;
            foreach (Player p in players)
            {
                playerRoadsLength = m.PlacedRoads.Where(road => road.Owner == p).Count();
                if (playerRoadsLength > longest)
                    longest = playerRoadsLength;
            }
            // Console.WriteLine("playerul cu cel mai lung drum este "+ m.R)
            return longest;
        }

        private static bool validateNodes(Node n1, Node n2, Map m, Player p)
        {
            bool nodesAreAdjacent = false;
            //List<Node> nodes = m.NodeNeighborsOfNodes[n1.Id];
            List<int> nodesIds = n1.NodeNeighborIds;

            foreach (int id in nodesIds)
            {
                if (id == n2.Id)
                {
                    nodesAreAdjacent = true;
                    break;
                }
            }

            bool hasNodeNeighbor = false;
            bool hasAlreadyRoad = false;

            // verific odata pt n1
            List<Road> roadList = m.PlacedRoads.Where(road => road.Owner == p).ToList();
            foreach (Road rode in roadList) // drumurile 
            {
                foreach (int id in nodesIds) // noduri vecine
                {
                    if ((rode.Node1 == n1 && rode.Node2.Id == id) || (rode.Node2 == n1 && rode.Node1.Id == id))
                    {
                        hasNodeNeighbor = true;
                    }
                }

                if ((rode.Node1 == n1 && rode.Node2 == n2) || (rode.Node1 == n2 && rode.Node2 == n1))
                    hasAlreadyRoad = true;
            }

            // verific odata pe n2
            nodesIds = n2.NodeNeighborIds;
            foreach (Road rode in roadList) // drumurile 
            {
                foreach (int id in nodesIds) // noduri vecine
                {
                    if ((rode.Node1 == n2 && rode.Node2.Id == id) || (rode.Node2 == n2 && rode.Node1.Id == id))
                    {
                        hasNodeNeighbor = true;
                    }
                }

                if ((rode.Node1 == n1 && rode.Node2 == n2) || (rode.Node1 == n2 && rode.Node2 == n1))
                    hasAlreadyRoad = true;
            }

            if (nodesAreAdjacent)
            {
                if (hasAlreadyRoad)
                {
                    Console.WriteLine("Jucatorul " + p.Name + "(" + p.Id + ") are deja drum intre " + n1.Id + " si " + n2.Id + ".");
                    return false;
                }
                if (hasNodeNeighbor || (!n2.HasSettlement && n1.PlayerSettled == p) || (!n1.HasSettlement && n2.PlayerSettled == p))
                    return true;
            }
            return false;
        }

        /*
         * cand apelam BuildSettlement, stim deja ca avem un road plasat(primele 2 runde)
         * intai drumul si apoi setllementul(primele 2 runde)
         */

        public static bool BuildSettlement(Player p, Node n, Map m, bool ignoreCost)
        {
            // preluam lista de drumuri ce au ca owner playerul p (cel dat ca parametru)
            List<Road> roadList = m.PlacedRoads.Where(road => road.Owner == p).ToList();

            // Preluam lista cu noduri vecine ale nodului n(dat ca parametru)
            //List<Node> nodeList = m.NodeNeighborsOfNodes[n.Id];
            List<int> nodeIdList = n.NodeNeighborIds;

            /*
             * verificam daca nodul are deja un settlement sau nu
             daca are un sat ii facem upgrade la city
             daca nu are settlement punem un village
             */

            if (n.HasSettlement)    // update
            {
                // Daca settlement-ul de pe nod apartine playerului dat ca parametru ->daca este sat facem upgrade la oras
                // Daca settlement-ul de pe nod nu apartine playerului dat ca parametru atunci acesta nu are drept sa modifice nimic
                if (p.Id == n.PlayerSettled.Id)
                {
                    if (n.SettlementType == Settlements.Village && !ignoreCost)
                    {
                        if (!ignoreCost)
                        {
                            if (p.StoneQty < 2 || p.WheatQty < 3)
                            {
                                Console.WriteLine("Nu avem resursele necesare ");
                                return false;
                            }

                            p.StoneQty -= 2;
                            p.WheatQty -= 3;
                        }

                        // facem upgrade de la sat la oras
                        // actualizam datele despre nod
                        p.Points++;
                        n.SettlementType = Settlements.City;
                        n.PlayerSettled = p;

                        Console.WriteLine("S-a construit oras peste satul de pe nodul " + n.Id);
                        return true;
                    }
                    else if (ignoreCost)
                    {
                        Console.WriteLine("Exista deja asezare, nu se poate construi nimic peste in aceasta runda");
                        return false;
                    }
                    else if (n.SettlementType == Settlements.City)
                    {
                        Console.WriteLine("Exista deja oras, nu se poate construi nimic peste");
                        return false;
                    }
                }
            }
            else //daca nu exista settlement pe nodul ales
            {
                /*
                 * in cazul in care nu exista settlement-uri la un nod distanta 
                 * si nodul curent este legat de un drum al playerului
                 * vom construi sat;
                 */

                if ((hasNoNearbySettlement(nodeIdList, m) && ignoreCost) || (hasNoNearbySettlement(nodeIdList, m) && hasRoadWithSmth(nodeIdList, roadList, n)))
                {
                    if (!ignoreCost)
                    {
                        //verificam daca playerul are resursele necesare
                        if (p.ClayQty < 1 || p.WoodQty < 1 || p.SheepQty < 1 || p.WheatQty < 1)
                        {
                            Console.WriteLine("Nu avem resursele necesare.");
                            return false;
                        }

                        p.ClayQty--;
                        p.WoodQty--;
                        p.SheepQty--;
                        p.WheatQty--;
                    }

                    // construim sat
                    p.Points++;
                    n.HasPlayer = true;
                    n.HasSettlement = true;
                    n.SettlementType = Settlements.Village;
                    n.PlayerSettled = p;
                    Console.WriteLine("S-a construit sat pe nodul " + n.Id);
                    return true;
                    // stii deja daca nodul are port sau nu, deci nu trebuie actualizate variabilele legate de porturi
                }
                else
                {
                    Console.WriteLine("Nodurile vecine au deja settlement sau nu exista drum ");
                    return false;
                }
            }
            return false;
        }

        /*
         * functia verifica daca nodurile vecine din 
         * lista nodeList au deja un settlement
         */
        private static bool hasNoNearbySettlement(List<int> nodeIdList, Map m)
        {
            foreach (int id in nodeIdList)
            {
                if (m.getNodeById(id).HasSettlement)
                {
                    return false;
                }
            }
            return true;
        }

        // verificam daca avem road (care are ca owner playerul p) catre nodul pe care vrem sa punem settlement
        // roadList va fi lista de road-uri care au ca owner playerul p
        // nodeList va fi lista de noduri vecine ale nodului n
        private static bool hasRoadWithSmth(List<int> nodeIdList, List<Road> roadList, Node n)
        {
            foreach (Road rode in roadList) // drumurile 
            {
                foreach (int id in nodeIdList) // noduri vecine
                {
                    if (rode.Node1 == n && rode.Node2.Id == id || rode.Node2 == n && rode.Node1.Id == id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // verificam daca playerul are deja 2 drumuri construite oriunde pe harta
        // folosim aceasta functie pentru a ne da seama daca suntem in primele 2 ture sau nu
        public static bool has2BuiltRoads(List<Road> roads, Player p)
        {
            int nr = 0;
            foreach (Road road in roads)
            {
                if (road.Owner == p)
                    nr++;
            }
            return nr < 2 ? false : true;
        }
    }
}