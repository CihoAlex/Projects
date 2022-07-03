using Catan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catan
{
    public class Permissions
    {

        private bool allowRollDice; //0
        private bool allowEndTurn; //1
        private bool allowUseDevCards; //2
        private bool allowReceiveTrade; //3
        private bool allowInitiateTrade; //4
        private bool allowBuildCity; //5
        private bool allowBuildSettlement; //6
        private bool allowBuildRoad; //7
        private bool allowMoveRobber; //8
        private bool allowSteal; //9
        private bool allowBuyCards; //10

        public Permissions()
        {
            Reset();
        }

        public void ChangePermissionCurrentPlayer(params int[] all)
        {
            foreach (int allow in all)
            {
                switch (allow)
                {
                    case 0: allowRollDice = !allowRollDice; break;
                    case 1: allowEndTurn = !allowEndTurn; break;
                    case 2: allowUseDevCards = !allowUseDevCards; break;
                    case 3: allowReceiveTrade = !allowReceiveTrade; break;
                    case 4: allowInitiateTrade = !allowInitiateTrade; break;
                    case 5: allowBuildCity = !allowBuildCity; break;
                    case 6: allowMoveRobber = !allowMoveRobber; break;
                    case 7: allowSteal = !allowSteal; break;
                    case 8: allowBuyCards = !allowBuyCards; break;
                    case 9: allowBuildSettlement = !allowBuildSettlement; break;
                    case 10: allowBuildRoad = !allowBuildRoad; break;

                }
            }
        }

        public void Reset()
        {
            allowRollDice = false;
            allowEndTurn = false;
            allowUseDevCards = false;
            allowReceiveTrade = false;
            allowInitiateTrade = false;
            allowBuildCity = false;
            allowMoveRobber = false;
            allowSteal = false;
            allowBuyCards = false;
            allowBuildSettlement = false;
            allowBuildRoad = false;
        }

        public void NewTurnCurrent()
        {
            Reset();
            ChangePermissionCurrentPlayer(0, 2);
        }

        public void NewTurnOthers()
        {
            Reset();
        }

        public void AfterDiceCurrent()
        {
            Reset();
            ChangePermissionCurrentPlayer(1, 2, 4, 5, 6, 7, 10);
        }

        public bool AllowRollDice { get { return this.allowRollDice; } set { this.allowRollDice = value; } }
        public bool AllowEndTurn { get { return this.allowEndTurn; } set { this.allowEndTurn = value; } }
        public bool AllowUseDevCards { get { return this.allowUseDevCards; } set { this.allowUseDevCards = value; } }
        public bool AllowRecieveTrade { get { return this.allowReceiveTrade; } set { this.allowReceiveTrade = value; } }
        public bool AllowInitiateTrade { get { return this.allowInitiateTrade; } set { this.allowInitiateTrade = value; } }
        public bool AllowBuild { get { return this.allowBuildCity; } set { this.allowBuildCity = value; } }
        public bool AllowMoveRobber { get { return this.allowMoveRobber; } set { this.allowMoveRobber = value; } }
        public bool AllowSteal { get { return this.allowSteal; } set { this.allowSteal = value; } }
        public bool AllowBuyCards { get { return this.allowBuyCards; } set { this.allowBuyCards = value; } }
        public bool AllowBuildSettlement { get { return this.allowBuildSettlement; } set { this.allowBuildSettlement = value; } }
        public bool AllowBuildRoad { get { return this.allowBuildRoad; } set { this.allowBuildRoad = value; } }




    }
}