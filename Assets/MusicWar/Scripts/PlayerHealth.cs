﻿using UnityEngine;
using UniRx;

public class PlayerHealth : MonoBehaviour ,IDamageAppliable
{
    public IReadOnlyReactiveProperty<float> HP { get { return _hp; } }

    private ReactiveProperty<float> _hp = new ReactiveProperty<float>();

    private IStateProvider _state;

    public void Initialize()
    {
        _hp.Value = 100;
        _state = this.GetComponent<IStateProvider>();
        _hp.Where(hp => hp <= 0)
           .Subscribe(hp => _state.ToDead());
    }

    public void ApplyDamage(Damage damage)
    {
        _hp.Value -= damage.Value;
    }
}
