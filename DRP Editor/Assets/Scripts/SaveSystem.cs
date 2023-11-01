using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] Obstacle obstaclePrefab;
    public static List<Obstacle> obstacles = new List<Obstacle>();

    const string Obstacle_sub = "/obstacle";
    const string Obstacle_Count_sub = "/obstacle.count";

    void Awake()
    {
        LoadObstacle();
    }

    void OnApplicationQuit()
    {
        SaveObstacle();
    }
 

    void SaveObstacle()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + Obstacle_sub +SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + Obstacle_Count_sub + SceneManager.GetActiveScene().buildIndex;

        FileStream countStream = new FileStream(countPath, FileMode.Create);
        formatter.Serialize(countStream, obstacles.Count);
        countStream.Close();


        for (int i = 0; i < obstacles.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            ObstacleData data = new ObstacleData(obstacles[i]);

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    void LoadObstacle()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + Obstacle_sub + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + Obstacle_Count_sub + SceneManager.GetActiveScene().buildIndex;
        int obstacleCount = 0;

        if (File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);

            obstacleCount = (int)formatter.Deserialize(countStream);
            countStream.Close();

        }
        else
        {
            Debug.LogError("Path not found in "+ countPath);
        }

        for (int i = 0; i < obstacleCount; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                ObstacleData data = formatter.Deserialize(stream) as ObstacleData;

                stream.Close();

                Vector3 position = new Vector3 (data.position[0], data.position[1],data.position[2]);
                Quaternion orientation = new Quaternion(data.orientation[0], data.orientation[1], data.orientation[2], data.orientation[3]);


                Obstacle obstacle = Instantiate(obstaclePrefab, position, orientation);

                obstacle.type = data.type;
                obstacle.id = data.id;

            }
            else
            {
                Debug.LogError("Path not found in " + path);
            }

        }
    }

}
