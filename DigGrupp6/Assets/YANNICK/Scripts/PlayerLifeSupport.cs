using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;


public class PlayerLifeSupport : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float maxShield = 10f;

    public GameObject[] heartsArray;
    public GameObject[] shieldArray;
    
    [SerializeField] GameObject heart;
    [SerializeField] GameObject shield;

    [SerializeField] Canvas healthCanvas;
    [SerializeField] Transform heartsPos;
    [SerializeField] Transform shieldPos;

    [SerializeField, Range(0, 200)] float heartSpreadement;
    [SerializeField, Range(0, 200)] float shieldSpreadement;

    [SerializeField] Sprite[] heartImageArray;

    public bool ifHearts = false;
    public bool ifBar = true;

    //test
    int[] heartValueIndex;

    void Start()
    {
        if (ifHearts)
        {
            heartValueIndex = new int[heartsArray.Length];

            for (int i = 0; i < heartsArray.Length; i++)
            {
                heartsArray[i] = Instantiate(heart, heartsPos.transform.position, heartsPos.rotation, healthCanvas.transform);
                heartsArray[i].transform.localPosition = new Vector3(heartsPos.transform.localPosition.x + (i * heartSpreadement), heartsPos.transform.localPosition.y, 0);
                heartsArray[i].GetComponent<Image>().sprite = heartImageArray[0];
                heartValueIndex[i] = 2;
            }

            for (int i = 0; i < shieldArray.Length; i++)
            {
                shieldArray[i] = Instantiate(shield, shieldPos.transform.position, shieldPos.rotation ,healthCanvas.transform);
                shieldArray[i].transform.localPosition = new Vector3(shieldPos.transform.localPosition.x + i * shieldSpreadement, shieldPos.transform.localPosition.y, 0);
            }
        }
        else
        {
            maxHealth = 100f;
            maxShield = 10f;
        }
    }

    void Update()
    {
        if (maxHealth <= 0)
        {
            PlayerDeath();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float damageToTake)
    {
        if (ifBar)
        {
            maxShield -= damageToTake;

            if (maxShield <= 0)
            {
                maxHealth -= damageToTake;
            }

            maxShield = 0;
        }
        else if (ifHearts)
        {
            for (int i = heartValueIndex.Length; i > 0; i--)
            {
                if (heartValueIndex[i] > 0)
                {
                    heartValueIndex[i]--;
                    break;
                }
            }
        }
    }

    public void GainHealth(float healthToGain)
    {
        if (ifBar)
        {
            maxHealth += healthToGain;
        }
        else if (ifHearts)
        {

        }
    }

    public void GainShield(float shieldToGain)
    {
        if (ifBar)
        {
            maxShield += shieldToGain;
        }
        else if (ifHearts)
        {

        }
    }

    public void PlayerDeath()
    {
        // Play the death animation, die, respawn at the last checkpoint
    }
}
