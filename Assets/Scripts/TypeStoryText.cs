﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;

public class TypeStoryText : MonoBehaviour {

    public string filename;
    public Text text;
    public double typeSpeed = .25;
    private double counter;
    private string fileText= "The Watchers. A group of beings who for untold ages have guarded the sanctity of time. Observing from outside of time, they monitor the proper flow of time and remedy any anomalies that they detect. \n\nThe least of the Watchers is called Apprentice. While the others primarily monitor the flow of time, Apprentice is the one sent to handle most of the anomalies that they find. \n\nBut one day, as he was observing the Earth, he found an anomaly that the others had not yet told him about.He reported this to the chief Watcher, Aion.But Aion replied that it was nothing to worry about yet, and forbade Apprentice from going to Earth to fix it.\n\nApprentice couldn't just let it go, however, and when the anomaly suddenly multiplied, he knew that he had to do something about it. He set out for Earth, defying his master's orders to do what he knew was right.Thus, his adventure began.";
	
    public void awake()
    {
        text = GetComponent<Text>();
        counter = 0;
        fileText = content();
    }

    public string content()
    {
        string output = "";
        try
        {
            using (StreamReader stream = new StreamReader(filename))
            {
                output = stream.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Could not read file.");
            Console.WriteLine(e.Message);
        }
        return output;
    }
	
	// Update is called once per frame
	void Update () {
        if (counter < fileText.Length)
            text.text = fileText.Substring(0, (int)counter);
        else text.text = fileText;
        counter+=typeSpeed;
        Console.WriteLine("updating");
	}
}