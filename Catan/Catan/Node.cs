using System;
using System.Collections.Generic;
using System.Text;

namespace Catan
{
    public class Node
    {
        private int id;
        public bool hasPlayer = false;
        private Player playerSettled = new Player();
        private bool hasSettlement = false;
        private Settlements settlementType = new Settlements();
        private bool hasPort = false;
        private Ports portType = new Ports();
        private List<int> hexNeighborIds;
        private List<int> nodeNeighborIds;

        // CONSTRUCTOR
        public Node(int id) { this.id = id; }//completat cu restul atributelor default (gen hassettlement false etc)} 
        public Node() { }

        // METHODS
        public override string ToString()
        {
            StringBuilder final = new StringBuilder();
            final.Append("Node ID: " + id.ToString());

            final.Append(", HasPort: " + HasPort);
            if (HasPort)
                final.Append(", Port: " + PortType);
            //final.Append(", HasPlayer: " + HasPlayer);
            //final.Append(", HasSettlement: " + HasSettlement);

            if (hasPlayer == true)
                final.Append(", Settled by player: " + playerSettled.Name + " with a " + settlementType.ToString());
            return final.ToString();
        }

        public void SettlePlayer(Player player, Settlements type)
        {
            if (this.hasPlayer == false && type == Settlements.Village)
            {
                this.hasPlayer = true;
                this.playerSettled = player;
                this.settlementType = type;
            }
            else if (this.hasPlayer == true && player == this.playerSettled && type == Settlements.City)
            {
                this.settlementType = type;
            }
        }

        // GETTERS AND SETTERS
        public int Id { get { return this.id; } set { this.id = value; } }
        public bool HasPlayer { get { return this.hasPlayer; } set { this.hasPlayer = value; } }
        public Player PlayerSettled { get { return this.playerSettled; } set { this.playerSettled = value; } }
        public bool HasSettlement { get { return this.hasSettlement; } set { this.hasSettlement = value; } }
        public Settlements SettlementType { get { return this.settlementType; } set { this.settlementType = value; } }
        public bool HasPort { get { return hasPort; } set { hasPort = value; } }
        public Ports PortType { get { return this.portType; } set { this.portType = value; } }
        public List<int> HexNeighborIds { get { return this.hexNeighborIds; } set { this.hexNeighborIds = value; } }
        public List<int> NodeNeighborIds { get { return this.nodeNeighborIds; } set { this.nodeNeighborIds = value; } }
    }
}