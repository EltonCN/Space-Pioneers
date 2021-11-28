using UnityEngine;

[CreateAssetMenu(menuName ="Space Pioneers/Save/SaveManager")]
public class SaveManager : ScriptableObject
{
    [SerializeField] int currentSave;
    [SerializeField] Save[] saves;

    public Save GetSave()
    {
        return saves[currentSave];
    }

    public void changeCurrentSave(int save_index)
    {
        if(save_index >= saves.Length || save_index < 0)
        {
            Debug.LogError("Changing to save out of range. Aborting change");
            return;
        }

        save();
        if(saves[save_index] == null)
        {
            saves[save_index] = Save.Load(save_index);
        }

        currentSave = save_index;
    }

    public void save()
    {
        saves[currentSave].save();
    }
}