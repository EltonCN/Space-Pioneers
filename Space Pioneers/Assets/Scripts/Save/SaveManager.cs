using UnityEngine;

[CreateAssetMenu(menuName ="Space Pioneers/Save/SaveManager")]
public class SaveManager : ScriptableObject
{
    [SerializeField] int currentSave;
    [SerializeField] Save[] saves;

    public Save Save
    {
        get
        {
            if(saves[currentSave] == null)
            {
                load();
            }
            return saves[currentSave];
        }
    }

    public int CurrentSave
    {
        get
        {
            return currentSave;
        }
        set
        {
            changeCurrentSave(value);
        }
    }

    public void changeCurrentSave(int save_index)
    {
        if(save_index >= saves.Length || save_index < 0)
        {
            Debug.LogError("Changing to save out of range. Aborting change");
            return;
        }

        save();
        currentSave = save_index;
        load();
    }

    public void save()
    {
        saves[currentSave].save();
    }

    public void load()
    {
        saves[currentSave] = Save.Load(currentSave);
    }

    public void saveAll()
    {
        for(int i = 0; i<saves.Length; i++)
        {
            if(saves[i] != null)
            {
                saves[i].save();
            }
        }
    }

    public void deleteAll()
    {
        for(int i = 0; i<saves.Length; i++)
        {
            Save.delete(i);
        }
    }

    public void delete(int index)
    {
        Save.delete(index);
    }
}