namespace ElevatorSimulation;

/// <summary>
/// Represents the direction of elevator movement.
/// </summary>
public enum Direction
{
	/// <summary>Moving upward to higher floors.</summary>
	Up,

	/// <summary>Moving downward to lower floors.</summary>
	Down,

	/// <summary>Not moving, waiting for requests.</summary>
	Idle
}