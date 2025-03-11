public class HealFactor: InteractableStuff
{
    protected override void ApplyPlayer(PlayerSprit player)
    {
        player.entity.Heal(20f);
    }
}
