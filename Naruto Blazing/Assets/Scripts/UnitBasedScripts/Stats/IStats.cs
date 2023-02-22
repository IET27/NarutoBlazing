using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats{
    int getAttackDamage();
    int getJutsuDamage();
    int getHealth();
    void decreaseHealth(int damage);
}