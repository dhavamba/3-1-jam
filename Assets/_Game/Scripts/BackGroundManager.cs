using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour {

    public Transform[] PropsAreas;
    public GameObject[] PropsPrefab;
    public float OffsetZ = 10f;
    public float OffsetX = 2f;
    public float scaleProps = 2f;

    // Use this for initialization
    void Start ()
    {
        GenerateBackGround();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void GenerateBackGround()
    {
        for (int i = 0; i < PropsAreas.Length; i++)
        {
            RndProps(PropsAreas[i].position.x, PropsAreas[i].position.y, PropsAreas[i].position.z);
        }
    }

    void RndProps(float x,float y,float z)
    {
        float count = -OffsetZ;
        while(count<OffsetZ)
        {
            if (Random.Range(0, 2) == 1)
            {
                if(Random.Range(0, 2) == 1)
                    InstantiateProp(new Vector3(x+1,y,z+count));
                else
                    InstantiateProp(new Vector3(x - 1, y, z + count));
            }
            count += 2f;
        }
    }

    
    void InstantiateProp(Vector3 pos)
    {
        PropsPrefab[Random.Range(0, PropsPrefab.Length)].Spawn(new Vector3(pos.x, pos.y, pos.z));
    }

   
}
