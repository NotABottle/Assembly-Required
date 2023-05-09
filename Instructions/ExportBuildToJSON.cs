using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExportBuildToJSON : MonoBehaviour
{
    [System.Serializable]
    public class PlacedBrickData{
        public float xPosition;
        public float yPosition;
        public float zPosition;

        public float yRotation;
        
        // public Transform brickParent;
    }

    public List<PlacedBrickData> placedBrickData;

    private string filePath;

    private void Awake(){
        filePath = Application.dataPath + "/BrickData.json";
    }

    public void WriteBuildDataToJSON()
    {
        //Create json file
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        //Get data
        var placedBricks = GameObject.FindObjectsOfType<Brick>();
        placedBrickData = new List<PlacedBrickData>();
        foreach(Brick b in placedBricks){
            var brickData = new PlacedBrickData();

            var brickPosition = b.transform.position;
            brickData.xPosition = brickPosition.x;
            brickData.yPosition = brickPosition.y;
            brickData.zPosition = brickPosition.z;

            var brickRotation = b.transform.root.eulerAngles;
            brickData.yRotation = brickRotation.y;

            placedBrickData.Add(brickData);
        }
        var jsonData = JsonHelper.ToJson(placedBrickData.ToArray(),true);

        Debug.Log(jsonData);

        //Store data in file
        File.WriteAllText(filePath,jsonData);

    }

}
