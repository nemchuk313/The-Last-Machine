using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private float maxHealth = 100f;

    private float _currentHealth = 0f;
    private bool _isAlive;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
        _isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float p_damage)
    {
        if(!_isAlive)
        {
            return;
        }

        if(_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            _isAlive = false;
            gameObject.SetActive(false);
        }

        //do damage
        _currentHealth -= p_damage;

        UpdateHealthBar();

    }

    private void UpdateHealthBar()
    {
        float calc_health = _currentHealth / maxHealth;
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(calc_health,0f,1f) , healthBar.transform.localScale.y , healthBar.transform.localScale.z);
    }

}
    