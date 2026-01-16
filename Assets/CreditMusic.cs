using UnityEngine;
using Utils;

public class CreditMusic : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayMusic(AudioManager.Musics.STEEL, true, transform);
    }
}
