using UnityEngine;
//using BhorGames;

public class FinishLine : MonoBehaviour
{
    private const string PLAYER = "Player";
    private const string WIN = "win";
    private const string FAIL = "fail";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER))
        {
            UIManager.Instance.HideLevelText();
            FindObjectOfType<PlayerController>().enabled = false;

            //Level Failed
            if (SeasonManager.lives <= 0)
            {
                GameManager.Instance.playerAnimator.SetTrigger(FAIL);
                UIManager.Instance.OpenLosePanel();
            }
            //Level Won
            else
            {
                GameManager.Instance.playerAnimator.SetTrigger(WIN);
                UIManager.Instance.OpenWinPanel();
            }
        }
    }
}
