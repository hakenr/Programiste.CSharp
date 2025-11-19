namespace ElevatorSimulation.Strategies;

/// <summary>
/// Directional strategy (SCAN algorithm) that continues in current direction
/// servicing all requests, then reverses. Commonly used in real elevator systems.
/// </summary>
public class DirectionalStrategy : IElevatorStrategy
{
	/// <inheritdoc />
	public MoveResult DecideNextMove(ElevatorSystem elevator)
	{
		// Check for passengers to drop off at this floor
		if (elevator.ActiveRiders.Any(r => r.To == elevator.CurrentElevatorFloor))
		{
			return MoveResult.OpenDoors;
		}

		// Check for passengers to pick up at this floor
		if (elevator.PendingRequests.Any(r => r.From == elevator.CurrentElevatorFloor))
		{
			return MoveResult.OpenDoors;
		}

		// If no requests, stay idle
		if (elevator.PendingRequests.Count == 0 && elevator.ActiveRiders.Count == 0)
		{
			return MoveResult.NoAction;
		}

		// Collect all target floors (dropoffs and pickups)
		var targetFloors = elevator.PendingRequests.Select(r => r.From)
			.Concat(elevator.ActiveRiders.Select(r => r.To))
			.ToList();

		var current = elevator.CurrentElevatorFloor;
		var dir = elevator.CurrentElevatorDirection;

		if (dir == Direction.Up || dir == Direction.Idle)
		{
			// Look for targets above current floor
			var upTargets = targetFloors.Where(f => f > current).OrderBy(f => f).ToList();
			if (upTargets.Any())
			{
				return MoveResult.MoveUp;
			}

			// No targets above, look below
			var downTargets = targetFloors.Where(f => f < current).OrderByDescending(f => f).ToList();
			if (downTargets.Any())
			{
				return MoveResult.MoveDown;
			}
		}
		else if (dir == Direction.Down)
		{
			// Look for targets below current floor
			var downTargets = targetFloors.Where(f => f < current).OrderByDescending(f => f).ToList();
			if (downTargets.Any())
			{
				return MoveResult.MoveDown;
			}

			// No targets below, look above
			var upTargets = targetFloors.Where(f => f > current).OrderBy(f => f).ToList();
			if (upTargets.Any())
			{
				return MoveResult.MoveUp;
			}
		}

		return MoveResult.NoAction;
	}
}
