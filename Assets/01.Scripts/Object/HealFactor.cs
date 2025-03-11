public class HealFactor: InteractableStuff
{
    protected override void ApplyPlayer(PlayerSprit player)
    {
        player.Entity.Heal(20f);
    }
}
