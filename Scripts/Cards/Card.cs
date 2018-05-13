using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    AttackCard,//攻击卡
    PrayCard,//祈祷卡
    ForbidCard,//反制卡
    FaliCard,//法力卡
    EquipmentCard,//装备卡
    WizardCard,//咒术卡
    ActionCard,//行动卡
}

public abstract class Card{
    public int Id { get; set; }
    public CardType CardType { get; set; }
    public int Consume { get; set; }
    public int Rank { get; set; }

    public abstract void CardFunction(RoleBase role);
}
