using UnityEngine;
using System.Collections;
using  System.Xml;
public class test : MonoBehaviour
{
    private int gg = 10;
    public delegate  void Mfunc(int a,int b);
	// Use this for initialization
	void Start () {
	 //DoSth(OnGet);
     test2.DoSth(OnGet);
	}
	
	// Update is called once per frame
	void Update () {
     
	}

    void DoSth(Mfunc x)
    {
        x(3, 4);
        

    }
    void OnGet(int a ,int c)
    {
        print(gg*a*c);
    }
}
