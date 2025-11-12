namespace ElevatorSimulation;

/// <summary>
/// Defines a strategy for selecting the next request to process.
/// </summary>
public interface IElevatorStrategy
{
	/// <summary>
	/// Selects the next request for the elevator to handle.
	/// </summary>
	/// <param name="elevator">The elevator instance with current state</param>
	/// <returns>The selected request, or null if no requests are pending</returns>
	Request SelectNext(Elevator elevator);
}