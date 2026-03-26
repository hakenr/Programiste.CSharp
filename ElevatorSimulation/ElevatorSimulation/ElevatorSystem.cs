using ElevatorSimulation.Strategies;

namespace ElevatorSimulation;

/// <summary>
/// Represents an elevator that services passenger requests using a specified strategy.
/// </summary>
public class ElevatorSystem
{
	/// <summary>
	/// The current floor where the elevator is located.
	/// </summary>
	public int CurrentElevatorFloor { get; private set; } = 0;

	/// <summary>
	/// The current direction of elevator movement.
	/// </summary>
	public Direction CurrentElevatorDirection { get; private set; } = Direction.Idle;

	/// <summary>
	/// List of all pending requests (waiting to be picked up).
	/// </summary>
	public List<RiderRequest> PendingRequests { get; } = new();

	/// <summary>
	/// Active riders currently in the elevator.
	/// </summary>
	public List<RiderRequest> ActiveRiders { get; } = new();

	/// <summary>
	/// Statistics tracker for this elevator.
	/// </summary>
	public Statistics Statistics { get; } = new();

	/// <summary>
	/// The building configuration.
	/// </summary>
	public Building Building { get; }

	/// <summary>
	/// Current simulation time.
	/// </summary>
	public int CurrentTime { get; private set; }

	/// <summary>
	/// If true, suppresses console output during simulation.
	/// </summary>
	public bool SilentMode { get; set; } = false;

	private IElevatorStrategy _strategy;

	/// <summary>
	/// Creates a new elevator with the specified strategy and building configuration.
	/// </summary>
	/// <param name="strategy">The strategy to use for decision making</param>
	/// <param name="building">The building configuration</param>
	public ElevatorSystem(IElevatorStrategy strategy, Building building)
	{
		_strategy = strategy;
		Building = building;

		if (!building.IsValidFloor(CurrentElevatorFloor))
		{
			CurrentElevatorFloor = building.MinFloor;
		}
	}

	/// <summary>
	/// Adds a new request to the pending queue.
	/// </summary>
	public void ReceiveRequest(RiderRequest request)
	{
		if (!Building.IsValidFloor(request.From))
		{
			throw new ArgumentException($"Invalid From floor: {request.From}. Must be between {Building.MinFloor} and {Building.MaxFloor}");
		}
		if (!Building.IsValidFloor(request.To))
		{
			throw new ArgumentException($"Invalid To floor: {request.To}. Must be between {Building.MinFloor} and {Building.MaxFloor}");
		}

		PendingRequests.Add(request);
	}

	/// <summary>
	/// Executes one time unit tick. Returns the action taken.
	/// Moving one floor = 1 time unit.
	/// Opening/closing doors (stopping) = 1 time unit.
	/// Idling = 1 time unit.
	/// </summary>
	public MoveResult TickOneTimeUnit()
	{
		CurrentTime++;

		var result = _strategy.DecideNextMove(this);

		// Handle movement / people getting on/off
		switch (result)
		{
			case MoveResult.MoveUp:
				CurrentElevatorFloor++;
				CurrentElevatorDirection = Direction.Up;
				if (!SilentMode)
					Console.WriteLine($"[{CurrentTime:00}] ⬆️  Move up to floor {CurrentElevatorFloor}");
				break;
			case MoveResult.MoveDown:
				CurrentElevatorFloor--;
				CurrentElevatorDirection = Direction.Down;
				if (!SilentMode)
					Console.WriteLine($"[{CurrentTime:00}] ⬇️  Move down to floor {CurrentElevatorFloor}");
				break;
			case MoveResult.OpenDoors:
				// Drop off riding passengers at current floor
				var droppedOff = ActiveRiders
					.Where(r => r.To == CurrentElevatorFloor)
					.ToList();

				foreach (var request in droppedOff)
				{
					request.CompletedAt = CurrentTime;
					Statistics.RecordCompletedRequest(request);
					ActiveRiders.Remove(request);
					if (!SilentMode)
						Console.WriteLine($"[{CurrentTime:00}] 🚪 Drop off passenger at floor {CurrentElevatorFloor}");
				}

				// Pick up waiting passengers at current floor
				var waitingRiders = PendingRequests
					.Where(r => r.From == CurrentElevatorFloor)
					.ToList();

				foreach (var rider in waitingRiders)
				{
					// Remove from pending & add to active
					rider.PickedUpAt = CurrentTime;
					PendingRequests.Remove(rider);
					ActiveRiders.Add(rider);
					if (!SilentMode)
						Console.WriteLine($"[{CurrentTime:00}] 🚪 Pick up passenger at floor {CurrentElevatorFloor} (→ {rider.To})");
				}
				break;
			case MoveResult.NoAction:
				CurrentElevatorDirection = Direction.Idle;
				if (!SilentMode)
					Console.WriteLine($"[{CurrentTime:00}] 🛑 No action (idle)");
				break;
		}

		return result;
	}
}
