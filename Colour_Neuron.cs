using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Colour_Neuron : MonoBehaviour
{
    public int size;
    public int neuronDNASize = 6;
    public string targetColourHex;
    private System.Random rnd = new System.Random();
    public ColourDNA myDNA;

    // Start is called before the first frame update
    void Awake()
    {
        InitializeNeuron();
        // print neuron DNA data and spawn objects
        // print DNA data
        string shownDNA = string.Join("", myDNA.DNA);
        // Debug.Log("NEW COLOUR");
        // Debug.Log(shownDNA);

        // // spawn GameObject
        // GameObject temp_obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // // Add a Renderer component (e.g., MeshRenderer or SpriteRenderer) to the GameObject
        // // This line assumes you are working with 3D objects; adjust for 2D if needed
        Renderer renderer = gameObject.GetComponentInChildren<Renderer>();

        // Convert the hexadecimal color code to a Color object
        Color newColor = ColorUtility.TryParseHtmlString("#" + shownDNA, out Color parsedColor) ? parsedColor : Color.white;
        // Set the material color to the new color
        renderer.material.color = newColor;

        // // Randomise position
        // Transform transform = temp_obj.GetComponent<Transform>();
        // Vector3 randomPosition = new Vector3(rnd.Next(-50, 50), 1, rnd.Next(-50, 50));
        // transform.position = randomPosition;

        // // Add rigidBody
        // temp_obj.AddComponent<Rigidbody>();

        // Instantiate(temp_obj);
    }

    private void InitializeNeuron() {
        myDNA = new ColourDNA(rnd, neuronDNASize, null, null);
    }

    public void MergeDNA(ColourDNA DNAFather, ColourDNA DNAMother) {
        myDNA.GenerateDNA(DNAFather.DNA, DNAMother.DNA);
    }

    public void EnsureCorrectColour() {
        string shownDNA = string.Join("", myDNA.DNA);
        Debug.Log("PARENT INHERITED COLOUR");
        Debug.Log(shownDNA);
        Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
        Color DNADrivenColor = ColorUtility.TryParseHtmlString("#" + shownDNA, out Color parsedColor) ? parsedColor : Color.white;
        // Set the material color to the proper color
        renderer.material.color = DNADrivenColor;
    }
}

public class ColourDNA {
    public string[] DNA;
    private System.Random myRnd;
    private int myDNASize;

    public ColourDNA(System.Random rnd, int DNASize, string[] DNAFather, string[] DNAMother) {
        myRnd = rnd;
        myDNASize = DNASize;
        if (DNAFather != null && DNAMother != null) {
            DNA = GenerateDNA(DNAFather, DNAMother);
        } else {
            DNA = RandomiseDNA();
        }
    }

    public void SetDNA(string[] newDNA) {
        DNA = newDNA;
    }

    public string[] GenerateDNA(string[] DNAFather, string[] DNAMother) {
        string[] tempDNA = new string[DNAFather.Length + DNAMother.Length];
        string[] parentDNA = new string[DNAFather.Length + DNAMother.Length];

        Array.Copy(DNAFather, parentDNA, DNAFather.Length);
        Array.Copy(DNAMother, 0, parentDNA, DNAFather.Length, DNAMother.Length);

        int parentSize = parentDNA.Length;
        for (int i = 0; i < parentSize; i++) {
            int randomNumber = myRnd.Next(parentSize);
            tempDNA[i] = parentDNA[randomNumber];
        }

        return tempDNA;
    }

    private string[] RandomiseDNA() {
        string[] tempDNA = new string[myDNASize];
        for (int i = 0; i < myDNASize; i++) {
            string randomDNA = myRnd.Next(16).ToString("X");;
            tempDNA[i] = randomDNA;
        }
        return tempDNA;
    }

    // section up DNA into hexadecimal chunks
}