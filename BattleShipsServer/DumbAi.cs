using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipsServer
{
    public class DumbAi : IBattleShipsAi
    {
        private readonly bool[] MyBoard = new bool[100];
        private readonly bool[] EnemyBoard = new bool[100];

        private int previousCoordinate = -1;
        private int piecesLeft = 17;

        public void Start(out ShipPosition carrier, out ShipPosition battleship, out ShipPosition Cruiser, out ShipPosition Submarine, out ShipPosition Destroyer)
        {
            carrier = new ShipPosition(Coordinate.A1, Orientation.Vertical);
            MyBoard[0] = true;
            MyBoard[10] = true;
            MyBoard[20] = true;
            MyBoard[30] = true;
            MyBoard[40] = true;
            battleship = new ShipPosition(Coordinate.A2, Orientation.Vertical);
            MyBoard[1] = true;
            MyBoard[11] = true;
            MyBoard[21] = true;
            MyBoard[31] = true;
            Cruiser = new ShipPosition(Coordinate.A3, Orientation.Vertical);
            MyBoard[2] = true;
            MyBoard[12] = true;
            MyBoard[22] = true;
            Submarine = new ShipPosition(Coordinate.A4, Orientation.Vertical);
            MyBoard[3] = true;
            MyBoard[13] = true;
            MyBoard[23] = true;
            Destroyer = new ShipPosition(Coordinate.A5, Orientation.Vertical);
            MyBoard[4] = true;
            MyBoard[14] = true;
        }

        public bool Ai(in AttackStatus previousAttackStatus, in Coordinate incomingAttack, out AttackStatus enemyAttackStatus, out Coordinate returnAttack)
        {
            if (previousAttackStatus == AttackStatus.Hit && incomingAttack == Coordinate.End)
            {
                enemyAttackStatus = AttackStatus.Miss;
                returnAttack = Coordinate.End;
                return false;
            }

            previousCoordinate++;

            if (MyBoard[(int)incomingAttack])
            {
                MyBoard[(int)incomingAttack] = false;
                enemyAttackStatus = AttackStatus.Hit;
                piecesLeft--;
                if (piecesLeft == 0)
                {
                    returnAttack = Coordinate.End;
                    return false;
                }
                else
                    returnAttack = (Coordinate)previousCoordinate;
            }
            else
            {
                enemyAttackStatus = AttackStatus.Miss;
                returnAttack = (Coordinate)previousCoordinate;
            }


            return true;
        }

        public void End()
        {
        }
    }
}
