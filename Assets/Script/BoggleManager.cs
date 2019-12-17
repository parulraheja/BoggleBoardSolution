using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoggleManager : MonoBehaviour
{
    public InputField noOfRows;
    public InputField noOfCols;
    public Transform parentGrid;
    public InputField prefabGrid;

    public InputField noOfValidElments;
    public Transform parentDict;
    public InputField prefabDictInput;
    public List<InputField> dictText;


    public List <InputField> gridText;

    public char[,] board;
    public Dictionary<string, string> possibleWords = new Dictionary<string, string>();

    public void OnSubmitBtn()
    {
         for (int i = 0; i < int.Parse(noOfRows.text); i++)
        {
            for (int j = 0; j < int.Parse(noOfCols.text); j++)

            {
              gridText.Add(Instantiate(prefabGrid, parentGrid));

            }
        }
    }


    public void AddTextValuesToBoard()
    {
        int count = 0;
        board = new char[int.Parse(noOfRows.text), int.Parse(noOfCols.text)];

        for (int i = 0; i < int.Parse(noOfRows.text); i++)
        {
            for (int j = 0; j < int.Parse(noOfCols.text); j++)

            {    

                board[i, j] = char.Parse(gridText[count].text);
                count++;
            }
        }
    }

    public void IntantiateInputField()
    {
       for(int i=0;i< int.Parse(noOfValidElments.text);i++)
        {
            dictText.Add(Instantiate(prefabDictInput, parentDict));

        }
    }

    public void addValuesToValidDictionary()
    {
        int index = 0;
        for (int i = 0; i < int.Parse(noOfValidElments.text); i++)
        {
            possibleWords.Add(dictText[index].text, dictText[index].text);

                index++;
        }
    }

    public void TraverseElementsInBoggleBoard()
    {

    }


}
