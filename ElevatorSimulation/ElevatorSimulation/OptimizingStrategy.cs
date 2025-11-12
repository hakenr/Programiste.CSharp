namespace ElevatorSimulation;

/// <summary>
/// Optimizing strategy that selects the closest request to minimize total travel distance.
/// This strategy prioritizes efficiency by always picking the nearest pending request.
/// </summary>
public class OptimizingStrategy : IElevatorStrategy
{
	public Request SelectNext(Elevator elevator)
	{
		// If we have an active passenger, we must deliver them first
		if (elevator.ActiveRequest?.PickedUpAt != null)
		{
			return elevator.ActiveRequest;
		}

		var current = elevator.CurrentFloor;
		
		// Select the request with minimum distance from current floor
		return elevator.PendingRequests
			.OrderBy(r => Math.Abs(r.From - current))
			.ThenBy(r => Math.Abs(r.To - current)) // Secondary sort by destination
			.FirstOrDefault();
	}
}
