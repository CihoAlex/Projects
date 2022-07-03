using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using Catan;

/*
 * To run the tests add a new project to the existing project(the project that contains the game)
 * add this class to the project and create a depedency between the 2 projects
 * to run the tests go to "Tests" in the upper toolbar and click Run All Tests
 */
namespace UnitTestProject1Tests
{
    [TestClass]
    public class BuildTest
    {
        GameState gs;
        public void init()
        {
            List<Player> players = new List<Player>() { new Player("player1"), new Player("player2"), new Player("player3") };
            gs = new GameState(3, players, 1, 10, 1);
            // setez id la jucatori:
            int idp = 1;

            foreach (Player p in players)
            {
                p.Id = idp++;
                if (p.Name == "player1")
                {

                    gs.Map.Nodes[11].HasPlayer = true;
                    gs.Map.Nodes[11].HasSettlement = true;
                    gs.Map.Nodes[11].SettlementType = Settlements.Village;
                    gs.Map.Nodes[11].PlayerSettled = p;
                    Road r = new Road();
                    r.Node1 = gs.Map.Nodes[11];
                    r.Node2 = gs.Map.Nodes[12];
                    r.Owner = p;
                    gs.Map.PlacedRoads.Add(r);
                }
            }
            players[1].WheatQty = 5;
            players[1].SheepQty = 5;
            players[1].ClayQty = 5;
            players[1].StoneQty = 5;
            players[1].WoodQty = 5;

            Road rr = new Road();
            rr.Node1 = gs.Map.Nodes[21];
            rr.Node2 = gs.Map.Nodes[22];
            rr.Owner = players[1];
            gs.Map.PlacedRoads.Add(rr);

            Road rrr = new Road();
            rrr.Node1 = gs.Map.Nodes[14];
            rrr.Node2 = gs.Map.Nodes[15];
            rrr.Owner = players[2];
            gs.Map.PlacedRoads.Add(rrr);
        }

        [TestMethod]
        public void Build_Settlement_When_No_Settlement_Nearby_No_Cost__Works_without_built_road()
        {
            init();
            Assert.IsTrue(Build.BuildSettlement(gs.Players[1], gs.Map.Nodes[21], gs.Map, true));
        }
        [TestMethod]
        public void Build_Settlement_When_Settlement_Nearby_No_Cost()
        {
            init();
            Assert.IsFalse(Build.BuildSettlement(gs.Players[1], gs.Map.Nodes[12], gs.Map, true));
        }
        [TestMethod]
        public void Build_Settlement_When_Settlement_Nearby_And_Has_Road_And_Player_Has_Resources_Necesary_With_Cost()
        {
            init();
            Assert.IsFalse(Build.BuildSettlement(gs.Players[1], gs.Map.Nodes[12], gs.Map, false));
        }
        [TestMethod]
        public void Build_Settlement_When_No_Settlement_Nearby_And_Has_Road_And_Player_Has_Resources_Necesary_With_Cost()
        {
            init();
            Assert.IsTrue(Build.BuildSettlement(gs.Players[1], gs.Map.Nodes[21], gs.Map, false));
        }
        [TestMethod]
        public void Build_Settlement_When_No_Settlement_Nearby_And_Has_Road_And_Player_Doesnt_Have_Resources_Necesary_With_Cost()
        {
            init();
            Assert.IsFalse(Build.BuildSettlement(gs.Players[2], gs.Map.Nodes[14], gs.Map, false));
        }
        [TestMethod]
        public void Build_Settlement_When_No_Settlement_Nearby_And_Has_No_Road_And_Player_Doesnt_Have_Resources_Necesary_With_Cost()
        {
            init();
            Assert.IsFalse(Build.BuildSettlement(gs.Players[2], gs.Map.Nodes[25], gs.Map, false));
        }
        [TestMethod]
        public void Build_Settlement_When_No_Settlement_Nearby_And_Has_No_Road_And_Player_Has_Resources_Necesary_With_Cost()
        {
            init();
            Assert.IsFalse(Build.BuildSettlement(gs.Players[1], gs.Map.Nodes[32], gs.Map, false));
        }
        [TestMethod]
        public void Build_Road_No_Cost__Works_without_having_connected_road_or_settlement()
        {
            init();
            Assert.IsTrue(Build.BuildRoad(gs.Players[0], gs.Map.Nodes[45], gs.Map.Nodes[46], gs.Map, true));
        }
        [TestMethod]
        public void Build_Road_Over_Other_Road_No_Cost()
        {
            init();
            Assert.IsFalse(Build.BuildRoad(gs.Players[0], gs.Map.Nodes[11], gs.Map.Nodes[12], gs.Map, true));
        }
        [TestMethod]
        public void Build_Road_When_Player_Has_Resources_No_Link_Road_With_Cost()
        {
            init();
            Assert.IsFalse(Build.BuildRoad(gs.Players[1], gs.Map.Nodes[45], gs.Map.Nodes[46], gs.Map, false));
        }
        [TestMethod]
        public void Build_Road_When_Player_Has_Resources_Link_Road_Exists_With_Cost()
        {
            init();
            Assert.IsTrue(Build.BuildRoad(gs.Players[1], gs.Map.Nodes[20], gs.Map.Nodes[21], gs.Map, false));
        }
        [TestMethod]
        public void Build_Road_When_Player_Doesnt_Have_Resources_Link_Road_Exists_With_Cost()
        {
            init();
            Assert.IsFalse(Build.BuildRoad(gs.Players[0], gs.Map.Nodes[10], gs.Map.Nodes[11], gs.Map, false));
        }
        [TestMethod]
        public void Build_Road_When_Player_Doesnt_Have_Resources_No_Link_Road_With_Cost()
        {
            init();
            Assert.IsFalse(Build.BuildRoad(gs.Players[0], gs.Map.Nodes[32], gs.Map.Nodes[33], gs.Map, false));
        }
    }

