public abstract class InteractableItem : Interactable<InteractableItemData>
{
    protected override void OccupidPlayer(PlayerSprit player)
    {
        player.Persona.AddItem(data);
    }
    protected override void OccupidNPC(NPCSprit npc)
    {
       // npc.persona.AddItem(data);
    }
}
