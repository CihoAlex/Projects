using System;
using System.Collections.Generic;
using System.Text;

namespace Catan
{
    public class Road
    {
        private Node node1;
        private Node node2;
        private Player owner = new Player();

        // GETTERS AND SETTERS
        public Node Node1 { get { return this.node1; } set { this.node1 = value; } }
        public Node Node2 { get { return this.node2; } set { this.node2 = value; } }
        public Player Owner { get { return this.owner; } set { this.owner = value; } }

    }
}