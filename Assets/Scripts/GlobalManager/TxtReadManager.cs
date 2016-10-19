using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    /// <summary>
    /// 文章内容
    /// </summary>
    private string content;
    /// <summary>
    /// txt根目录
    /// </summary>
    private string txtFilePath = "article";
    private string txtFileName = "article";
    /// <summary>
    /// 设置txt文件根目录
    /// </summary>
    private void SetTxtPath(string url,string name)
    {
        txtFilePath = url;
        txtFileName = name;
    }
    /// <summary>
    /// 文章全部为txt文件,且放在Resources/article下
    /// </summary>
    /// <param name="txtName"></param>
    /// <param name="needDelSpace">是否需要去掉空格回车</param>
    /// <returns></returns>
    public string ReadTxt(string txtName,bool needDelSpace=true,string txtPath="article")
    {
        SetTxtPath(txtPath,txtName);

        TextAsset textAsset = Resources.Load<TextAsset>(string.Format(txtFilePath+"/{0}", txtFileName));
        if (null == textAsset) content= TxtNameList[0]; //todo:这里得做容错处理:比如读取一个必定读的到的或提示读取失败

        if (needDelSpace)
            content = textAsset.text.Replace(" ", "").Replace("\r", "").Replace("\n", ""); ///去除所有空格回车换行
        else
            content = textAsset.text;
        return content;
    }


    public void BeginRead()
    {
        ReadTxt(GetNextTxtName());
    }

    private int ReadLen = 20; //每次读入的字符长度
    private int currentReadIndex = 0;//当前读到哪里

    public void ReadNext(int readLen=20)
    {
        SetReadLen(readLen);
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


    /// <summary>
    /// 写入txt文件中,写入路径默认和读取时的一样
    /// </summary>
    /// <param name="txtName">txt名字</param>
    /// <param name="txtContent">内容</param>
    public void WriteTxt( string txtContent)
    {
        File.WriteAllText(string.Format(txtFilePath + "/{0}", txtFileName), txtContent);
    }
}
