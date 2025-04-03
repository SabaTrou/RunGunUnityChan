using System;
using UnityEngine;

public class ControllerAddEvent
{

    public readonly PlayerController controller;
    public ControllerAddEvent(PlayerController controller)
    {
        this.controller = controller;
    }

}
public class PlayerCharacterAddEvent
{
    public readonly BasePlayerCharacter character;
    public PlayerCharacterAddEvent(BasePlayerCharacter character)
    {
        this.character = character;
    }
}
public class EnemyCharacterAddEvent
{
    public readonly BaseEnemyCharacter character;
    public EnemyCharacterAddEvent(BaseEnemyCharacter character)
    {
        this.character = character;
    }



}


public class CollisionableAddEvent
{
    public readonly ICollisionable2D collisionable;
    public CollisionableAddEvent(ICollisionable2D collisionable)
    {
        this.collisionable = collisionable;
    }
}
public class SkillAddEvent
{
    public readonly SkillData skillData;
    public readonly Skillcategory skillCategory;
    public SkillAddEvent(SkillData skillData, Skillcategory skillCategory)
    {
        this.skillData = skillData;
        this.skillCategory = skillCategory;
    }
    public enum Skillcategory
    {
        main,
        sub,
        guard,
        s
    }
}

    public class CharacterCreateRequest
    {
        public readonly BasePlayerCharacter character;
        public readonly Vector3 position;
        public readonly Quaternion rotation;
        public readonly Transform parent;
        public CharacterCreateRequest(BasePlayerCharacter character, Vector3 position, Quaternion rotation, Transform parent)
        {
            this.character = character;
            this.position = position;
            this.rotation = rotation;
            this.parent = parent;
        }
    }
