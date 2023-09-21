using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransferRing : MonoBehaviour
{
    [SerializeField]private GameObject buttonPanel1,buttonPanel2,buttonPanel3;
    [SerializeField]private GameObject errorMessage;
    [SerializeField]private Transform StackA,StackB,StackC;
    Transform fir,sec; //ring removed in sec and ring added in fir
    [SerializeField]private Transform startStack;
    
    List<Node> instructions;
    static int index;


    public static bool game;
    private bool error;
    private bool ButPanel;
    private bool sim;
    
    void Start()
    {   
        index=0;
        instructions= new List<Node>();
        fir=sec=startStack;
        error=false;
        game=false;
        if(SceneManager.GetActiveScene().buildIndex==1)
            sim=false;
        else if(SceneManager.GetActiveScene().buildIndex==2)
        {
            sim=true;
            AutoTransferArray(UIController.getRings(),StackA,StackB,StackC);
            InvokeRepeating(nameof(Simulation),1f,1f);
        }
        ButPanel=true;
        buttonPanel1.SetActive(false);
        buttonPanel2.SetActive(false);
        buttonPanel3.SetActive(false);
        
    }

    private void Simulation()
    {   
        if (game)
        {
            if (index<instructions.Count)
            {
                AddRing(RemoveRing(instructions[index].getStart()),instructions[index].getEnd());
                index++;
            }
        }
    }

    void FixedUpdate()
    {
        errorMessage.SetActive(error);
        TogglePanel();
    }

    private void Transfer()
    {   
        if((sec.childCount>0 && fir.childCount>0 && sec.GetChild(0).localScale.x>fir.GetChild(0).localScale.x))
        {   
            error=true;
        }
        else if (ButPanel && sec!=fir && sec!=fir &&sec.childCount>0)
        {   
            AddRing(RemoveRing(sec), fir);
        }
        else if(sec.childCount<=0||sec==fir)
        {
            error=true;
        }
        else
        {   
            error = true;
        }
        
    }
    private void TogglePanel()
    {
        if (game)
        {
            buttonPanel1.SetActive(ButPanel&& (!sim));
            buttonPanel2.SetActive(!ButPanel&& (!sim));
            buttonPanel3.SetActive(true);
        }
    }

    public void addButtonA()
    {   
        fir = StackA;
        ButPanel=!ButPanel;
        Transfer();
    }
    public void addButtonB()
    {   
        fir = StackB;
        ButPanel=!ButPanel;
        Transfer();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void addButtonC()
    {   
        fir = StackC;
        ButPanel=!ButPanel;
        Transfer();
    }
    public void remButtonA()
    {   
        sec = StackA;
        ButPanel=!ButPanel;
    }
    public void remButtonB()
    {   
        sec = StackB;
        ButPanel=!ButPanel;
    }
    public void remButtonC()
    {
        sec = StackC;
        ButPanel=!ButPanel;
    }

    public void reset()
    {
        SceneManager.LoadScene(1);
    }

    public void SimulationButton()
    {
        SceneManager.LoadScene(2);
        
    }

    private GameObject RemoveRing(Transform A) //Would be used as pop
    {
        if(A.childCount==0)
        {   
            error=true;
            return null;
        }
        GameObject popped = A.GetChild(0).gameObject;
        Destroy(A.GetChild(0).gameObject);
        return popped;
    }

    private void AddRing(GameObject obj, Transform B) //Would Be used as a append
    {   
        if(B.childCount>0&&obj.transform.localScale.x>B.GetChild(0).localScale.x)
        {   
            error=true;
            return;
        }
        error=false;
        Vector3 pos = new Vector3(B.position.x,B.position.y+3,B.position.z);
        GameObject inst = Instantiate(obj,pos,Quaternion.identity,B);
        inst.transform.SetSiblingIndex(0);
    }
    
    
    
    void AutoTransferArray(int n,Transform start, Transform mid, Transform end)//Used to provide the instructions to solve the puzzle
    {   
        if(n>2)
        {   
            AutoTransferArray(n-1, start, end, mid); //Here we transfer shit from start->mid block with end as the intermediate
            instructions.Add(new Node(start,end));
            AutoTransferArray(n-1,mid, start, end); // Here we transfer shit from mid->end block with starting block/ori block as the intermediary
        }
        else if(n==2)
        {
            instructions.Add(new Node(start,mid));
            instructions.Add(new Node(start,end));
            instructions.Add(new Node(mid,end));
        }
    }


}
