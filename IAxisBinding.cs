namespace Bloops.SOInputSystem
{
	public interface IAxisBinding
	{
		public string Identifier { get; }
		public float GetAxis();
	}
}