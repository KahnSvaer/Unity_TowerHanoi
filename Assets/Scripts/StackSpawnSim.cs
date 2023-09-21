using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSpawnSim : MonoBehaviour
{   
    private int rings;
    [SerializeField]Material mat1;
    [SerializeField]Material mat2;
    [SerializeField]private GameObject ringPrefab;

    private Vector3 pos;
    [SerializeField]private Transform stackA;
    private void Start()
    {   
        rings = UIController.getRings();
        pos = new Vector3(stackA.position.x,stackA.position.y+3,stackA.position.z);
        InvokeRepeating(nameof(spawnRings),0,.2f);
    }

    private void spawnRings()
    {
        if(rings >0)
        {   
            GameObject inst = Instantiate(ringPrefab,pos,Quaternion.identity,stackA);
            if (rings%2==0)
            {
                inst.GetComponent<Renderer>().material=mat1;
            }
            else
            {
                inst.GetComponent<Renderer>().material=mat2;
            }
            inst.transform.SetSiblingIndex(0);
            inst.name = rings+" Ring";
            inst.transform.localScale=new Vector3(rings*0.2f+0.5f,ringPrefab.transform.localScale.y,rings*0.2f+0.5f);
            rings--;
        }
        else
        {
            TransferRing.game=true;
        }

    }
}
