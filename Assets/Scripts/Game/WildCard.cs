
public class WildCard : Card
{
	public override void Break() 
	{
		StartCoroutine(BreakIEnumerator());
		Board.Singleton.StartClearCreateFallLoop();
	}
}
