using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ScriptableObjects;
using Core.Player;
using UnityEngine.UI;

public class CombineElements : MonoBehaviour
{
    public static CombineElements instance;
    [SerializeField]  private DamageType element1, element2;
    public Button combinedElement;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        combinedElement.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddElement(DamageType damageType)
    {
        //if null set damage types 
        if (element1 == null)
        {
            element1 = damageType;
            combinedElement.gameObject.SetActive(false);

        }
        else if (element2 == null)
        {
            element2 = damageType;
            CombineTwoElements();
        }

        //Debug.Log(element1);
        //Debug.Log(element2);
    }

    private void CombineTwoElements()
    {
        foreach (AbilityData ability in PlayersManager.instance.warrior.abilities)
        {
            if (ability.DamageType[0] == element1 && ability.DamageType[1] == element2)
            {
                NewCombinedElement(ability);
                break;
            }
            else if (ability.DamageType[0] == element2 && ability.DamageType[1] == element1)
            {
                NewCombinedElement(ability);
                break;
            }
        }
        //reset elements
        element1 = null;
        element2 = null;
    }

    private void NewCombinedElement(AbilityData abilityData)
    {
        combinedElement.image.sprite = abilityData.Icon;
        combinedElement.gameObject.GetComponent<NoteButtonManager>().noteToSpawn = abilityData.NotePrefab;
        combinedElement.gameObject.SetActive(true);

        //Debug.Log(abilityData);
    }
}
