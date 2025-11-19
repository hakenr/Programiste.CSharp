namespace ElevatorSimulation.Strategies;

/// <summary>
/// Defines a strategy for deciding the next elevator action.
/// Each decision represents one time unit (move one floor or open doors).
/// </summary>
public interface IElevatorStrategy
{
	/// <summary>
	/// Decides the next action for the elevator to take.
	/// </summary>
	/// <param name="elevator">The elevator instance with current state</param>
	/// <returns>The action to take (MoveUp, MoveDown, OpenDoors, NoAction)</returns>
	MoveResult DecideNextMove(ElevatorSystem elevator);
}