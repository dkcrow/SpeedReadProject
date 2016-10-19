using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour
{
    /// <summary>
    /// 以下两者必须按钮---面板  一一对应
    /// </summary>
    public Button[] m_panelBtns;
    public RectTransform[] m_panelTransforms;
	// Use this for initialization 
	void Start () {
	    for (int i = 0; i < m_panelBtns.Length; i++)
	    {
            GetPanelOrignPos(m_panelTransforms[i].anchoredPosition);
	        int tempIndex = i;
	        m_panelBtns[tempIndex].onClick.AddListener(() =>
	        {
                BackToOrignalPos();
	            m_panelTransforms[tempIndex].DOAnchorPos(Vector3.zero, 2);
	            _currentSelectPanelIndex = tempIndex;
	        });
	    }
	}

    private List<Vector2> panelOrignalPosList=new List<Vector2>();
    private int _currentSelectPanelIndex=-1;//当前所选择的界面
    void GetPanelOrignPos(Vector2 pos)
    {
        panelOrignalPosList.Add(pos);
        
    }

    void BackToOrignalPos()
    {
        if (_currentSelectPanelIndex >= 0)
        {
            m_panelTransforms[_currentSelectPanelIndex].DOAnchorPos(panelOrignalPosList[_currentSelectPanelIndex], 2);
        }
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
