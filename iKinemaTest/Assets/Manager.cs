using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    enum ModelType
    {
        Kyle,
        Male
    }

    ModelType currentModelType;

    [SerializeField]
    private Animator[] animListKyle;

    [SerializeField]
    private Animator[] animListMale;

    private int[] currentAnimIndexList;
    private const int animIndexMax = 3;

    public int CurrentTarget { get; private set; }

    [SerializeField]
    private string[] animNames;

    [SerializeField]
    private Text[] animNameTexts;

    [SerializeField]
    private GameObject[] arrows;
    
	// Use this for initialization
	void Start ()
    {
        CurrentTarget = 0;
        currentAnimIndexList = new int[animListKyle.Length];
        for( int i=0; i < currentAnimIndexList.Length; ++i )
        {
            currentAnimIndexList[i] = 0;
        }

        currentModelType = ModelType.Kyle;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckInput();

        UpdateUI();
	}

    private void UpdateUI()
    {
        for (int i = 0; i < arrows.Length; ++i)
        {
            arrows[i].SetActive(i == CurrentTarget);
        }

        for (int i = 0; i < animNameTexts.Length; ++i)
        {
            animNameTexts[i].text = animNames[currentAnimIndexList[i]];
        }

    }

    private void CheckInput()
    {
        // 左右でターゲット切り替え
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeTarget(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeTarget(false);
        }

        // 上下でアニメーション切り替え
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeAnim(true);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeAnim(false);
        }

        // スペースで表示モデル切り替え
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchModel();
        }
    }

    public void ChangeTarget(bool forward)
    {
        if (forward)
        {
            CurrentTarget = (CurrentTarget + 1) % animListKyle.Length;
        }
        else
        {
            CurrentTarget = (CurrentTarget + animListKyle.Length - 1) % animListKyle.Length;
        }

        Debug.Log("現在のターゲット = " + CurrentTarget.ToString());
    }

    private void ChangeAnim(bool forward)
    {
        if (forward)
        {
            currentAnimIndexList[ CurrentTarget ] = (currentAnimIndexList[CurrentTarget] + 1) % animIndexMax;
        }
        else
        {
            currentAnimIndexList[CurrentTarget] = (currentAnimIndexList[CurrentTarget] + animIndexMax - 1) % animIndexMax;
        }

        ExecAnim();
    }

    private void ExecAnim()
    {
        var animName = "Motion" + currentAnimIndexList[CurrentTarget].ToString();

        animListKyle[CurrentTarget].SetTrigger(animName);
        animListMale[CurrentTarget].SetTrigger(animName);

        Debug.Log("ターゲット = " + CurrentTarget.ToString() + "にSetTrigger : " + animName);

    }

    /// <summary>
    /// 表示モデルを切り替え
    /// </summary>
    private void SwitchModel()
    {
        currentModelType = currentModelType == ModelType.Kyle ? ModelType.Male : ModelType.Kyle;

        foreach( Animator a in animListKyle )
        {
            var mesh = a.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach( SkinnedMeshRenderer m in mesh)
            {
                m.enabled = currentModelType == ModelType.Kyle;
            }
        }

        foreach (Animator a in animListMale)
        {
            var mesh = a.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer m in mesh)
            {
                m.enabled = currentModelType == ModelType.Male;
            }
        }
    }
}
