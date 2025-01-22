
using System.Threading.Tasks;

public class WildCard : Card
{
	public override Task Break()
	{
		isBreaking = false;
		return base.Break();
	}
}
