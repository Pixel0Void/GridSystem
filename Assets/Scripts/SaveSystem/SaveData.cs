using System;

[Serializable]
public struct SaveData
{
    public string Name;
    public int XPosition;
    public int ZPosition;
    public float YRotation;

    public SaveData(string name, int x, int z, float yRotation)
    {
        Name = name;
        XPosition = x;
        ZPosition = z;
        YRotation = yRotation;
    }
}
