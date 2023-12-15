using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Colour_Neurons : MonoBehaviour
{
    public int size;
    public int neuronDNASize;
    public string targetColourHex;
    private System.Random rnd = new System.Random();
    public List<ColourDNA> neurons = new List<ColourDNA>();

    // Start is called before the first frame update
    void Start()
    {
        InitializeNeurons();
        // print neuron DNA data and spawn objects
        foreach (ColourDNA neuron in neurons) {
            // print DNA data
            string shownDNA = string.Join("", neuron.DNA);
            Debug.Log(shownDNA);
            // spawn GameObject
            GameObject temp_obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // Add a Renderer component (e.g., MeshRenderer or SpriteRenderer) to the GameObject
            // This line assumes you are working with 3D objects; adjust for 2D if needed
            Renderer renderer = temp_obj.GetComponent<Renderer>();
            // Convert the hexadecimal color code to a Color object
            Color newColor = ColorUtility.TryParseHtmlString("#" + shownDNA, out Color parsedColor) ? parsedColor : Color.white;
            // Set the material color to the new color
            renderer.material.color = newColor;

            // Randomise position
            Transform transform = temp_obj.GetComponent<Transform>();
            Vector3 randomPosition = new Vector3(rnd.Next(-50, 50), 1, rnd.Next(-50, 50));
            transform.position = randomPosition;

            // Add rigidBody
            temp_obj.AddComponent<Rigidbody>();

            Instantiate(temp_obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeNeurons() {
        while (neurons.Count < size) {
            ColourDNA temp_neuron = new ColourDNA(rnd, neuronDNASize, null, null);
            neurons.Add(temp_neuron);
        }
    }
}

public class ColourDNA {
    public List<string> DNA { get; }
    private System.Random myRnd;
    private int myDNASize;

    public ColourDNA(System.Random rnd, int DNASize, List<string> DNAFather, List<string> DNAMother) {
        myRnd = rnd;
        myDNASize = DNASize;
        if (DNAFather != null && DNAMother != null) {
            DNA = GenerateDNA(DNAFather, DNAMother);
        } else {
            DNA = RandomiseDNA();
        }
    }

    private List<string> GenerateDNA(List<string> DNAFather, List<string> DNAMother) {
        List<string> tempDNA = new List<string>();
        List<string> parentDNA = DNAFather.Concat(DNAMother).ToList();
        int parentSize = parentDNA.Count;
        do {
            int randomNumber = myRnd.Next(parentSize);
            tempDNA.Add(parentDNA[randomNumber]);
        } while(tempDNA.Count < myDNASize);
        return tempDNA;
    }

    private List<string> RandomiseDNA() {
        List<string> tempDNA = new List<string>();
        do {
            string randomDNA = myRnd.Next(16).ToString("X");;
            tempDNA.Add(randomDNA);
        } while(tempDNA.Count < myDNASize);
        return tempDNA;
    }

    // section up DNA into hexadecimal chunks
}