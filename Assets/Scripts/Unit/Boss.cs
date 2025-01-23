using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit
{
    bool _summon;
    bool _turnTwice;

    public bool summon
    {
        get
        {
            return _summon;
        }
        set
        {
            _summon = value;
        }
    }

    public bool turnTwice
    {
        get
        {
            return _turnTwice;
        }
        set
        {
            _turnTwice = value;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _summon = true;
        _turnTwice = true;
        movable = false;
    }


    public override void ResetAble()
    {
        base.ResetAble();
        movable = false;
        move_Re = false;
    }
}
