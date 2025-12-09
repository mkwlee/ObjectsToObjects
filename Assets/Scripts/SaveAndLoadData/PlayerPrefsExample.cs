using UnityEngine;

public class PlayerPrefsExample : MonoBehaviour
{
    public void SaveData()
    {
        PlayerPrefs.SetString("KeybindJump", "Space");
    }


    public void LoadData()
    {
        if (PlayerPrefs.HasKey("KeybindJump"))
        {
            string keybind = PlayerPrefs.GetString("KeybindJump");
            Debug.Log($"Loaded Keybind for Jump: {keybind}");
        }
        else
        {
            Debug.Log("No Keybind found for Jump.");
        }
    }

    public void DeleteData()
    {
        if (PlayerPrefs.HasKey("KeybindJump"))
        {
            PlayerPrefs.DeleteKey("KeybindJump");
            Debug.Log("Deleted Keybind for Jump.");
        }
        else
        {
            Debug.Log("No Keybind found for Jump to delete.");
        }
    }
}
