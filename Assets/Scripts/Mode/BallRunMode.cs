using UnityEngine;
using System.Collections;
using DG.Tweening;


public class BallRunMode : MonoBehaviour
{
    /// <summary>
    /// 小球运动模式,训练眼球
    /// </summary>

    private Vector2[] CornerPosArray;
   public RectTransform BallTransform;
    public RectTransform ParentRectTransform;
	// Use this for initialization
	void Start () {
        InitCornerPos();
       InvokeRepeating("BallMov",-1,2*MoveTime);
	}

    void InitCornerPos()
    {
        float parentWidth = ParentRectTransform.sizeDelta.x;
        float parentHeight = ParentRectTransform.sizeDelta.y;
        float ballWidth = BallTransform.sizeDelta.x;
        float ballHeight = BallTransform.sizeDelta.y;
        print(BallTransform.anchoredPosition);
        CornerPosArray = new[]
        {
            new Vector2(ballWidth/2, parentHeight-ballHeight/2), new Vector2(parentWidth-ballWidth/2, parentHeight-ballHeight/2),
            BallTransform.anchoredPosition, new Vector2(parentWidth-ballWidth/2, ballHeight/2),
            Vector2.zero
        };
        for (int i = 0; i < CornerPosArray.Length-1; i++)
        {
            Debug.DrawLine(CornerPosArray[i],CornerPosArray[i+1],Color.red);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}

    private int _currentIndex=0;
    public float MoveTime = 0.5f;
    int randomIndex;
    private float randomScale;//随机大小
    
    void BallMov()
    {
        
        while ((randomIndex=new System.Random().Next(CornerPosArray.Length))==_currentIndex)
        {
            //直到不为前面一个才会退出 
        }
        _currentIndex = randomIndex;
        randomScale = Random.Range(0.5f, 3f);
        DOTween.Sequence()
               .Append(BallTransform.DOAnchorPos(CornerPosArray[_currentIndex], MoveTime))
               .Insert(0, BallTransform.DOScale(Vector3.one*randomScale, MoveTime));


    }

  
}
