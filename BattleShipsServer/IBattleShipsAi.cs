namespace BattleShipsServer;

public interface IBattleShipsAi
{
    /// <summary>
    /// Start of the game, Setup your board.
    /// </summary>
    /// <param name="carrier">The Carrier position and orientation.</param>
    /// <param name="battleship">The BattleShip position and orientation.</param>
    /// <param name="Cruiser">The Cruiser position and orientation.</param>
    /// <param name="Submarine">The Submarine position and orientation.</param>
    /// <param name="Destroyer">The Destroyer position and orientation.</param>
    public void Start(out ShipPosition carrier, out ShipPosition battleship, out ShipPosition Cruiser, out ShipPosition Submarine, out ShipPosition Destroyer);

    /// <summary>
    /// Ai method for user to implement.
    /// </summary>
    /// <param name="previousAttackStatus">The status of your previous attack.</param>
    /// <param name="incomingAttack">The enemy's current attack coordinates.</param>
    /// <param name="enemyAttackStatus">The status of the enemy's 'incomingAttack'.</param>
    /// <param name="returnAttack">The next attack coordinates you will launch on the enemy.</param>
    public bool Ai(in AttackStatus previousAttackStatus, in Coordinate incomingAttack, out AttackStatus enemyAttackStatus, out Coordinate returnAttack);

    /// <summary>
    /// When the game ends or is interupted.
    /// </summary>
    public void End();
}
