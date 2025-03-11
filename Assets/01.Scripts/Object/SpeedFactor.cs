public class SpeedFactor : InteractableStuff
{
    protected override void ApplyPlayer(PlayerSprit player)
    {
        player.Entity.StartCoroutine(player.Entity.IncreaseSpeed(10f,5f));
    }
}