    [TestClass]
    public class CardsTest
    {

        GameState gs;
        public void initcards()
        {
            List<Player> players = new List<Player>() { new Player("player1"), new Player("player2"), new Player("player3"), new Player("player4") };
            gs = new GameState(4, players, 1, 10, 1);

            players[0].WheatQty = 1;
            players[0].SheepQty = 1;
            players[0].ClayQty = 0;
            players[0].StoneQty = 1;
            players[0].WoodQty = 0;
            players[0].KnightCardsLeft = 0;
            players[0].RoadBuildingCardsLeft = 0;
            players[0].MonopolyCardsLeft = 0;
            players[0].YearOfPlentyCardsLeft = 0;
            players[0].VictoryPointCardsLeft = 0;
            players[0].unusedKnightCardsLeft = 0;
            players[0].unusedRoadBuildingCardsLeft = 0;
            players[0].unusedYearOfPlentyCardsLeft = 0;
            players[0].unusedMonopolyCardsLeft = 0;


            players[1].WheatQty = 0;
            players[1].SheepQty = 1;
            players[1].ClayQty = 0;
            players[1].StoneQty = 1;
            players[1].WoodQty = 0;
            players[1].VictoryPointCardsLeft = 1;
            players[1].HiddenPoints = 0;
            players[1].MonopolyCardsLeft = 1;
            players[1].YearOfPlentyCardsLeft = 1;
            players[1].RoadBuildingCardsLeft = 1;
            players[1].KnightCardsLeft = 1;
            players[1].KnightCardsUsed = 0;


            players[2].WheatQty = 1;
            players[2].SheepQty = 0;
            players[2].ClayQty = 0;
            players[2].StoneQty = 1;
            players[2].WoodQty = 0;
            players[2].KnightCardsUsed = 4;
            players[2].HasLargestArmy = false;

            players[3].WheatQty = 1;
            players[3].SheepQty = 1;
            players[3].ClayQty = 0;
            players[3].StoneQty = 0;
            players[3].WoodQty = 0;
        }

