using UnityEngine;

public class EyeExplandMode : MonoBehaviour
{

    public RectTransform[] Arrows;
    public RectTransform center;//用于旋转
    private float[] _arrowRotationX;
    private float[] _arrowRotationY;
    private float currentWidth;

    private  float _orignWidth ;
    private  float _orignHeight;
    private int maxWidth = 350;//箭头的最大长度
    private int deltaWidth = 2;//每帧增减的长度量
    private int centerRotation = 1;//每一帧中心旋转的角度
	// Use this for initialization
	void Start ()
	{
	    _orignWidth = Arrows[0].sizeDelta.x;
        _orignHeight = Arrows[0].sizeDelta.y;
	    currentWidth = _orignWidth;
        GetArrowsRotation();
	}

    /// <summary>
    /// 为了延伸长度必须获取每个箭头的旋转的sin /cos值 而不必在update里每次都重复计算了
    /// </summary>
    void GetArrowsRotation()
    {
        _arrowRotationX=new float[Arrows.Length];
        _arrowRotationY = new float[Arrows.Length];
        for (int i = 0; i < Arrows.Length; i++)
        {
           
            float angle=Arrows[i].eulerAngles.z;
            float rad = angle*Mathf.Deg2Rad;
            _arrowRotationX[i] = Mathf.Cos(rad);
            _arrowRotationY[i] = Mathf.Sin(rad);
        }
    }
	// Update is called once per frame
	void Update ()
	{

        if (currentWidth > maxWidth || currentWidth < _orignWidth)
            deltaWidth = -deltaWidth;

	    currentWidth += deltaWidth;
	    for (int i = 0; i < Arrows.Length; i++)
	    {
            Arrows[i].sizeDelta = new Vector2(currentWidth, _orignHeight);
            Arrows[i].anchoredPosition = new Vector2(_arrowRotationX[i]*currentWidth/2,_arrowRotationY[i]* currentWidth/2);
        }

        center.Rotate(0,0,centerRotation);

    }

  
    
}
