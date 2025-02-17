using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCalculator : MonoBehaviour
{
    public double CalculAccuracy(Unit attacker, Unit target)
    {
        return 888.888 /
            (888.888 +
            (((target[EStatType.LV] * 10) + target[EStatType.Avoid]) - ((attacker[EStatType.LV] * 10) + attacker[EStatType.Accuracy])));
    }

    public double CalculCritical(Unit attacker, Unit target)
    {
        return 0.4 * (99.99 / (99.99 + (target[EStatType.LV] * 10) - attacker[EStatType.LV] * 10));
    }

    public bool CheckAccuracy(Unit attacker, Unit target)
    {
        float randomValue = Random.value;
        double accuracy = 888.888 /
            (888.888 + 
            (((target[EStatType.LV] * 10) + target[EStatType.Avoid]) -((attacker[EStatType.LV] * 10) + attacker[EStatType.Accuracy])));

        Debug.Log($"{randomValue},,,, {accuracy}");
        if (randomValue <= accuracy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckCritical(Unit attacker, Unit target)
    {
        float randomValue = Random.value;

        double critical = 0.4 * (99.99 / (99.99 + (target[EStatType.LV] * 10) - attacker[EStatType.LV] * 10));
        Debug.Log($"{randomValue},,,, {critical}");
        if (randomValue <= critical)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