        [TestMethod]
        public void buyDevCard_all_resources()
        {
            initcards();
            Assert.IsTrue(CardsForTest.buyDevCard(gs, gs.Players[0], 1));
            Assert.AreEqual(gs.Players[0].WheatQty, 0);
            Assert.AreEqual(gs.Players[0].SheepQty, 0);
            Assert.AreEqual(gs.Players[0].StoneQty, 0);
        }
        [TestMethod]
        public void buyDevCard_no_Wheat()
        {
            initcards();
            Assert.IsFalse(CardsForTest.buyDevCard(gs, gs.Players[1], 1));
        }
        [TestMethod]
        public void buyDevCard_no_Sheep()
        {
            initcards();
            Assert.IsFalse(CardsForTest.buyDevCard(gs, gs.Players[2], 1));
        }
        [TestMethod]
        public void buyDevCard_no_Stone()
        {
            initcards();
            Assert.IsFalse(CardsForTest.buyDevCard(gs, gs.Players[3], 1));
        }

        [TestMethod]
        public void UseKnightCard_no_card()
        {
            initcards();
            Assert.IsFalse(CardsForTest.UseKnightCard(gs.Players, gs.Players[0], gs));
        }

        [TestMethod]
        public void UseVictoryCard_no_card()
        {
            initcards();
            Assert.IsFalse(CardsForTest.UseVictoryCard(gs.Players[0]));
        }
        [TestMethod]
        public void UseVictoryCard()
        {
            initcards();
            Assert.IsTrue(CardsForTest.UseVictoryCard(gs.Players[1]));
            Assert.AreEqual(gs.Players[1].HiddenPoints, 1);
        }

        [TestMethod]
        public void UseMonopolyCard_no_card()
        {
            initcards();
            Assert.IsFalse(CardsForTest.UseMonopolyCard(gs.Players[0], gs.Players, Resources.Wheat));
        }

        [TestMethod]
        public void UseMonopolyCard()
        {
            initcards();
            Assert.IsTrue(CardsForTest.UseMonopolyCard(gs.Players[1], gs.Players, Resources.Wheat));
            Assert.AreEqual(gs.Players[1].MonopolyCardsLeft, 0);
            Assert.AreEqual(gs.Players[1].WheatQty, 3); //testez doar cazul in care alege Wheat
            Assert.AreEqual(gs.Players[0].WheatQty, 0);
            Assert.AreEqual(gs.Players[2].WheatQty, 0);
            Assert.AreEqual(gs.Players[3].WheatQty, 0); //ceilalti playeri au ramas cu 0 resurce
        }

        [TestMethod]
        public void UseYearOfPlentyCard_no_card()
        {
            initcards();
            Assert.IsFalse(CardsForTest.UseYearOfPlentyCard(gs.Players[0], Resources.Wheat));
        }
        [TestMethod]
        public void UseYearOfPlentyCard()
        {
            initcards();
            Assert.IsTrue(CardsForTest.UseYearOfPlentyCard(gs.Players[1], Resources.Wheat));
            Assert.AreEqual(gs.Players[1].WheatQty, 2);  //testez doar pe cazul Wheat  
        }

        [TestMethod]
        public void UseRoadBuildingCard_no_card()
        {
            initcards();
            Assert.IsFalse(CardsForTest.UseRoadBuildingCard(gs.Players[0], gs.Map.Nodes[1], gs.Map.Nodes[2], gs.Map));
        }

        [TestMethod]
        public void UseRoadBuildingCard()
        {
            initcards();
            Assert.IsTrue(CardsForTest.UseRoadBuildingCard(gs.Players[1], gs.Map.Nodes[1], gs.Map.Nodes[2], gs.Map));
            Assert.AreEqual(gs.Players[1].RoadBuildingCardsLeft, 0);
            Assert.IsTrue(Build.BuildRoad(gs.Players[1], gs.Map.Nodes[1], gs.Map.Nodes[2], gs.Map, true));
        }

