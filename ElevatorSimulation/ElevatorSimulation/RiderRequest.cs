namespace ElevatorSimulation;

/// <summary>
/// Represents a passenger request for elevator service.
/// </summary>
public class RiderRequest
{
	/// <summary>
	/// The floor where the passenger is waiting.
	/// </summary>
	public int From { get; }

	/// <summary>
	/// The destination floor where the passenger wants to go.
	/// </summary>
	public int To { get; }

	/// <summary>
	/// The simulation time when this request was created.
	/// </summary>
	public int CreatedAt { get; }

	/// <summary>
	/// The simulation time when the passenger was picked up (null if not yet picked up).
	/// </summary>
	public int? PickedUpAt { get; set; }

	/// <summary>
	/// The simulation time when the passenger was dropped off (null if not yet completed).
	/// </summary>
	public int? CompletedAt { get; set; }

	/// <summary>
	/// Creates a new elevator request.
	/// </summary>
	/// <param name="from">Starting floor</param>
	/// <param name="to">Destination floor</param>
	/// <param name="time">Current simulation time</param>
	public RiderRequest(int from, int to, int time)
	{
		From = from;
		To = to;
		CreatedAt = time;
	}
}
