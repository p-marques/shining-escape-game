
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject retryScreen;
    void EndGame()
    {
        Debug.Log("Game over");
        retryScreen.SetActive(true);

    }
}