        [TestMethod]
        public void initializare_corecta()
        {
            initcards();
            CardsForTest.initializeComp(1);
            Assert.AreEqual(CardsForTest.kcComp, 15);
            Assert.AreEqual(CardsForTest.vpComp1, 14);
            Assert.AreEqual(CardsForTest.vpComp2, 20);
            Assert.AreEqual(CardsForTest.rbComp1, 19);
            Assert.AreEqual(CardsForTest.rbComp2, 22);
            Assert.AreEqual(CardsForTest.ypComp1, 21);
            Assert.AreEqual(CardsForTest.ypComp2, 24);
            Assert.AreEqual(CardsForTest.mcComp1, 23);
            Assert.AreEqual(CardsForTest.mcComp2, 26);
        }

        [TestMethod]
        public void giveDevCard_knightCard()
        {
            initcards();
            CardsForTest.initializeComp(1);
            CardsForTest.giveDevCard(gs, gs.Players[0], 1, 1);
            Assert.AreEqual(gs.Players[0].unusedKnightCardsLeft, 1);
            Assert.AreEqual(CardsForTest.kcComp, 14);
            Assert.AreEqual(CardsForTest.vpComp1, 13);
            Assert.AreEqual(CardsForTest.vpComp2, 19);
            Assert.AreEqual(CardsForTest.rbComp1, 18);
            Assert.AreEqual(CardsForTest.rbComp2, 21);
            Assert.AreEqual(CardsForTest.ypComp1, 20);
            Assert.AreEqual(CardsForTest.ypComp2, 23);
            Assert.AreEqual(CardsForTest.mcComp1, 22);
            Assert.AreEqual(CardsForTest.mcComp2, 25);
            Assert.AreEqual(gs.KnightCardsLeft, 13);
            Assert.AreEqual(gs.CardsLeft, 24);
        }

        [TestMethod]
        public void giveDevCard_victorycard()
        {
            initcards();
            CardsForTest.initializeComp(1);
            CardsForTest.giveDevCard(gs, gs.Players[0], 1, 15);
            Assert.AreEqual(gs.Players[0].VictoryPointCardsLeft, 1);
            Assert.IsTrue(CardsForTest.UseVictoryCard(gs.Players[0]));
            Assert.AreEqual(CardsForTest.kcComp, 15);
            Assert.AreEqual(CardsForTest.vpComp1, 14);
            Assert.AreEqual(CardsForTest.vpComp2, 19);
            Assert.AreEqual(CardsForTest.rbComp1, 18);
            Assert.AreEqual(CardsForTest.rbComp2, 21);
            Assert.AreEqual(CardsForTest.ypComp1, 20);
            Assert.AreEqual(CardsForTest.ypComp2, 23);
            Assert.AreEqual(CardsForTest.mcComp1, 22);
            Assert.AreEqual(CardsForTest.mcComp2, 25);
            Assert.AreEqual(gs.VictoryPointCardsLeft, 4);
            Assert.AreEqual(gs.CardsLeft, 24);
        }
        [TestMethod]
        public void giveDevCard_roadCard()
        {
            initcards();
            CardsForTest.initializeComp(1);
            CardsForTest.giveDevCard(gs, gs.Players[0], 1, 20);
            Assert.AreEqual(gs.Players[0].unusedRoadBuildingCardsLeft, 1);
            Assert.AreEqual(CardsForTest.kcComp, 15);
            Assert.AreEqual(CardsForTest.vpComp1, 14);
            Assert.AreEqual(CardsForTest.vpComp2, 20);
            Assert.AreEqual(CardsForTest.rbComp1, 19);
            Assert.AreEqual(CardsForTest.rbComp2, 21);
            Assert.AreEqual(CardsForTest.ypComp1, 20);
            Assert.AreEqual(CardsForTest.ypComp2, 23);
            Assert.AreEqual(CardsForTest.mcComp1, 22);
            Assert.AreEqual(CardsForTest.mcComp2, 25);
            Assert.AreEqual(gs.RoadBuildingCardsLeft, 1);
            Assert.AreEqual(gs.CardsLeft, 24);
        }

