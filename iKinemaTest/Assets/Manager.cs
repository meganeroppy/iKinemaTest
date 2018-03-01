using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private Animator[] animList;

    private int[] currentAnimIndexList;
    private const int animIndexMax = 8;

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
        currentAnimIndexList = new int[animList.Length];
        for( int i=0; i < currentAnimIndexList.Length; ++i )
        {
            currentAnimIndexList[i] = 0;
        }
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
    }

    public void ChangeTarget(bool forward)
    {
        if (forward)
        {
            CurrentTarget = (CurrentTarget + 1) % animList.Length;
        }
        else
        {
            CurrentTarget = (CurrentTarget + animList.Length - 1) % animList.Length;
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

        animList[CurrentTarget].SetTrigger(animName);

        Debug.Log("ターゲット = " + CurrentTarget.ToString() + "にSetTrigger : " + animName);

    }
}
