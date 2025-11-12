namespace ElevatorSimulation;

/// <summary>
/// Represents an elevator that services passenger requests using a specified strategy.
/// </summary>
public class Elevator
{
	/// <summary>
	/// The current floor where the elevator is located.
	/// </summary>
	public int CurrentFloor { get; private set; } = 0;
	
	/// <summary>
	/// The current direction of elevator movement.
	/// </summary>
	public Direction Direction { get; private set; } = Direction.Idle;
	
	/// <summary>
	/// List of all pending requests (not yet completed).
	/// </summary>
	public List<Request> PendingRequests { get; } = new();
	
	/// <summary>
	/// The currently active request being serviced (null if none).
	/// </summary>
	public Request ActiveRequest => _active;
	
	/// <summary>
	/// Statistics tracker for this elevator.
	/// </summary>
	public Statistics Statistics { get; } = new();
	
	/// <summary>
	/// The building configuration.
	/// </summary>
	public Building Building { get; }
	
	private IElevatorStrategy _strategy;
	private Request _active;

	/// <summary>
	/// Creates a new elevator with the specified strategy and building configuration.
	/// </summary>
	/// <param name="strategy">The strategy to use for request selection</param>
	/// <param name="building">The building configuration</param>
	public Elevator(IElevatorStrategy strategy, Building building)
	{
		_strategy = strategy;
		Building = building;
		
		if (!building.IsValidFloor(CurrentFloor))
		{
			CurrentFloor = building.MinFloor;
		}
	}

	/// <summary>
	/// Adds a new request to the elevator queue.
	/// </summary>
	/// <param name="req">The request to add</param>
	/// <exception cref="ArgumentException">Thrown if floor numbers are invalid</exception>
	public void AddRequest(Request req)
	{
		if (!Building.IsValidFloor(req.From))
		{
			throw new ArgumentException($"Invalid From floor: {req.From}. Must be between {Building.MinFloor} and {Building.MaxFloor}");
		}
		if (!Building.IsValidFloor(req.To))
		{
			throw new ArgumentException($"Invalid To floor: {req.To}. Must be between {Building.MinFloor} and {Building.MaxFloor}");
		}
		
		PendingRequests.Add(req);
	}

	/// <summary>
	/// Executes one simulation step, moving the elevator or servicing a request.
	/// </summary>
	/// <param name="time">Current simulation time</param>
	public void Step(int time)
	{
		if (_active == null)
		{
			_active = _strategy.SelectNext(this);
			if (_active == null)
			{
				Direction = Direction.Idle;
				return;
			}
		}

		int target = _active.PickedUpAt == null ? _active.From : _active.To;

		if (CurrentFloor == target)
		{
			if (_active.PickedUpAt == null)
			{
				_active.PickedUpAt = time;
				Console.WriteLine($"[{time:00}] Pick up passenger at {CurrentFloor} (→ {_active.To})");
			}
			else
			{
				_active.CompletedAt = time;
				Console.WriteLine($"[{time:00}] Drop off passenger at {CurrentFloor}");
				PendingRequests.Remove(_active);
				Statistics.RecordCompletedRequest(_active);
				_active = null;
			}
		}
		else
		{
			Direction = target > CurrentFloor ? Direction.Up : Direction.Down;
			CurrentFloor += Direction == Direction.Up ? 1 : -1;
			Console.WriteLine($"[{time:00}] Elevator moving {Direction} (floor {CurrentFloor})");
		}
	}
}