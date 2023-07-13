using System.Net.Sockets;

namespace BattleShipsServer;

public static class BattleShipConnector
{
    public static void Battle<T>(Socket connection, T battleshipAi) where T : IBattleShipsAi
    {
        battleshipAi.Start(out ShipPosition carrier, out ShipPosition battleship, out ShipPosition cruiser, out ShipPosition submarine, out ShipPosition destroyer);
        connection.Send(carrier.ToBytes());
        connection.Send(battleship.ToBytes());
        connection.Send(cruiser.ToBytes());
        connection.Send(submarine.ToBytes());
        connection.Send(destroyer.ToBytes());
        byte[] buffer = new byte[2];
        connection.Receive(buffer);
        while (connection.Connected && battleshipAi.Ai((AttackStatus)buffer[0], (Coordinate)buffer[1], out AttackStatus enemyAttackStatus, out Coordinate returnAttack))
        {
            connection.Send(new byte[] { (byte)enemyAttackStatus, (byte)returnAttack });
            connection.Receive(buffer);
        }
        battleshipAi.End();
    }

    public static void Battle<T>(Stream connection, T battleshipAi) where T : IBattleShipsAi
    {
        battleshipAi.Start(out ShipPosition carrier, out ShipPosition battleship, out ShipPosition cruiser, out ShipPosition submarine, out ShipPosition destroyer);
        connection.Write(carrier.ToBytes());
        connection.Write(battleship.ToBytes());
        connection.Write(cruiser.ToBytes());
        connection.Write(submarine.ToBytes());
        connection.Write(destroyer.ToBytes());
        byte[] buffer = new byte[2];
        connection.Read(buffer);
        while (battleshipAi.Ai((AttackStatus)buffer[0], (Coordinate)buffer[1], out AttackStatus enemyAttackStatus, out Coordinate returnAttack))
        {
            connection.Write(new byte[] { (byte)enemyAttackStatus, (byte)returnAttack });
            connection.Read(buffer);
        }
        battleshipAi.End();
    }
}
