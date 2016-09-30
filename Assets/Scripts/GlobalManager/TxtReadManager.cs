using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class TxtReadManager : SingltonTemplate<TxtReadManager>
{
    /// <summary>
    /// 所有的txt名称
    /// </summary>
    public List<string> TxtNameList = new List<string>
    {
        "test","test2"
    };

    private string content;
    /// <summary>
    /// 文章全部为txt文件,且放在Resources/article下
    /// </summary>
    /// <param name="txtName"></param>
    /// <returns></returns>
    private void ReadTxt(string txtName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(string.Format("article/{0}", txtName));
        if (null == textAsset) content=""; //todo:这里得做容错处理:比如读取一个必定读的到的

        content=textAsset.text;
    }


    public void BeginRead()
    {
        ReadTxt(GetNextTxtName());
    }

    private int ReadLen = 20; //每次读入的字符长度
    private int currentReadIndex = 0;//当前读到哪里

    public void ReadNext()
    {
        string result;
        if (content.Length <= currentReadIndex)//超出 12>10
        {
             BeginRead();
             result = content.Substring(currentReadIndex, ReadLen);
        }

        else if (content.Length < currentReadIndex + ReadLen)//加上后超出6+5>10
        {
            result= content.Substring(currentReadIndex);//不够就直接读完
        }
        else//6<10
        {
            result= content.Substring(currentReadIndex, ReadLen);
        }

        currentReadIndex += ReadLen;
        
        EventDispatcher.GetInstance().DisPatchEvent(DispatchData.EventNames.READ_NEXT,result);
    }

    public void SetReadLen(int len)
    {
        ReadLen = Mathf.Clamp(len, 1, 100);
    }

    private int currentTxtIndex = 0;
    private string GetNextTxtName()
    {
        if (currentReadIndex >= TxtNameList.Count)
        {
            currentReadIndex = 0;
        }

        return TxtNameList[currentReadIndex++];
    }
}
