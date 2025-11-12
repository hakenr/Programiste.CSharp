namespace ElevatorSimulation;

/// <summary>
/// Directional strategy (also known as SCAN algorithm) that services all requests
/// in the current direction before reversing. This minimizes direction changes
/// and is commonly used in real elevator systems.
/// </summary>
public class DirectionalStrategy : IElevatorStrategy
{
	/// <inheritdoc />
	public Request SelectNext(Elevator elevator)
	{
		// If we have an active passenger, we must deliver them first
		if (elevator.ActiveRequest?.PickedUpAt != null)
		{
			return elevator.ActiveRequest;
		}

		var dir = elevator.Direction;
		var current = elevator.CurrentFloor;

		if (dir == Direction.Up)
		{
			// Look for requests in the current direction (up)
			var upReq = elevator.PendingRequests
				.Where(r => r.From >= current)
				.OrderBy(r => r.From)
				.FirstOrDefault();
			if (upReq != null) return upReq;
			
			// No more requests going up, change direction
			return elevator.PendingRequests
				.OrderByDescending(r => r.From)
				.FirstOrDefault();
		}
		else if (dir == Direction.Down)
		{
			// Look for requests in the current direction (down)
			var downReq = elevator.PendingRequests
				.Where(r => r.From <= current)
				.OrderByDescending(r => r.From)
				.FirstOrDefault();
			if (downReq != null) return downReq;
			
			// No more requests going down, change direction
			return elevator.PendingRequests
				.OrderBy(r => r.From)
				.FirstOrDefault();
		}
		
		// Idle - pick the closest request
		return elevator.PendingRequests
			.OrderBy(r => Math.Abs(r.From - current))
			.FirstOrDefault();
	}
}