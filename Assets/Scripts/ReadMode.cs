using UnityEngine;
using System.Collections;
using System.ComponentModel;
using DG.Tweening;
using UnityEngine.UI;


public class ReadMode : MonoBehaviour
{
    public Text MainText;
    public ReadType CurrentReadType=ReadType.Verticle;
    public int MovTime=1;//文本移动时间
    private Vector3[] _textPosArray=new Vector3[10];
    private RectTransform _textBgTransform;
    
    int[] crossQueue= {0,3,4,7,8,9,6,5,2,1};//交叉时的顺序
   public enum ReadType
    {   
        Horizantal,
        Verticle,
        Cross,
        Random
    }

   
	// Use this for initialization
	void Start () {
        Init();
        ReadTxt("test");
      BeginTextMov();


	}

    void Init()
    {
        InitPosArray();
        InitColor();
    }

    void InitColor()
    {
        ///todo:从本地读取保存的喜好颜色
        SetTextBGColor();
        SetTextColor();
    }
    private int currentReadIndex = 0;//当前读到哪里
    private string content;
    void ReadTxt(string txtName)
    {
        TextAsset textAsset = Instantiate(Resources.Load<TextAsset>(string.Format("article/{0}",txtName)));
        if (null == textAsset) return;//todo:这里得做容错处理:比如读取一个必定读的到的

        content = textAsset.text;
        currentReadIndex = 0;
    }
    void InitPosArray()
    {
        _textBgTransform = MainText.rectTransform.parent.GetComponent<RectTransform>();
        Vector2 orignalPos = _textBgTransform.localPosition;
        float mainTextWidth = _textBgTransform.sizeDelta.x;
        float mainTextHeight = _textBgTransform.sizeDelta.y;
        
        //print(mainTextWidth+" "+ mainTextHeight);
        for (int i = 0; i < _textPosArray.Length; i++)
        {
            _textPosArray[i] = orignalPos + new Vector2(mainTextWidth*(i%2), -mainTextHeight*(i/2)*2);//往下为负数 所以为减(非笛卡尔坐标系)
            //print(_textPosArray[i]);
        }
      
      
    }


    private Color currentBgColor = Color.green;//偏爱喜好 ,要保存
    private Color currentTextColor = Color.black;

    void SetTextBGColor()
    {
        _textBgTransform.GetComponent<Image>().color = currentBgColor;
    }

    void SetTextColor()
    {
        MainText.color = currentTextColor;
    }
    void ReSetAll()
    {
        content = "";
        currentReadIndex = 0;
        index = 0;
        currentBgColor = Color.white;
        currentTextColor = Color.black;
    }


    public void SetTextMovTime(int newTime)
    {
        MovTime = newTime;
        CancelInvoke("TextMov");
        InvokeRepeating("TextMov", 0, MovTime);
    }

    void BeginTextMov()
    {
        InvokeRepeating("TextMov", 0, MovTime);
    }



    private int index = 0;
    void TextMov()
    {
        switch (CurrentReadType)
        {
                case ReadType.Horizantal:

                if (index >= _textPosArray.Length) index = 0;

                _textBgTransform.localPosition = _textPosArray[index];
                ReadNext();
                index++;
                break;
            case ReadType.Verticle:

                if (index >= _textPosArray.Length)
                {
                    index = index % 2 == 0 ? 1 : 0;

                }
                _textBgTransform.localPosition = _textPosArray[index];
                ReadNext();
                index += 2;
                break;
            case ReadType.Cross:

                if (index >= _textPosArray.Length) index = 0;

                _textBgTransform.localPosition = _textPosArray[crossQueue[index]];
                ReadNext();
                index++;
                break;
            case ReadType.Random:

                index = Random.Range(0, 10);

                _textBgTransform.localPosition = _textPosArray[index];
                ReadNext();
                break;
        }
            
    }

    public int ReadLen = 10;//每次读入的字符长度
    /// <summary>
    /// 指针往下翻
    /// </summary>
    void ReadNext()
    {
        if (content.Length <= currentReadIndex )//超出 12>10
        {
           //todo:我也不知道啊啊
            ReadTxt("test");
            MainText.text = content.Substring(currentReadIndex, ReadLen);
        }

        else if (content.Length < currentReadIndex+ReadLen)//加上后超出6+5>10
        {
            MainText.text = content.Substring(currentReadIndex);//不够就直接读完
        }
        else//6<10
        {
            MainText.text = content.Substring(currentReadIndex, ReadLen);
        }
        

        currentReadIndex += ReadLen;
    }
    //void Play(int index)
    //{
    //    //_textBgTransform.do
    //   print(index);
    //    //_textBgTransform.DOLocalPath(_textPosArray, 10, PathType.Linear);
    //    ////_textBgTransform.do
    //    _textBgTransform.DOLocalMove(_textPosArray[index], 0.1f).OnComplete((() =>
    //    {
    //        if(index>=_textPosArray.Length-1)
    //            index =-1;

    //        Play(++index);   
    //    }));
    //}

	// Update is called once per frame
	void Update () {
	
	}
}
