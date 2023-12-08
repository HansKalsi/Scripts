using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roman_Logic : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject skin; // Skin
    public string unitName; // title
    public string[] nickname; // commendations
    public int startingAmount; // starting number of soliders for generation
    private List<Soldier> soldiers; // Collection of soldiers

    private int fightingNumbers; // fighting capacity
    private float averageMorale; // willingness to fight
    private float averageHealth; // health of units

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the collection of soldiers
        InitializeSoldiers();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Function to add a string to the nickname array
    public void AddNickname(string newNickname)
    {
        List<string> updatedNicknames = new List<string>(nickname);
        updatedNicknames.Add(newNickname);
        nickname = updatedNicknames.ToArray();
    }

    // Function to initialize the collection of soldiers
    private void InitializeSoldiers()
    {
        soldiers = new List<Soldier>();

        var xSpawn = spawnPoint.position.x;
        var zSpawn = spawnPoint.position.z;

        // Create soldiers based on the number array
        for (int i = 0; i < startingAmount; i++)
        {
            var tempPosition = spawnPoint.position;
            if (i%10 == 0) {
                // if there are less than 10 left to generate, ensure they are positioned centrally at the back of the unit
                var remainderOfSA = startingAmount%10;
                if (i + remainderOfSA >= startingAmount - remainderOfSA) {
                    var finalStartingXPos = (10 - remainderOfSA) / 2; // 10 is the max length for each line, divided by 2 to compensate for left & right
                    xSpawn = spawnPoint.position.x + finalStartingXPos;
                } else {
                    xSpawn = spawnPoint.position.x;
                }
                zSpawn += 1;
            }
            tempPosition.x = xSpawn;
            xSpawn += 1;
            tempPosition.z = zSpawn;
            Soldier newSoldier = new Soldier();
            GameObject newSoliderObject = Instantiate(skin, tempPosition, spawnPoint.rotation);
            newSoldier.GO = newSoliderObject;
            newSoldier.Morale = Random.Range(1, 100); // You can modify this as needed
            newSoldier.Health = Random.Range(1, 100); // You can modify this as needed
            newSoldier.Strength = Random.Range(1, 100); // You can modify this as needed
            newSoldier.Armour = Random.Range(1, 100); // You can modify this as needed
            newSoldier.Equipment = new string[] { "Sword", "Shield" }; // You can modify this as needed

            soldiers.Add(newSoldier);
        }

        // // Calculate averages since the troops exist now
        // CalculateAverages();
    }

    // // Function to calculate averages
    // private void CalculateAverages()
    // {
    //     // if (soldiers.Count > 0)
    //     // {
    //     //     // Calculate average morale
    //     //     averageMorale = soldiers.Average(soldier => soldier.Morale);

    //     //     // Calculate average health
    //     //     averageHealth = soldiers.Average(soldier => soldier.Health);
    //     // }
    // }
}

// Soldier class to represent individual soldiers
public class Soldier
{
    public GameObject GO { get; set; }
    public int Morale { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Armour { get; set; }
    public string[] Equipment { get; set; }
}
