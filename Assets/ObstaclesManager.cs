using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public Transform[] ObstaclesAreas;
    public Transform[] Lanes;
    public GameObject [] ObstaclePrefabs;


    private ObstacleArea[] Oas;
 
    // Use this for initialization
    public struct ObstacleArea
    {
        public Vector3 start, end;

        public ObstacleArea(Transform s, Transform e)
        {
            start = s.TransformPoint(s.position);
            end = e.TransformPoint(e.position);
        }
    }

    void Start()
    {
      
        Oas = new ObstacleArea[ObstaclesAreas.Length];
        for (int i = 0; i < ObstaclesAreas.Length; i++)
        {
            Oas[i] = new ObstacleArea(ObstaclesAreas[i].GetChild(0), ObstaclesAreas[i].GetChild(1));
        }
        SpawObject();
    }

    public void SpawObject()
    {

        foreach(ObstacleArea ob in Oas)
        {
            int range = Mathf.Abs((int)(ob.start.z-ob.end.z));
            Debug.Log(range);
            int step = 0;
            while(step<range)
            {
                //lancio una moneta per decidere se spawnare o no l'ostacolo
                if (Random.Range(0, 2) == 1)
                    step = RndObstacles(ob.start.x,0.0f,ob.start.z,step);
                else
                    step += 1;
                         
            }


        }

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    int RndObstacles(float x,float y,float z,int Currentstep)
    {
        //decido random se l'ostacolo occupa 1 o più di un blocco
        switch(Random.Range(0, 3))
        {
            // un blocco
            case 0:     
                InstantiateObstacle(new Vector3(Lanes[Random.Range(0,3)].position.x, y, z+Currentstep));
                Currentstep += 4;
                break;

            //due blocchi
            case 1:
                
                int excluded=Random.Range(0, 3);
                for(int i=0;i<3;i++)
                {
                    if(i!=excluded)
                    {
                        InstantiateObstacle(new Vector3(Lanes[i].position.x, y , z + Currentstep + 1));
                    }

                }
                Currentstep += 5;
                break;

            //tre blocchi
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    InstantiateObstacle(new Vector3(Lanes[i].position.x, y, z + Currentstep + 1));
                }
                Currentstep += 6;
                break;
        }
        return Currentstep;
        
    }

    //z=y
    //y=z
    void InstantiateObstacle(Vector3 pos)
    {
       StaticPool.Instantiate(ObstaclePrefabs[Random.Range(0, ObstaclePrefabs.Length)],new Vector3(pos.x,pos.y,pos.z));
    }

    public void ResetObstaclesArea()
    {

        GameObject[] obstacles = MultiTags.FindGameObjectsWithMultiTags("Obstacle");
        foreach (GameObject o in obstacles)
            StaticPool.Destroy(o);
        
    }


}
