using UnityEngine;

[CreateAssetMenu(fileName = "CharDialogueData", menuName = "Scriptable Objects/CharDialogueData")]
public class CharDialogueData : ScriptableObject
{
    public string charname;
    public AudioClip voice_beep;
    public Emotion[] emotions;
    public Vector2 offset = new Vector2(0, 0);


    [System.Serializable]
    public class Emotion
    {
        public string animation_name;
        public Sprite[] frames;
    }

    public Sprite get_emotion(string emotion_to_fetch = "default", int frame_to_get = 0)
    {
        // Error checks
        if (emotions == null || emotions.Length == 0)
        {
            return null;
        }
        // Loop through emotions[] to find a specific frame
        int i;
        for(i = 0; i < emotions.Length; i++)
        {
            if(emotions[i].animation_name == emotion_to_fetch)
            {
                return emotions[i].frames[frame_to_get];
            }
        }

        Debug.Log("CharDialogueData: ANIMATION NOT FOUND, LOADING FALLBACK ANIM");
        return emotions[0].frames[0];
    }
}
