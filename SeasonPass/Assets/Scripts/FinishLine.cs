using UnityEngine;
using BhorGames;

public class FinishLine : MonoBehaviour
{
    private const string WIN = "win";
    private const string PLAYER = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER))
        {
            UIManager.Instance.HideLevelText();
            GameManager.Instance.playerAnimator.SetTrigger(WIN);
            FindObjectOfType<PlayerController>().enabled = false;
            UIManager.Instance.OpenWinPanel();
        }
    }
}
