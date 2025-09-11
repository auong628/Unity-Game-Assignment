
using UnityEngine;
using TMPro;

public class MolassesManager : MonoBehaviour
{
    public static MolassesManager instance;
    private int molasses;
    [SerializeField] private TMP_Text molassesDisplay;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    private void OnGUI()
    {
        molassesDisplay.text = molasses.ToString();
    }
    public void ChangeMolasses(int amount)
    {
        molasses += amount;
    }
}
