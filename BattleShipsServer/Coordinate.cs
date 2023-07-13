namespace BattleShipsServer;

// Position on board (10x10). The byte can be used as an index into a single demensional array representation of the board.
public enum Coordinate : byte
{
    A1, A2, A3, A4, A5, A6, A7, A8, A9, A10,
    B1, B2, B3, B4, B5, B6, B7, B8, B9, B10,
    C1, C2, C3, C4, C5, C6, C7, C8, C9, C10,
    D1, D2, D3, D4, D5, D6, D7, D8, D9, D10,
    E1, E2, E3, E4, E5, E6, E7, E8, E9, E10,
    F1, F2, F3, F4, F5, F6, F7, F8, F9, F10,
    G1, G2, G3, G4, G5, G6, G7, G8, G9, G10,
    H1, H2, H3, H4, H5, H6, H7, H8, H9, H10,
    I1, I2, I3, I4, I5, I6, I7, I8, I9, I10,
    J1, J2, J3, J4, J5, J6, J7, J8, J9, J10,

    End = 100 // Used to signal the end of the game, not actually part of the board. Sent by loser after 'AttackState.Hit' ex: 0x6764
}

public enum AttackStatus : byte
{
    Start = 101, // Sent from server to client on start of game. Followed by 'Player' byte ex: 0x6564 = first player, 0x6565 = second player.
    Miss = 102,  // Relayed from client to client on misses. Followed by attack 'Coordinate' byte ex: 0x662A = previous attack missed, next attack is E3.
    Hit = 103,   // Relayed from client to client on hits. Followed by attack 'Coordinate' byte.
}

// Sent along with but after 'AttackState.Start' byte. For client to know which player they are.
public enum Player : byte
{
    One = 104, // Starts computing moves as first player (makes the first move).
    Two = 105, // Starts computing moves as second player (waits for first move)
}

// Followed by 'Coordinate' byte
public enum Orientation : byte
{
    Horizontal = 150, // right
    Vertical = 151,   // down
}

public readonly struct ShipPosition
{
    public readonly Coordinate Coordinate { get; }
    public readonly Orientation Orientation { get; }
    public ShipPosition(Coordinate coordinate, Orientation orientation)
    {
        Coordinate = coordinate;
        Orientation = orientation;
    }
    public byte[] ToBytes() => new byte[] { (byte)Coordinate, (byte)Orientation };
}