using TMPro;
using UnityEngine;

public class DropdownValue : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private MainMenu mainmenu;
    public GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        mainmenu = gameManager.GetComponent<MainMenu>();
    }

    public void GetDifficultyValue()
    {
        int pickedEntryIndex = dropdown.value;
        mainmenu.dropdifficulty = pickedEntryIndex;
    }

    public void GetLapValue()
    {
        int pickedEntryIndex = dropdown.value;
        mainmenu.droplap = pickedEntryIndex;
    }

    public void GetLengthValue()
    {
        int pickedEntryIndex = dropdown.value;
        mainmenu.droplength = pickedEntryIndex;
    }
}
