using UnityEngine;

public class CharacterDialogueTable : MonoBehaviour
{
    public CharacterDialogueTable_Entry[] table;
    public static CharacterDialogueTable instance;

    [System.Serializable]
    public class CharacterDialogueTable_Entry
    {
        public string internal_name = "";
        public CharDialogueData chardata;
    }

    public CharDialogueData get_character(string char_to_get)
    {
        for(int i = 0; i < table.Length; i++)
        {
            if(char_to_get == table[i].internal_name)
            {
                return table[i].chardata;
            }
        }
        
        Debug.Log("CharacterDialogueTable: CHAR" + char_to_get + " NOT FOUND, LOADING FALLBACK CHARDATA");
        return table[0].chardata;
    }

    // Singleton code---------------------------------
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
