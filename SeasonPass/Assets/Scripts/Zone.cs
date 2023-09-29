using UnityEngine;

public class Zone : MonoBehaviour
{
    private const string PLAYER = "Player";
    public Clothes linkedClothes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER))
        {
            linkedClothes.SetClothes(other);
        }
    }
}
