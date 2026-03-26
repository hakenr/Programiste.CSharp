namespace ElevatorSimulation;

/// <summary>
/// Represents a building configuration with floor constraints.
/// </summary>
public class Building
{
	/// <summary>
	/// Minimum floor number (typically 0 for ground floor).
	/// </summary>
	public int MinFloor { get; }

	/// <summary>
	/// Maximum floor number (top floor).
	/// </summary>
	public int MaxFloor { get; }

	public Building(int minFloor, int maxFloor)
	{
		if (minFloor >= maxFloor)
		{
			throw new ArgumentException("MinFloor must be less than MaxFloor");
		}

		MinFloor = minFloor;
		MaxFloor = maxFloor;
	}

	/// <summary>
	/// Validates if a floor number is within building bounds.
	/// </summary>
	public bool IsValidFloor(int floor)
	{
		return (floor >= MinFloor) && (floor <= MaxFloor);
	}

	/// <summary>
	/// Generates a random valid floor number.
	/// </summary>
	public int GetRandomFloor(Random random)
	{
		return random.Next(MinFloor, MaxFloor + 1);
	}

	/// <summary>
	/// Creates a request with random floors, ensuring they are different.
	/// </summary>
	public RiderRequest CreateRandomRequest(Random random, int currentTime)
	{
		int from = GetRandomFloor(random);
		int to;
		do
		{
			to = GetRandomFloor(random);
		} while (to == from);

		return new RiderRequest(from, to, currentTime);
	}
}
