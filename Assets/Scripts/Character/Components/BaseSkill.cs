using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public SkillData SkillData { get=>_skillData; }
    [SerializeField]
    protected SkillData _skillData=new();

    private void Awake()
    {
        
        
    }
    private void Start()
    {
        
        
        Debug.Log("init damage:"+_skillData.DefaultDamage);
        SubStart();
    }
    protected virtual void SubStart()
    {

    }
    public void Init()
    {
        _skillData.Initialize();
    }


}
