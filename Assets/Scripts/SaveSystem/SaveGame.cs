using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public Transform BuildingsParent;
    public BuildingsRefrencess Buildings;

    void Start()
    {
        List<SaveData> data = SaveSystem.Load<List<SaveData>>("BuildingsData");
        if (data == null)
            return;

        foreach (SaveData dataItem in data)
        {
            GameObject objToBuild = Buildings.FloorPrefab;
            if (dataItem.Name.Contains("Floor")) objToBuild = Buildings.FloorPrefab;
            else if (dataItem.Name.Contains("Wall")) objToBuild = Buildings.WallPrefab;
            else if (dataItem.Name.Contains("Window")) objToBuild = Buildings.WindowPrefab;
            else if (dataItem.Name.Contains("Door")) objToBuild = Buildings.DoorPrefab;

            Instantiate(objToBuild, new Vector3(dataItem.XPosition, 0f, dataItem.ZPosition), Quaternion.Euler(0f, dataItem.YRotation, 0f), BuildingsParent);
        }
    }

    void OnApplicationQuit()
    {
        List<SaveData> data = new List<SaveData>();

        for (int i = 0; i < BuildingsParent.childCount; i++)
        {
            Transform t = BuildingsParent.GetChild(i);
            data.Add(new SaveData(t.name, (int)t.position.x, (int)t.position.z, t.rotation.eulerAngles.y));
        }

        Debug.Log(SaveSystem.Save("BuildingsData", data));
    }
}