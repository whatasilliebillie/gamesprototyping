using UnityEngine;

public class LevelMusicPlayer : MonoBehaviour
{
    [SerializeField] private SoundSO music;

    public void Start()
    {
        if(music != null && SoundManager.Instance != null)
        {
            // MAKE SURE SOUNDSO IS E TO ISLOOPING
            SoundManager.Instance.PlaySound(music, transform.position);
        }
    }

}
