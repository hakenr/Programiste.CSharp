namespace DirReduction
{
	[TestClass]
	public class DirReductionTests
	{
		[TestMethod]
		public void Test1()
		{
			string[] a = new string[] { "NORTH", "SOUTH", "SOUTH", "EAST", "WEST", "NORTH", "WEST" };
			string[] b = new string[] { "WEST" };
			CollectionAssert.AreEqual(b, DirReduction.dirReduc(a));
		}
		
		[TestMethod]
		public void Test2()
		{
			string[] a = new string[] { "NORTH", "WEST", "SOUTH", "EAST" };
			string[] b = new string[] { "NORTH", "WEST", "SOUTH", "EAST" };
			CollectionAssert.AreEqual(b, DirReduction.dirReduc(a));
		}
	}
}