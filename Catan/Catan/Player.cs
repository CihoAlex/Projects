using System;
using System.Collections.Generic;
using System.Text;

namespace Catan
{
    public class Player
    {
        private string name;
        private int id;
        private int points;
        private int hiddenPoints; //points without VictoryPointCards
        private int longestRoadPoints; //points from longest road
        private int largestArmyPoints; //points for largest army

        private int wheatQty;
        private int sheepQty;
        private int clayQty;
        private int stoneQty;
        private int woodQty;

        private int villagesLeft;
        private int citiesLeft;
        private int roadsLeft;

        private int knightCardsLeft;
        private int victoryPointCardsLeft;
        private int roadBuildingCardsLeft;
        private int yearOfPlentyCardsLeft;
        private int monopolyCardsLeft;

        private int unusedknightCardsLeft;
        public int unusedvictoryPointCardsLeft;
        private int unusedroadBuildingCardsLeft;
        private int unusedyearOfPlentyCardsLeft;
        private int unusedmonopolyCardsLeft;


        private int knightCardsUsed;
        private bool hasLongestRoad;
        private bool hasLargestArmy;

        private Permissions perm = new Permissions();

        // CONSTRUCTOR 

        public Player() { }

        public Player(string name)
        {
            this.name = name;
            this.points = 0;
            this.hiddenPoints = 0;

            this.wheatQty = 0;
            this.sheepQty = 0;
            this.clayQty = 0;
            this.stoneQty = 0;
            this.woodQty = 0;

            this.villagesLeft = 5;
            this.citiesLeft = 5;
            this.roadsLeft = 15;

            this.knightCardsLeft = 0;
            this.victoryPointCardsLeft = 0;
            this.roadBuildingCardsLeft = 0;
            this.yearOfPlentyCardsLeft = 0;
            this.monopolyCardsLeft = 0;

            this.unusedknightCardsLeft = 0;
            this.unusedvictoryPointCardsLeft = 0;
            this.unusedroadBuildingCardsLeft = 0;
            this.unusedyearOfPlentyCardsLeft = 0;
            this.unusedmonopolyCardsLeft = 0;

            this.knightCardsUsed = 0;
            this.hasLongestRoad = false;
            this.hasLargestArmy = false;


        }

        public override string ToString()
        {
            return "Player name: " + name;
        }

        public void PrintResources()
        {
            Console.WriteLine("Player has : \nWheat: " + wheatQty + "\nWood: " + woodQty + "\nStone: " + stoneQty + "\nClay: " + clayQty + "\nSheep: " + sheepQty);
        }

        public void PrintDevCards()
        {
            Console.WriteLine("Player " + this.Name + " has: \nKnight Cards: " + this.knightCardsLeft + "\nVictory Point Cards: " + this.victoryPointCardsLeft + "\nRoad Building Cards: " + this.roadBuildingCardsLeft + "\nYear of plenty cards: " + this.yearOfPlentyCardsLeft + "\nMonopoly Cards: " + this.monopolyCardsLeft);
        }

        public void Discard(List<Resources> resources)
        {
            foreach (Resources resource in resources)
            {
                switch (resource)
                {
                    case Resources.Clay:
                        clayQty--;
                        break;
                    case Resources.Sheep:
                        sheepQty--;
                        break;
                    case Resources.Stone:
                        stoneQty--;
                        break;
                    case Resources.Wheat:
                        wheatQty--;
                        break;
                    case Resources.Wood:
                        woodQty--;
                        break;
                }
            }
        }

        // GETTERS AND SETTERS
        public String Name { get { return this.name; } set { this.name = value; } }
        public int Id { get { return this.id; } set { this.id = value; } }
        public int Points { get { return this.points; } set { this.points = value; } }
        public int HiddenPoints { get { return this.hiddenPoints; } set { this.hiddenPoints = value; } }
        public int LongestRoadPoints { get { return this.longestRoadPoints; } set { this.longestRoadPoints = value; } }
        public int LargestArmyPoints { get { return this.largestArmyPoints; } set { this.largestArmyPoints = value; } }

        public int WheatQty { get { return this.wheatQty; } set { this.wheatQty = value; } }
        public int SheepQty { get { return this.sheepQty; } set { this.sheepQty = value; } }
        public int ClayQty { get { return this.clayQty; } set { this.clayQty = value; } }
        public int StoneQty { get { return this.stoneQty; } set { this.stoneQty = value; } }
        public int WoodQty { get { return this.woodQty; } set { this.woodQty = value; } }

        public int VillagesLeft { get { return this.villagesLeft; } set { this.villagesLeft = value; } }
        public int CitiesLeft { get { return this.citiesLeft; } set { this.citiesLeft = value; } }
        public int RoadsLeft { get { return this.roadsLeft; } set { this.roadsLeft = value; } }

        public int KnightCardsLeft { get { return this.knightCardsLeft; } set { this.knightCardsLeft = value; } }
        public int VictoryPointCardsLeft { get { return this.victoryPointCardsLeft; } set { this.victoryPointCardsLeft = value; } }
        public int RoadBuildingCardsLeft { get { return this.roadBuildingCardsLeft; } set { this.roadBuildingCardsLeft = value; } }
        public int YearOfPlentyCardsLeft { get { return this.yearOfPlentyCardsLeft; } set { this.yearOfPlentyCardsLeft = value; } }
        public int MonopolyCardsLeft { get { return this.monopolyCardsLeft; } set { this.monopolyCardsLeft = value; } }

        public int unusedKnightCardsLeft { get { return this.unusedknightCardsLeft; } set { this.unusedknightCardsLeft = value; } }
        public int unusedRoadBuildingCardsLeft { get { return this.unusedroadBuildingCardsLeft; } set { this.unusedroadBuildingCardsLeft = value; } }
        public int unusedYearOfPlentyCardsLeft { get { return this.unusedyearOfPlentyCardsLeft; } set { this.unusedyearOfPlentyCardsLeft = value; } }
        public int unusedMonopolyCardsLeft { get { return this.unusedmonopolyCardsLeft; } set { this.unusedmonopolyCardsLeft = value; } }

        public int KnightCardsUsed { get { return this.knightCardsUsed; } set { this.knightCardsUsed = value; } }
        public bool HasLongestRoad { get { return this.hasLongestRoad; } set { this.hasLongestRoad = value; } }
        public bool HasLargestArmy { get { return this.hasLargestArmy; } set { this.hasLargestArmy = value; } }

        public Permissions Perm { get { return this.perm; } set { this.perm = value; } }

    }
}