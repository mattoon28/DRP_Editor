using UnityEngine;

[System.Serializable]
public class ObstacleData
{
    public int type;
    public int id;
    public float[] position;
    public float[] orientation;



    public ObstacleData(Obstacle obstacle)
    {
        type = obstacle.type;
        id = obstacle.id;


        Vector3 obstaclePos = obstacle.transform.position;
        position = new float[]
        {
            obstaclePos.x, obstaclePos.y, obstaclePos.z
        };



        Quaternion obstacleOrient = obstacle.transform.rotation;
        orientation = new float[]
        {
            obstacleOrient.x, obstacleOrient.y, obstacleOrient.z, obstacleOrient.w
        };

    }

}