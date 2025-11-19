namespace ElevatorSimulation.Strategies;

/// <summary>
/// A simple nearest-first strategy - always serves the nearest pending request.
/// This is just an example strategy for testing the tournament.
/// </summary>
public class NearestFirstStrategy : IElevatorStrategy
{
	public MoveResult DecideNextMove(ElevatorSystem elevator)
	{
		// If we have active riders, prioritize dropping them off
		if (elevator.ActiveRiders.Count > 0)
		{
			var closestDestination = elevator.ActiveRiders
				.Select(r => r.To)
				.OrderBy(floor => Math.Abs(floor - elevator.CurrentElevatorFloor))
				.First();

			if (closestDestination > elevator.CurrentElevatorFloor)
				return MoveResult.MoveUp;
			if (closestDestination < elevator.CurrentElevatorFloor)
				return MoveResult.MoveDown;
			return MoveResult.OpenDoors;
		}

		// If we have pending requests, go to the nearest one
		if (elevator.PendingRequests.Count > 0)
		{
			var nearestRequest = elevator.PendingRequests
				.OrderBy(r => Math.Abs(r.From - elevator.CurrentElevatorFloor))
				.First();

			if (nearestRequest.From > elevator.CurrentElevatorFloor)
				return MoveResult.MoveUp;
			if (nearestRequest.From < elevator.CurrentElevatorFloor)
				return MoveResult.MoveDown;
			return MoveResult.OpenDoors;
		}

		// Nothing to do
		return MoveResult.NoAction;
	}
}
