using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private string _callToAction;
    [SerializeField] private string _sceneName;

    public string CallToAction => _callToAction;

    public void Interact()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public bool CanPlayerInteract(PlayerController player)
    {
        return !player.IsBeingChased;
    }
}
