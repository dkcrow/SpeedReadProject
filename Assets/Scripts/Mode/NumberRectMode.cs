using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class NumberRectMode : MonoBehaviour
{

    public GameObject NumberRect;
    public RectTransform ParentRectTransform;
    private GameObject parent;//把创建的都放在

    List<Button>haveShownBtnList=new List<Button>();//已经显示出来的按钮
    List<GameObject> haveShownRectList=new List<GameObject>();//已经显示出来的方块
    private PoolManager<GameObject> rectPool = new PoolManager<GameObject>();
    RandomManager<int>randomManager =new RandomManager<int>();
    //public int CurrentLevel = 1;//level 1 3x3  最高7级
    // Use this for initialization
    void Start () {
        InitNumberRect();
	
	}

    private int maxPoolCount = 81;
    void InitNumberRect()
    {
        parent=new GameObject();
        parent.name = "NumberRectParent";
        rectPool.InitIndicatorPool(() =>CreateGameObj(NumberRect,parent.transform) , maxPoolCount,(o => o.GetComponent<RectTransform>().sizeDelta = Vector2.zero));
    }


    GameObject CreateGameObj(GameObject template,Transform parent)
    {
        GameObject go=Instantiate(NumberRect);
        go.transform.parent = parent;
        return go;
    }
    public void SetByLevel(int level)
    {
        Clear();

        level = Mathf.Clamp(level,1, 7);//容错检查
   
        int divisionNum = level + 2;//level1 3x3
        maxCount = divisionNum*divisionNum;

        List<int>numberList=new List<int>();
        for (int i = 0; i < maxCount; i++)
        {
            numberList.Add(i + 1); //0->1
        }
        int number = 0;
        int rectWidth = (int)(ParentRectTransform.sizeDelta.x/ divisionNum);

        for (int i = 0; i < divisionNum; i++)
        {
            for (int j = 0; j < divisionNum; j++)
            {
                number = randomManager.GetRandomSpawn(numberList, true);
                InitRect(i,j,number,rectWidth);
               
            }

        }
           
    }

    private int currentSelectLevel = 1;//当前点击到第几个
    private int maxCount = 0;//最大的那个数
   
    void Clear()
    {
        currentSelectLevel = 1;
        maxCount = 0;
        rectPool.RecoverAll(haveShownRectList,(Destroy),(o => o.GetComponent<RectTransform>().sizeDelta=Vector2.zero));
        haveShownRectList.Clear();

        foreach (Button btn in haveShownBtnList)
        {
            btn.interactable = true;
        }
        haveShownBtnList.Clear();
        randomManager.Clear();
    }
    void InitRect(int i,int j,int number,int rectWidth)
    {
        GameObject rect = rectPool.GetFromPools(() => Instantiate(NumberRect), o => o.GetComponent<RectTransform>().sizeDelta = Vector2.one);
      
        RectTransform rectTransform = rect.GetComponent<RectTransform>();
        rectTransform.SetParent(ParentRectTransform);

        rectTransform.localScale = Vector3.one;
        rectTransform.sizeDelta=Vector2.one*rectWidth;
        rectTransform.anchoredPosition=new Vector2(j* rectWidth+ rectWidth/2, -i*rectWidth - rectWidth / 2);//先行再列的显示,所以需要j在前面
        


        Text childText = rect.GetComponentInChildren<Text>();
        childText.fontSize = (int)(rectWidth*0.8f);//字体比边框小10
        childText.text = number.ToString();

        Button rectBtn = rect.GetComponent<Button>();
        rectTransform.DOKill(rectTransform);
        rectTransform.DOShakeScale(0.5f);
        haveShownBtnList.Add(rectBtn);
        haveShownRectList.Add(rect);

        rectBtn.onClick.RemoveAllListeners();
        rectBtn.onClick.AddListener(() =>
        {
            if(currentSelectLevel!=number)return;
            rectBtn.interactable = false;

            if (currentSelectLevel>=maxCount) print("通关");
            currentSelectLevel++;
           
        });
    }

    private int temp = 1;
	// Update is called once per frame
	void Update () {
        
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
            SetByLevel(temp++);
	   
	    }
	}
}
