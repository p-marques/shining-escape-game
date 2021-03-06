public interface IInteractable
{
    string CallToAction { get; }
    void Interact();
    bool CanPlayerInteract(PlayerController player);
}
