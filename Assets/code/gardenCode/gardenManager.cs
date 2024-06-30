using UnityEngine.UI;
using UnityEngine;

public class gardenManager : MonoBehaviour
{
    [SerializeField] Transform[] plantSpots;
    private Plant blank;
    private List<Plant> allPlants = new List<Plant>();
    public List<Plant> unlockedPlants = new List<Plant>(); {get; private set;}



    private void Start()
    {
        //TODO: read these plants from file, for now creating them in start

        
    }
}
