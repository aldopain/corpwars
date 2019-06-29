using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This manager has functions to return both raw and formatted information about selected town
 * Set selected town (probably via event in selection manager) before getting info
 */
public class piqUI_TownInfoManager : MonoBehaviour
{
    [SerializeField]    //Used only for debugging, should be removed when selection manager is done
    private GameObject selectedTown;
    private Actor_Resources townResources;

    public class PopClassInfo {
        public int population;
        public int productivity;
        public int happiness;
        public double[] basicNeeds;
        public double[] luxuryNeeds;
    }

    public class TownInventoryInfo {
        public double[] Amount;
        public double[] Produced;
        public double[] Bought;
        public double[] Sold;

        //DEBUG. TODO: Get rid of magic number
        public TownInventoryInfo() {
            Amount = new double[9];
            Produced = new double[9];
            Bought = new double[9];
            Sold = new double[9];
        }
    }

    public class FactoryInfo {
        public Type _type;
        public Resource_Input[] input;
        public Resource_Input[] output;
    }

    void Start() {
        SetSelectedTown(selectedTown);  //Debug, used to initialize necessary fields when setting town from editor
    }

    public void SetSelectedTown(GameObject town) {
        selectedTown = town;
        townResources = town.GetComponent<Actor_Resources>();
    }

    public PopClassInfo GetPopClassInfo(piqUi_TownWindowManager.PopClass c) {
        Town_Population.PopClass selectedClass;
        switch (c) {
            case piqUi_TownWindowManager.PopClass.Poor:
                selectedClass = selectedTown.GetComponent<Town_Population>().Poor;
                break;
            case piqUi_TownWindowManager.PopClass.Middle:
                selectedClass = selectedTown.GetComponent<Town_Population>().Middle;
                break;
            case piqUi_TownWindowManager.PopClass.Rich:
                selectedClass = selectedTown.GetComponent<Town_Population>().Rich;
                break;
            default:
                throw new System.Exception("Invalid PopClass");
        }

        PopClassInfo info = new PopClassInfo {
            population = selectedClass.Amount,
            happiness = selectedClass.Happiness,
            productivity = selectedClass.Productivity,
            basicNeeds = selectedClass.BirthrateDemands,
            luxuryNeeds = selectedClass.HappinessDemands
        };

        return info;
    }

    public TownInventoryInfo GetInventoryInfo() {
        TownInventoryInfo info = new TownInventoryInfo();

        for (int i = 0; i < townResources.Debug_GetResourceLength(); i++) {
            info.Amount[i] = townResources.GetResource(i);
        }

        return info;
    }

    public int GetMaxHappiness() {
        return Town_Population.PopClass.maxHappiness;
    }

    public int GetMaxProductivity() {
        return Town_Population.PopClass.maxProductivity;
    }

    public List<FactoryInfo> GetFactoriesInfo() {
        List<FactoryInfo> info = new List<FactoryInfo>();
        List<Resource_Factory> factories = selectedTown.GetComponent<Town_Production>().factories;
        foreach (var f in factories) {
            FactoryInfo i = new FactoryInfo();
            i._type = f.type;
            i.input = f.recipe.input;
            i.output = f.recipe.output;
            info.Add(i);
        }

        return info;
    }
}
