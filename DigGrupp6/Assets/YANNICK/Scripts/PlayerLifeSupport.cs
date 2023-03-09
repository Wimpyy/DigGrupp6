using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeSupport : MonoBehaviour
{
    #region Sustainance Values
    [SerializeField] int maxHealth = 1000;
    [SerializeField] int currentHealth;
    [SerializeField] int maxShield = 500;
    [SerializeField] int currentShield;
    #endregion

    #region Object Arrays
    public GameObject[] heartsArray;
    public GameObject[] shieldArray;
    #endregion

    #region UI Element Objects
    [SerializeField] GameObject heart;
    [SerializeField] GameObject shield;
    #endregion

    #region UI Element Position
    [SerializeField] Canvas healthCanvas;
    [SerializeField] Transform heartsPos;
    [SerializeField] Transform shieldPos;
    [SerializeField, Range(0, 200)] float heartSpreadement;
    [SerializeField, Range(0, 200)] float shieldSpreadement;
    #endregion

    #region Image Arrays
    [SerializeField] Sprite[] heartImageArray;
    [SerializeField] Sprite[] shieldImageArray;
    #endregion

    #region UI Animations
    [SerializeField] AnimationClip takeDamageHeartAnim;
    [SerializeField] AnimationClip takeDamageShieldAnim;
    #endregion

    #region Booleans
    public bool ifHearts = false;
    public bool ifBar = true;
    public bool overShieldActive = true;
    public bool fullHealth;
    #endregion

    #region Value Indecies
    [SerializeField] int[] heartValueIndex;
    [SerializeField] int[] shieldValueIndex;
    #endregion

    void Start()
    {
        if (ifHearts)
        {
            SetUpHearts();
        }
        else
        {
            currentHealth = maxHealth;
            currentShield = maxShield;
        }
    }

    private void SetUpHearts()
    {
        heartValueIndex = new int[heartsArray.Length];

        for (int i = 0; i < heartsArray.Length; i++)
        {
            heartsArray[i] = Instantiate(heart, heartsPos.transform.position, heartsPos.rotation, healthCanvas.transform);
            heartsArray[i].transform.localPosition = new Vector3(heartsPos.transform.localPosition.x + (i * heartSpreadement), heartsPos.transform.localPosition.y, 0);
            heartsArray[i].GetComponent<Image>().sprite = heartImageArray[0];
            heartValueIndex[i] = 1;
            fullHealth = true;
        }

        shieldValueIndex = new int[shieldArray.Length];

        for (int i = 0; i < shieldArray.Length; i++)
        {
            shieldArray[i] = Instantiate(shield, shieldPos.transform.position, shieldPos.rotation, healthCanvas.transform);
            shieldArray[i].transform.localPosition = new Vector3(shieldPos.transform.localPosition.x + i * shieldSpreadement, shieldPos.transform.localPosition.y, 0);
            shieldArray[i].GetComponent<Image>().sprite = shieldImageArray[0];
            shieldValueIndex[i] = 1;
        }
    }

    void Update()
    {
        if (maxHealth <= 0)
        {
            PlayerDeath();
        }

        HandleDebugInputs();
    }

    private void HandleDebugInputs()
    {
        if (Input.GetKeyDown(KeyCode.K)) // For debugging
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.H)) // For debugging
        {
            GainHealth(1);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            GainShield(1);
        }
    }

    #region TakeDamageFunctions
    public void TakeDamage(int damageToTake)
    {
        #region Bar Mode Health System
        if (ifBar)
        {
            if (currentShield < 0)
            {
                currentShield = 0;
            }
            else if (currentShield > 0)
            {
                int shieldDamage = Mathf.Min(currentShield, damageToTake);
                int healthDamage = Mathf.Min(currentHealth, damageToTake - shieldDamage);
            }
        }
        #endregion
        #region Heart Mode Health System
        else if (ifHearts && !overShieldActive)
        {
            TakeDamageHealth();
        }
        #region Shield Stuff
        else if (ifHearts && overShieldActive)
        {
            TakeDamageShield();
            #endregion
            #endregion
        }
    }

    private void TakeDamageHealth()
    {
        for (int i = heartValueIndex.Length - 1; i >= 0; i--)
        {
            Debug.Log("Yo");
            if (heartValueIndex[i] >= 0)
            {
                heartValueIndex[i]--;
                fullHealth = false;
                foreach (int iHeart in heartValueIndex)
                {
                    if (heartValueIndex[i] == 0)
                    {
                        heartsArray[i].GetComponent<Image>().sprite = heartImageArray[1];
                    }
                    else if (heartValueIndex[i] == -1)
                    {
                        heartsArray[i].GetComponent<Image>().sprite = heartImageArray[2];
                    }
                    Debug.Log("array current value is " + heartValueIndex[i]);
                }
                StartCoroutine(DamageThrob(heartsArray[i].gameObject));
                break;
            }
            if (heartValueIndex[0] == -1)
            {
                heartsArray[0].GetComponent<Image>().sprite = heartImageArray[2];
                PlayerDeath();
            }
        }
    }

    private void TakeDamageShield()
    {
        for (int i = shieldValueIndex.Length - 1; i == 1; i -= 1)
        {
            if (shieldValueIndex[0] == 1 || shieldValueIndex[1] == 1)
            {
                if (shieldValueIndex[1] <= 0)
                {
                    shieldValueIndex[0]--;
                    overShieldActive = false;
                }
                shieldValueIndex[1]--;
                if (shieldValueIndex[1] < 0)
                {
                    shieldValueIndex[1] = 0;
                }
                foreach (int iShield in shieldValueIndex)
                {
                    if (shieldValueIndex[1] == 0)
                    {
                        shieldArray[1].GetComponent<Image>().sprite = shieldImageArray[1];
                    }

                    if (shieldValueIndex[0] == 0)
                    {
                        shieldArray[0].GetComponent<Image>().sprite = shieldImageArray[1];
                    }
                    StartCoroutine(DamageThrob(shieldArray[i].gameObject));
                    break;
                }
            }
        }
    }

    IEnumerator DamageThrob(GameObject currentIndicator)
    {
        if (!overShieldActive)
        {
            currentIndicator.GetComponent<Image>().color = Color.red;
            currentIndicator.GetComponent<Animator>().SetBool("HeartTakeDamageThrob", true);

            yield return new WaitForSecondsRealtime(0.3f);

            currentIndicator.GetComponent<Animator>().SetBool("HeartTakeDamageThrob", false);
            currentIndicator.GetComponent<Image>().color = Color.white;
        }
        else if (overShieldActive)
        {
            currentIndicator.GetComponent<Image>().color = Color.red;
            currentIndicator.GetComponent<Animator>().SetBool("ShieldDamageAnim", true);

            yield return new WaitForSecondsRealtime(0.5f);

            currentIndicator.GetComponent<Image>().color = Color.white;
            currentIndicator.GetComponent<Animator>().SetBool("ShieldDamageAnim", false);
        }
    }
    #endregion

    #region GainFunctions


    public void GainHealth(int healthToGain)
    {
        if (ifBar)
        {
            currentHealth += healthToGain;
        }
        else if (ifHearts)
        {
            for (int i = 0; i < 3; i++)
            {
                if (heartValueIndex[i] == -1)
                {
                    heartsArray[i].GetComponent<Image>().sprite = heartImageArray[1];
                    heartValueIndex[i]++;
                    StartCoroutine(HealThrob(heartsArray[i].gameObject));
                    return;
                }

                if (heartValueIndex[i] == 0)
                {
                    heartsArray[i].GetComponent<Image>().sprite = heartImageArray[0];
                    heartValueIndex[i]++;
                    StartCoroutine(HealThrob(heartsArray[i].gameObject));
                    if (heartValueIndex[heartValueIndex.Length - 1] == 1)
                    {
                        fullHealth = true;
                    }
                    return;
                }

                if (heartValueIndex[i] == 1)
                {
                    heartsArray[i].GetComponent<Image>().sprite = heartImageArray[0];
                }
            }
        }
    }

    public void GainShield(int shieldToGain)
    {
        if (ifBar)
        {
            currentShield += shieldToGain;
            if (currentShield > maxShield)
            {
                currentShield = maxShield;
            }
        }
        else if (ifHearts)
        {
            if (overShieldActive!)
            {
                overShieldActive = true;
            }
            for (int i = 0; i < 1; i++)
            {
                if (shieldValueIndex[0] <= 0)
                {
                    overShieldActive = true;
                    shieldValueIndex[0]++;
                    StartCoroutine(ShieldGainEffect(shieldArray[0].gameObject));
                    shieldArray[0].GetComponent<Image>().sprite = shieldImageArray[0];
                    return;
                }

                if (shieldValueIndex[1] <= 0)
                {
                    if (shieldValueIndex[0] > 0)
                    {
                        shieldValueIndex[1]++;
                        StartCoroutine(ShieldGainEffect(shieldArray[1].gameObject));
                        shieldArray[1].GetComponent<Image>().sprite = shieldImageArray[0];
                    }
                    return;
                }
            }
        }
    }

    IEnumerator HealThrob(GameObject currentHeart)
    {
        currentHeart.GetComponent<Image>().color = Color.green;
        currentHeart.GetComponent<Animator>().SetBool("HeartTakeDamageThrob", true);

        yield return new WaitForSecondsRealtime(0.4f);

        currentHeart.GetComponent<Animator>().SetBool("HeartTakeDamageThrob", false);
        currentHeart.GetComponent<Image>().color = Color.white;
    }

    IEnumerator ShieldGainEffect(GameObject currentShield)
    {
        currentShield.GetComponent<Image>().color = Color.blue;
        currentShield.GetComponent<Animator>().SetBool("ShieldGainEffect", true);

        yield return new WaitForSecondsRealtime(0.4f);

        currentShield.GetComponent<Animator>().SetBool("ShieldGainEffect", false);
        currentShield.GetComponent<Image>().color = Color.white;
    }
    #endregion

    public void PlayerDeath()
    {
        // Play the death animation, die, respawn at the last checkpoint with full hp and no shield
    }

    public void PlayerSpawn()
    {
        //Reset all hearts and values that should be renewed.
    }
}
