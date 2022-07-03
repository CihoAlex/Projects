using System;
using System.Collections.Generic;
using System.Text;

namespace Catan
{
    public class GameState
    {

        private Map map;
        private List<Player> players = new List<Player>();
        private int numberOfPlayers;
        private int gameType;
        private int winningPoints;

        private int knightCardsLeft;
        private int victoryPointCardsLeft;
        private int roadBuildingCardsLeft;
        private int yearOfPlentyCardsLeft;
        private int monopolyCardsLeft;
        private int cardsLeft;
        private int seed;

        // CONSTRUCTOR
        public GameState() { }

        public GameState(int numberOfPlayers, List<Player> players, int gameType, int winningPoints, int seed)
        {
            this.seed = seed;
            map = new Map(gameType, seed);

            if (gameType == 1)
            {
                this.knightCardsLeft = 14;
                this.victoryPointCardsLeft = 5;
                this.roadBuildingCardsLeft = 2;
                this.yearOfPlentyCardsLeft = 2;
                this.monopolyCardsLeft = 2;

            }
            else
            {
                this.knightCardsLeft = 20;
                this.victoryPointCardsLeft = 5;
                this.roadBuildingCardsLeft = 3;
                this.yearOfPlentyCardsLeft = 3;
                this.monopolyCardsLeft = 3;
            }

            cardsLeft = KnightCardsLeft + RoadBuildingCardsLeft + VictoryPointCardsLeft + YearOfPlentyCardsLeft + MonopolyCardsLeft;
            Cards.initializeComp(gameType);
            this.players = players;
            this.numberOfPlayers = numberOfPlayers;
            this.gameType = gameType;
            this.winningPoints = winningPoints;

        }

        // GETTERS AND SETTERS
        public Map Map { get { return this.map; } }
        public List<Player> Players { get { return this.players; } set { this.players = value; } }
        public int NumberOfPlayers { get { return this.numberOfPlayers; } set { this.numberOfPlayers = value; } }
        public int GameType { get { return this.gameType; } set { this.gameType = value; } }
        public int WinningPoints { get { return this.winningPoints; } set { this.winningPoints = value; } }

        public int KnightCardsLeft { get { return this.knightCardsLeft; } set { this.knightCardsLeft = value; } }
        public int VictoryPointCardsLeft { get { return this.victoryPointCardsLeft; } set { this.victoryPointCardsLeft = value; } }
        public int RoadBuildingCardsLeft { get { return this.roadBuildingCardsLeft; } set { this.roadBuildingCardsLeft = value; } }
        public int YearOfPlentyCardsLeft { get { return this.yearOfPlentyCardsLeft; } set { this.yearOfPlentyCardsLeft = value; } }
        public int MonopolyCardsLeft { get { return this.monopolyCardsLeft; } set { this.monopolyCardsLeft = value; } }
        public int CardsLeft { get { return this.cardsLeft; } set { this.cardsLeft = value; } }
        public int Seed { get { return seed; } }
    }
}