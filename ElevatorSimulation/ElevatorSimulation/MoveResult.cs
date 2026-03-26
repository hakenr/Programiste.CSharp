namespace ElevatorSimulation;

/// <summary>
/// Represents the result of a single time unit tick in the elevator simulation.
/// </summary>
public enum MoveResult
{
	/// <summary>Elevator moves up one floor (1 time unit).</summary>
	MoveUp,
	
	/// <summary>Elevator moves down one floor (1 time unit).</summary>
	MoveDown,
	
	/// <summary>Elevator opens doors for boarding/alighting (1 time unit).</summary>
	OpenDoors,
	
	/// <summary>Elevator has no action to perform.</summary>
	NoAction
}
