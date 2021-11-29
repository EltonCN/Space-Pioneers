using UnityEngine;
using System.IO;

[System.Serializable]
[CreateAssetMenu(menuName ="Space Pioneers/Save/Save")]
public class Save : ScriptableObject
{
    public int last_played_level;
    [SerializeField] private int index;

    public float Index
    {
        get
        {
            return this.index;
        }
    }

    private static string get_file_path(int index)
    {
        string path;

        #if UNITY_WEBGL
        path = System.IO.Path.Combine("idbfs", Application.productName);
        #else
        path = Application.persistentDataPath;
        #endif
        
        Directory.CreateDirectory(path);

        string filename = "save_file_"+index.ToString();

        return System.IO.Path.Combine(path, filename);
    }


    public static Save Load(int index)
    {
        string file_path = get_file_path(index);

        Save save = null;

        if(File.Exists(file_path))
        {
            try
            {
                using(StreamReader streamReader = File.OpenText(file_path))
                {
                    string json = streamReader.ReadToEnd();
                    save = JsonUtility.FromJson<Save>(json);

                    streamReader.Close();
                }
            }
            catch
            {
                Debug.LogError("Save "+index.ToString()+" corrompido");
            }
        }
        else
        {
            Debug.LogWarning("Save "+index.ToString()+" não encontrado");
        }

        if(save == null)
        {
            save = new Save();
            save.index = index;
        }

        return save;
    }

    public static void delete(int index)
    {
        string file_path = get_file_path(index);

        if(File.Exists(file_path))
        {
            File.Delete(file_path);
        }

    }

    public void save()
    {
        string json = JsonUtility.ToJson(this);
        string file_path = get_file_path(this.index);

        using (StreamWriter streamWriter = File.CreateText(file_path))
        {
            streamWriter.Write(json);
            streamWriter.Close();
        }

    }

}
