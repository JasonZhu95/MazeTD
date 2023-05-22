using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMoney : MonoBehaviour
{
    private string correctCode = "greedisgood";
    [SerializeField] private PlayerStats playerStats;

    private string inputCode = "";

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            // Get the pressed key
            string key = Input.inputString;

            // Ignore non-alphanumeric characters
            if (!string.IsNullOrEmpty(key) && char.IsLetterOrDigit(key[0]))
            {
                // Add the pressed key to the input code
                inputCode += key;

                // Check if the input code matches the correct code
                if (inputCode == correctCode)
                {
                    GiveMoney();
                }
                else if (!correctCode.StartsWith(inputCode))
                {
                    // Reset the input code if an incorrect key is entered
                    inputCode = "";
                }
            }
        }
    }

    private void GiveMoney()
    {
        playerStats.AddCoins(1000000);
        inputCode = "";
    }
}
