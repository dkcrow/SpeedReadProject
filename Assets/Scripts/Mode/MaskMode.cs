using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class MaskMode : MonoBehaviour
{
    public enum MaskType
    {
        HorizantalType,//横向
        VerticalType,//纵向
        ExplandType//横纵同时
    }

    public RectTransform MaskPanelTextBG;
    public RectTransform MaskTextBG;
    public Text MaskText;
    public MaskType CurrentMaskType;
    private float _orignalHeight;
    private float _orignalWidth;
    private int maxWidthInHorizantalMode=700;
    private int maxWidthInExplandMode=400;
    private int maxHeightInExplandMode = 400;
    private int maxHeight;
    private float currentWidth;
    private float currentHeight;
    private int deltaWidth=2;
    private int deltaHeight=0;
    // Use this for initialization
    void Start ()
    {
        currentWidth=_orignalWidth = MaskPanelTextBG.sizeDelta.x;
        currentHeight=_orignalHeight = MaskPanelTextBG.sizeDelta.y;

        TxtReadManager.GetInstance().BeginRead();

        EventDispatcher.GetInstance().RegisterEvent(OnGetMsg);
        ChangeMaskType(MaskType.HorizantalType);


        InvokeRepeating("ReadNext", 0,1);
    }

    void OnGetMsg(object sender, DispatchData data)
    {
        switch (data.EventName)
        {
            case DispatchData.EventNames.READ_NEXT:
                UpdateExpland();
                MaskText.text = data.Data.ToString();             
                break;
               
        }
        
    }

    void ChangeMaskType(MaskType maskType)
    {
        switch (maskType)
        {
                case MaskType.HorizantalType:
                deltaWidth = 20;
                deltaHeight = 0;
                break;
            //    case MaskType.ExplandType://todo:效果不行
            //    deltaWidth = 20;
            //    deltaHeight = 20;
            //    break;
            //case MaskType.VerticalType:
            //    deltaWidth = 20;
            //    deltaHeight = 20;
            //    break;
        }
        CurrentMaskType = maskType;
        ResetSize();
    }

    
   
   
    void ResetSize()
    {
        MaskPanelTextBG.sizeDelta=new Vector2(_orignalWidth,_orignalHeight);
    }

    
    void ReadNext()
    {
        TxtReadManager.GetInstance().ReadNext((int)(currentWidth/Mathf.Abs(deltaWidth))-2);
    }
    void UpdateExpland()
    {
        switch (CurrentMaskType)
        {
          case MaskType.HorizantalType:
                if (currentWidth > maxWidthInHorizantalMode||currentWidth< _orignalWidth)
                    deltaWidth = -deltaWidth;                   
                    break;
          //case MaskType.ExplandType:
          //      if (currentWidth > maxWidthInExplandMode || currentWidth < 0)
          //          deltaWidth = -deltaWidth;
          //      if (currentWidth > maxHeightInExplandMode || currentHeight < 0)
          //          deltaHeight = -deltaHeight;
          //      if (currentWidth < 0)
          //          TxtReadManager.GetInstance().ReadNext();
          //      break;
        }

        currentWidth += deltaWidth;
        currentHeight += deltaHeight;
        MaskTextBG.sizeDelta=MaskPanelTextBG.sizeDelta=new Vector2(currentWidth,currentHeight);
        


    }

    void OnDestroy()
    {
        UnRegisterEvent();
    }

    void UnRegisterEvent()
    {
        EventDispatcher.GetInstance().UnRegisterEvent((OnGetMsg));
    }

  
}
