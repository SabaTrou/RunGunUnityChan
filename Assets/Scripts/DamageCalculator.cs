using System;

public static class DamageCalculator
{
    public static void CalculateDamage(this BaseEnemyCharacter enemyCharacter,int damage)
    {
        enemyCharacter.Status.HitPoint-=damage;
    }
    public static void CalculateDamage(this BasePlayerCharacter playerCharacter,int damage)
    {
        playerCharacter.Status.HitPoint-=damage;
    }
}