        [TestMethod]
        public void giveDevCard_yearcard()
        {
            initcards();
            CardsForTest.initializeComp(1);
            CardsForTest.giveDevCard(gs, gs.Players[0], 1, 22);
            Assert.AreEqual(gs.Players[0].unusedYearOfPlentyCardsLeft, 1);
            Assert.AreEqual(CardsForTest.kcComp, 15);
            Assert.AreEqual(CardsForTest.vpComp1, 14);
            Assert.AreEqual(CardsForTest.vpComp2, 20);
            Assert.AreEqual(CardsForTest.rbComp1, 19);
            Assert.AreEqual(CardsForTest.rbComp2, 22);
            Assert.AreEqual(CardsForTest.ypComp1, 21);
            Assert.AreEqual(CardsForTest.ypComp2, 23);
            Assert.AreEqual(CardsForTest.mcComp1, 22);
            Assert.AreEqual(CardsForTest.mcComp2, 25);
            Assert.AreEqual(gs.YearOfPlentyCardsLeft, 1);
            Assert.AreEqual(gs.CardsLeft, 24);
        }

        [TestMethod]
        public void giveDevCard_monopolycard()
        {
            initcards();
            CardsForTest.initializeComp(1);
            CardsForTest.giveDevCard(gs, gs.Players[0], 1, 24);
            Assert.AreEqual(gs.Players[0].unusedMonopolyCardsLeft, 1);
            Assert.AreEqual(CardsForTest.kcComp, 15);
            Assert.AreEqual(CardsForTest.vpComp1, 14);
            Assert.AreEqual(CardsForTest.vpComp2, 20);
            Assert.AreEqual(CardsForTest.rbComp1, 19);
            Assert.AreEqual(CardsForTest.rbComp2, 22);
            Assert.AreEqual(CardsForTest.ypComp1, 21);
            Assert.AreEqual(CardsForTest.ypComp2, 24);
            Assert.AreEqual(CardsForTest.mcComp1, 23);
            Assert.AreEqual(CardsForTest.mcComp2, 25);
            Assert.AreEqual(gs.MonopolyCardsLeft, 1);
            Assert.AreEqual(gs.CardsLeft, 24);
        }

        [TestMethod]
        public void useKnightCard()
        {
            initcards();
            Assert.IsFalse(CardsForTest.UseKnightCard(gs.Players, gs.Players[0], gs));

            CardsForTest.UseKnightCard(gs.Players, gs.Players[1], gs);
            Assert.AreEqual(gs.Players[1].KnightCardsUsed, 1);

            CardsForTest.UseKnightCard(gs.Players, gs.Players[2], gs);
            // Assert.IsTrue(gs.Players[2].HasLargestArmy);
            //Assert.AreEqual(gs.Players[0].LargestArmyPoints, 2);

        }
    }

    [TestClass]
    public class NodeTest
    {

        GameState gs;
        public void init()
        {
            List<Player> players = new List<Player>() { new Player("player1"), new Player("player2"), new Player("player3"), new Player("player4") };
            gs = new GameState(4, players, 1, 10, 1);

        }

        [TestMethod]
        public void SettlePlayer()
        {
            init();
            gs.Map.Nodes[10].SettlePlayer(gs.Players[0], Settlements.Village);
            Assert.IsTrue(gs.Map.Nodes[10].hasPlayer);
            Assert.AreEqual(gs.Players[0], gs.Map.Nodes[10].PlayerSettled);
            Assert.AreEqual(Settlements.Village, gs.Map.Nodes[10].SettlementType);
            gs.Map.Nodes[10].SettlePlayer(gs.Players[0], Settlements.City);
            Assert.AreEqual(Settlements.City, gs.Map.Nodes[10].SettlementType);
        }
    }


