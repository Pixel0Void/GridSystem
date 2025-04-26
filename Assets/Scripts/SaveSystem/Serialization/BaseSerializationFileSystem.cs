using System.IO;

public abstract class BaseSerializationFileSystem : ISerializationFileSystem, ISerializationSystem
{
    public abstract string Extension { get; }

    public string DirectoryName { get; set; }

    public virtual string DefaultKey => "SaveFile";


    public BaseSerializationFileSystem(string directoryName)
    {
        DirectoryName = directoryName;
    }

    public bool SerializeObject<T>(T obj)
    {
        return SerializeObject(obj, DefaultKey);
    }


    public bool SerializeObject<T>(T obj, string key)
    {
        if(!Directory.Exists(DirectoryName))
        {
            Directory.CreateDirectory(DirectoryName);
        }

        using (FileStream stream = new FileStream(SavePath(key), FileMode.Create))
        {
            return HandleSaveObject(stream, obj);
        }
    }

    public T DeserializeObject<T>()
    {
        return DeserializeObject<T>(DefaultKey);
    }

    public T DeserializeObject<T>(string key)
    {
        if (!File.Exists(SavePath(key)))
        {
            return default;
        }

        using (FileStream stream = new FileStream(SavePath(key), FileMode.Open))
        {
            return HandleLoadObject<T>(stream);
        }
    }

    private string SavePath(string key)
    {
        return $"{DirectoryName}{key}.{Extension}";
    }

    protected abstract bool HandleSaveObject<T>(Stream stream, T obj);
    protected abstract T HandleLoadObject<T>(Stream stream);
}
