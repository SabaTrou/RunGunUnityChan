using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField]
    private PlayerLifeBar _lifeBar;
    public PlayerLifeBar LifeBar { get=> _lifeBar; }
    [SerializeField]
    private SkillIcon _mainSkillIcon;
    public SkillIcon MainIcon { get => _mainSkillIcon; }
    [SerializeField]
    private SkillIcon _subSkillIcon;
    public SkillIcon SubIcon { get => _subSkillIcon; }
}
