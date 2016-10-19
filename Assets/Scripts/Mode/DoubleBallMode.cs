using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class DoubleBallMode : MonoBehaviour
{

    public GameObject BaseNumGameObject;
    public Transform ParentTransform;


	// Use this for initialization
	void Start () {
        InitTxt();
	  InitNumRect();
	}


    List<int> BlueList=new List<int>();
    List<int> RedList=new List<int>();  

    private int RedNum = 25;
    private int BlueNum = 12;

    void InitNumRect()
    {
        for (int i = 0; i < RedNum; i++)
        {
            CreateNum(Color.red, i + 1);
        }

        for (int i = 0; i < BlueNum; i++)
        {
            CreateNum(Color.blue, i + 1);
        }
    }


    void InitTxt()
    {
        string[] result=TxtReadManager.GetInstance().ReadTxt("ballInfo", false, "BallSaver").Split(' ');
        
        if (result.Length != RedNum+BlueNum)
        {
            Debug.LogError("读取失败"+result.Length);
            return;
        }
        for (int i = 0; i < RedNum; i++)
        {
            RedList.Add(int.Parse(result[i]));
        }
        for (int i = 0; i < BlueNum; i++)
        {
            BlueList.Add(int.Parse(result[RedNum + i]));
        }

    }


    void SaveTxt()
    {
        
    }

    private int redIndex = 0;
    private int blueIndex =0;
    GameObject CreateNum(Color color,int num)
    {
        GameObject numGo=Instantiate(BaseNumGameObject,ParentTransform) as GameObject;
        
        if (null == numGo) return null;
        numGo.transform.localScale=Vector3.one;
        foreach (Image bg in numGo.GetComponentsInChildren<Image>())
        {
            bg.color = color;
        }

        Text[] txtArray = numGo.GetComponentsInChildren<Text>();
        txtArray[0].text= num.ToString();

    
        txtArray[1].text = color == Color.red ? RedList[redIndex++].ToString() : BlueList[blueIndex++].ToString();
      
        //foreach (Text text in numGo.GetComponentsInChildren<Text>())
        //{
        //    text.text = num.ToString();
        //}

        return numGo;
    }


    
    
	 //Update is called once per frame
	void Update () {
	
	}

   
}


