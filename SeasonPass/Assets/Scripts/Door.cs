using System;
using UnityEngine;
using BhorGames;

public class Door : MonoBehaviour
{
    private const string PLAYER = "Player";
    private const string SPIN = "spin";

    [SerializeField] private SeasonTheme seasonTheme;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER))
        {
            if (!SeasonManager.currentSeason.Equals(seasonTheme))
            {
                ChangeSeason();
            }
            ResetClothes(other);
            SeasonManager.Instance.ChangePlatformMaterials(seasonTheme);
            SeasonManager.RestoreLives();
            GameManager.Instance.playerAnimator.SetTrigger(SPIN);
            UIManager.Instance.ChangeSeasonTextAndImages(seasonTheme);
        }
    }

    private void ChangeSeason()
    {
        SeasonManager.seasonsGO.transform.Find(SeasonManager.currentSeason.ToString()).gameObject.SetActive(false);
        SeasonManager.seasonsGO.transform.Find(seasonTheme.ToString()).gameObject.SetActive(true);
        SeasonManager.currentSeason = seasonTheme;
    }

    private void ResetClothes(Collider other)
    {
        foreach (CharacterCustomization.ClothesPartType type in Enum.GetValues(typeof(CharacterCustomization.ClothesPartType)))
        {
            other.GetComponent<CharacterCustomization>().SetElementByIndex(type, -1);
        }
    }
}
