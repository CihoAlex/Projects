using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Catan
{
    public class Hex
    {
        private int id;
        private int number;
        private bool hasRobber = false;
        private int row = 0;
        private int posOnRow = 0;
        private Resources resource;
        private List<int> hexNeighborIds;
        private List<int> nodeNeighborIds;

        // CONSTRUCTOR
        public Hex(int id, Resources resource, int number)
        {
            this.id = id;
            this.resource = resource;
            this.number = number;
            this.hasRobber = false;
        }

        // CONSTRUCTOR
        public Hex(int id)
        {
            this.id = id;
        }

        // METHODS
        public void setDetails(Resources resource, int number)
        {
            this.resource = resource;
            this.number = number;
        }

        public override string ToString()
        {
            StringBuilder final = new StringBuilder();
            final.Append("Hex ID: " + id.ToString() + ", resource: " + Resource.ToString() + ", number: " + Number.ToString());
            final.Append(", HasRobber: " + HasRobber);

            return final.ToString();
        }

        public List<Node> SettledNeighborNodes(Map m)
        {
            return m.getNodeListByIdList(this.nodeNeighborIds.Where(id => m.getNodeById(id).HasPlayer == true).ToList());
        }

        // GETTERS AND SETTERS
        public int Id { get { return this.id; } set { this.id = value; } }
        public int Number { get { return this.number; } set { this.number = value; } }
        public bool HasRobber { get { return this.hasRobber; } set { this.hasRobber = value; } }
        public Resources Resource { get { return this.resource; } set { this.resource = value; } }
        public int Row { get { return row; } set { this.row = value; } }
        public int PosOnRow { get { return posOnRow; } set { this.posOnRow = value; } }
        public List<int> HexNeighborIds { get { return this.hexNeighborIds; } set { this.hexNeighborIds = value; } }
        public List<int> NodeNeighborIds { get { return this.nodeNeighborIds; } set { this.nodeNeighborIds = value; } }
    }
}