    [TestClass]
    public class PlayerTest
    {
        GameState gs;
        public void init()
        {
            List<Player> players = new List<Player>() { new Player("player1"), new Player("player2"), new Player("player3"), new Player("player4") };
            gs = new GameState(4, players, 1, 10, 1);
            players[1].WheatQty = 1;
            players[1].SheepQty = 1;
            players[1].ClayQty = 1;
            players[1].StoneQty = 1;
            players[1].WoodQty = 1;

        }

        [TestMethod]
        public void Constructor()
        {
            init();
            Assert.AreEqual(gs.Players[0].Points, 0);
            Assert.AreEqual(gs.Players[0].HiddenPoints, 0);

            Assert.AreEqual(gs.Players[0].WheatQty, 0);
            Assert.AreEqual(gs.Players[0].SheepQty, 0);
            Assert.AreEqual(gs.Players[0].ClayQty, 0);
            Assert.AreEqual(gs.Players[0].StoneQty, 0);
            Assert.AreEqual(gs.Players[0].WoodQty, 0);

            Assert.AreEqual(gs.Players[0].VillagesLeft, 5);
            Assert.AreEqual(gs.Players[0].CitiesLeft, 5);
            Assert.AreEqual(gs.Players[0].RoadsLeft, 15);

            Assert.AreEqual(gs.Players[0].KnightCardsLeft, 0);
            Assert.AreEqual(gs.Players[0].VictoryPointCardsLeft, 0);
            Assert.AreEqual(gs.Players[0].RoadBuildingCardsLeft, 0);
            Assert.AreEqual(gs.Players[0].YearOfPlentyCardsLeft, 0);
            Assert.AreEqual(gs.Players[0].MonopolyCardsLeft, 0);

            Assert.AreEqual(gs.Players[0].unusedKnightCardsLeft, 0);
            Assert.AreEqual(gs.Players[0].unusedMonopolyCardsLeft, 0);
            Assert.AreEqual(gs.Players[0].unusedRoadBuildingCardsLeft, 0);
            Assert.AreEqual(gs.Players[0].unusedYearOfPlentyCardsLeft, 0);
            Assert.AreEqual(gs.Players[0].unusedvictoryPointCardsLeft, 0);

            Assert.AreEqual(gs.Players[0].KnightCardsUsed, 0);
            Assert.AreEqual(gs.Players[0].HasLongestRoad, false);
            Assert.AreEqual(gs.Players[0].HasLargestArmy, false);
        }

        [TestMethod]
        public void DiscardTest()
        {
            init();
            List<Resources> resurse = new List<Resources>() { Resources.Sheep, Resources.Clay, Resources.Stone, Resources.Wheat, Resources.Wood };
            gs.Players[1].Discard(resurse);
            Assert.AreEqual(gs.Players[1].ClayQty, 0);
            Assert.AreEqual(gs.Players[1].WheatQty, 0);
            Assert.AreEqual(gs.Players[1].SheepQty, 0);
            Assert.AreEqual(gs.Players[1].StoneQty, 0);
            Assert.AreEqual(gs.Players[1].WoodQty, 0);

        }
    }

    [TestClass]
    public class AllPaths
    {

        [TestMethod]
        public void Constructor()
        {
            Graph a = new Graph();
            Assert.AreEqual(a.Vertices, 80);
        }

        [TestMethod]
        public void addEdgeTest()
        {
            Graph a = new Graph();
            a.addEdge(5, 4);
            a.addEdge(5, 3);
            List<int> test = new List<int>() { 4, 3 };
            CollectionAssert.AreEqual(a.AdjList[5], test);
        }

        [TestMethod]
        public void fillIntersectionstest()
        {
            Graph a = new Graph();
            a.initAdjList();
            a.addEdge(5, 4);
            a.addEdge(5, 3);
            a.addEdge(5, 2);
            a.addEdge(10, 1);
            a.addEdge(10, 6);
            a.addEdge(10, 7);

            a.fillIntersections(a.adjList);
            List<int> test = new List<int>() { 5, 10 };
            CollectionAssert.AreEqual(a.intersectList, test);
        }
    }
}