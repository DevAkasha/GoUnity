public class SpeedFactor : InteractableStuff
{
    protected override void ApplyPlayer(PlayerSprit player)
    {
        player.entity.StartCoroutine(player.entity.IncreaseSpeed(10f,5f));
    }
}
