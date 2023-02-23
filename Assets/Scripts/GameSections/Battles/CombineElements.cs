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
    private Color originalColour;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        originalColour = GetComponent<Image>().color; 
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
        foreach (DamageType damageType in PlayersManager.instance.warrior.combinationsUnlocked)
        {
            if (damageType.Combination[0] == element1 && damageType.Combination[1] == element2)
            {
                NewCombinedElement(damageType);
                break;
            }
            else if (damageType.Combination[0] == element2 && damageType.Combination[1] == element1)
            {
                NewCombinedElement(damageType);
                break;
            }
            else
            {
                StartCoroutine(FlashColour(Color.red));
            }

            //if(ReferenceEquals(damageType.Combination[1], element1) && ReferenceEquals(damageType.Combination[2], element2))
            //{
            //    NewCombinedElement(damageType);
            //    break;
            //}
            //else if (ReferenceEquals(damageType.Combination[1], element2) && ReferenceEquals(damageType.Combination[2], element1))
            //{
            //    NewCombinedElement(damageType);
            //    break;
            //}
        }
        
        //reset elements
        element1 = null;
        element2 = null;

    }

    private void NewCombinedElement(DamageType damageType)
    {
        StartCoroutine(FlashColour(Color.green));
        combinedElement.image.sprite = damageType.Icon;
        combinedElement.gameObject.GetComponent<NoteButtonManager>().noteToSpawn = damageType.NotePrefab;
        combinedElement.gameObject.SetActive(true);
    }

    IEnumerator FlashColour(Color colourToChange)
    {
        GetComponent<Image>().color = colourToChange;
        yield return new WaitForSeconds(0.25f);
        GetComponent<Image>().color = originalColour;
    }
}
