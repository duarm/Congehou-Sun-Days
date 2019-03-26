﻿using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DamageEvent : UnityEvent<int>
{ }

public class Damageable : MonoBehaviour
{

    [Serializable]
    public class HealEvent : UnityEvent<int, Damageable>
    { }

    public int startingHealth = 5;
    public bool invulnerableAfterDamage = true;
    public float invulnerabilityDuration = 3f;
    public bool disableOnDeath = false;
    [Tooltip("An offset from the obejct position used to set from where the distance to the damager is computed")]
    public Vector2 centreOffset = new Vector2(0f, 1f);
    public DamageEvent OnHealthSet;
    public DamageEvent OnTakeDamage;
    public UnityEvent OnDie;
    public HealEvent OnGainHealth;

    protected bool m_Invulnerable;
    protected float m_InulnerabilityTimer;
    protected int m_CurrentHealth;
    protected bool m_ResetHealthOnSceneReload;

    public int CurrentHealth
    {
        get { return m_CurrentHealth; }
    }

    void OnEnable()
    {
        m_CurrentHealth = startingHealth;

        OnHealthSet.Invoke(m_CurrentHealth);

        DisableInvulnerability();
    }

    void Update()
    {
        if (m_Invulnerable)
        {
            m_InulnerabilityTimer -= Time.deltaTime;

            if (m_InulnerabilityTimer <= 0f)
            {
                m_Invulnerable = false;
            }
        }
    }

    public void EnableInvulnerability(bool ignoreTimer = false)
    {
        m_Invulnerable = true;
        //technically don't ignore timer, just set it to an insanly big number. Allow to avoid to add more test & special case.
        m_InulnerabilityTimer = ignoreTimer ? float.MaxValue : invulnerabilityDuration;
    }

    public void DisableInvulnerability()
    {
        m_Invulnerable = false;
    }

    public void TakeDamage(bool ignoreInvincible = false)
    {
        if ((m_Invulnerable && !ignoreInvincible) || m_CurrentHealth <= 0)
            return;

        m_CurrentHealth--;
        
        if (!m_Invulnerable)
        {
            OnHealthSet.Invoke(m_CurrentHealth);
        }

        OnTakeDamage.Invoke(m_CurrentHealth);

        if (m_CurrentHealth <= 0)
        {
            OnDie.Invoke();
            m_ResetHealthOnSceneReload = true;
            EnableInvulnerability();
            if (disableOnDeath) gameObject.SetActive(false);
        }
    }

    public void GainHealth(int amount)
    {
        m_CurrentHealth += amount;

        if (m_CurrentHealth > startingHealth)
            m_CurrentHealth = startingHealth;

        OnHealthSet.Invoke(m_CurrentHealth);

        OnGainHealth.Invoke(amount, this);
    }

    public void SetHealth(int amount)
    {
        m_CurrentHealth = amount;

        if (m_CurrentHealth <= 0)
        {
            OnDie.Invoke();
            m_ResetHealthOnSceneReload = true;
            EnableInvulnerability();
            if (disableOnDeath) gameObject.SetActive(false);
        }

        OnHealthSet.Invoke(m_CurrentHealth);
    }
}
