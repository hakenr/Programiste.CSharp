namespace ElevatorSimulation.Strategies;

/// <summary>
/// Serves requests in a first-in-first-out (FIFO) manner.
/// Does not stop to pick up or drop off passengers unless it is at the floor of the earliest request.
/// </summary>
public class FifoStrategy : IElevatorStrategy
{
	public MoveResult DecideNextMove(ElevatorSystem elevator)
	{
		// Chce zde první pasažér vystoupit?
		if (elevator.ActiveRiders.FirstOrDefault()?.To == elevator.CurrentElevatorFloor)
		{
			return MoveResult.OpenDoors;
		}
		// Je zde první čekající požadavek na vyzvednutí?
		if ((elevator.ActiveRiders.Count == 0)
			&& (elevator.PendingRequests.FirstOrDefault()?.From == elevator.CurrentElevatorFloor))
		{
			return MoveResult.OpenDoors;
		}
		// If no requests, stay idle
		if (elevator.PendingRequests.Count == 0 && elevator.ActiveRiders.Count == 0)
		{
			return MoveResult.NoAction;
		}
		int targetFloor;
		if (elevator.ActiveRiders.Count > 0)
		{
			// Target the first active rider's destination
			var firstRider = elevator.ActiveRiders.First();
			targetFloor = firstRider.To;
		}
		else
		{
			// Target the first pending request's source floor
			var firstRequest = elevator.PendingRequests.First();
			targetFloor = firstRequest.From;
		}
		return MoveTowards(elevator.CurrentElevatorFloor, targetFloor);
	}

	private static MoveResult MoveTowards(int currentFloor, int targetFloor)
	{
		return (targetFloor - currentFloor) switch
		{
			> 0 => MoveResult.MoveUp,
			< 0 => MoveResult.MoveDown,
			_ => MoveResult.NoAction,
		};
	}
}
