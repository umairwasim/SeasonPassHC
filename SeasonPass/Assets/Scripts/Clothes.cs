using UnityEngine;
using TMPro;

public class Clothes : MonoBehaviour
{
    private const string CLOTHES = "Clothes";
    private const string SEASON_TEXT = "SeasonText";
    private const string ZONES = "Zones/";
    private const float TEXT_Y = 3.2f;
    private const string EMOJIS = "Emojis/";
    private const string EMOJI_COOL = "EmojiCool";
    private const string EMOJI_ANGRY = "EmojiAngry";
    private const string EMOJI_SAD = "EmojiSad";
    private const string EMOJI_YAWM = "EmojiYawn";
    private const string EMOJI_SCARED = "EmojiScared";

    public CharacterCustomization.ClothesPartType clothesPartType;
    public int index;
    public SeasonTheme relatedSeason;

    private GameObject clothesGO;

    private float rotateSpeed = 120f;
    private Vector3 startingScale;

    private void Start()
    {
        clothesGO = GameObject.Find(CLOTHES);
        startingScale = transform.localScale;

        InitializeZone();
        InitializeSeason();
    }

    private void InitializeSeason()
    {
        GameObject seasonText = Instantiate(Resources.Load(SEASON_TEXT)) as GameObject;
        seasonText.GetComponent<TextMeshPro>().text = relatedSeason.ToString();
        seasonText.transform.position = new Vector3(transform.position.x, TEXT_Y, transform.position.z);
        seasonText.transform.SetParent(transform);
    }

    private void InitializeZone()
    {
        string zonePath = ZONES + relatedSeason;
        GameObject zone = Instantiate(Resources.Load(zonePath)) as GameObject;
        zone.GetComponent<Zone>().linkedClothes = this;
        zone.transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
        zone.transform.SetParent(transform.parent);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0), Space.World);
        transform.localScale = Vector3.Lerp(startingScale, startingScale * 1.2f, Mathf.PingPong(Time.time, 1));
    }

    public void SetClothes(Collider other)
    {
        other.GetComponent<CharacterCustomization>().SetElementByIndex(clothesPartType, index);

        if (SeasonManager.currentSeason != relatedSeason)
            SeasonManager.LoseLives();

        string path = DetectEmojiPath();
        GameObject emoji = Instantiate(Resources.Load(path) as GameObject,
            new Vector3(transform.position.x, 3f, transform.position.z),
            Quaternion.identity);

        Destroy(clothesGO.transform.GetChild(0).gameObject);
    }

    private string DetectEmojiPath()
    {
        string path = EMOJIS;

        if (SeasonManager.currentSeason.Equals(SeasonTheme.AUTUMN))
        {
            if (relatedSeason.Equals(SeasonTheme.AUTUMN)) path += EMOJI_COOL;
            else if (relatedSeason.Equals(SeasonTheme.SUMMER)) path += EMOJI_ANGRY;
            else if (relatedSeason.Equals(SeasonTheme.WINTER)) path += EMOJI_SAD;
        }
        else if (SeasonManager.currentSeason.Equals(SeasonTheme.SUMMER))
        {
            if (relatedSeason.Equals(SeasonTheme.AUTUMN)) path += EMOJI_SAD;
            else if (relatedSeason.Equals(SeasonTheme.SUMMER)) path += EMOJI_COOL;
            else if (relatedSeason.Equals(SeasonTheme.WINTER)) path += EMOJI_YAWM;
        }
        else if (SeasonManager.currentSeason.Equals(SeasonTheme.WINTER))
        {
            if (relatedSeason.Equals(SeasonTheme.AUTUMN)) path += EMOJI_YAWM;
            else if (relatedSeason.Equals(SeasonTheme.SUMMER)) path += EMOJI_SCARED;
            else if (relatedSeason.Equals(SeasonTheme.WINTER)) path += EMOJI_COOL;
        }

        return path;
    }
}
