using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoggleManager : MonoBehaviour
{   [Header("Board elements")]
    public InputField noOfRows;
    public InputField noOfCols;
    public Transform parentGrid;
    public InputField prefabGrid;
    public InputField noOfValidElments;
    //Stores each character of the board
    public List<InputField> boardText;

    [Header("Dictinary elements")]
    public Transform parentDict;
    public InputField prefabDictInput;
    public List<InputField> dictText;    
    public Dictionary<string, string> possibleWords = new Dictionary<string, string>();

    [Header("Output Text")]
    public Text FoundWordsText;

    char[,] board;
    int rows;
    int columns;
    
    /// <summary>
	///Generate board with
	///input fields to add values in the board
	/// </summary>
    public void CreateBoard()
    {
        boardText.Clear();
        ClearOldElement(parentGrid);
        rows = int.Parse(noOfRows.text);
        columns = int.Parse(noOfCols.text);
        if (rows == columns)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    boardText.Add(Instantiate(prefabGrid, parentGrid));
                }
            }
        }
    }

    /// <summary>
    /// add grid text values to 2d Board
    /// when user provide all the input for the board
    /// </summary>
    public void AddTextValuesToBoard()
    {
        int count = 0;
        board = new char[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {    
                board[i, j] = char.Parse(boardText[count].text);
                count++;
            }
        }
    }
    /// <summary>
    /// create input field for the dictinary words 
    /// </summary>
    public void CreateDictinary()
    {
        dictText.Clear();
        ClearOldElement(parentDict);

        for (int i=0;i< int.Parse(noOfValidElments.text);i++)
       {
            dictText.Add(Instantiate(prefabDictInput, parentDict));
       }
    }

    /// <summary>
    ///  add the user input word to dictinary on submit btn
    /// </summary>
    public void addWordsToDictionary()
    {
        int totalWordsCount = int.Parse(noOfValidElments.text);
        for (int i = 0; i < totalWordsCount; i++)
        {
            possibleWords.Add(dictText[i].text, dictText[i].text);
        }
    }

    /// <summary>
    /// Traverse each element of the board 
    /// after submitting values of grid and dictinary 
    /// </summary>
    public void TraverseElementsInBoggleBoard()
    {
        bool[,] visitedGrid = new bool[rows, columns];
        string str = "";
        for (int i = 0; i < rows; i++)
        {

            for (int j = 0; j < columns; j++)
            {
                // Debug.Log(allElements[i, j]);
                FindAjacentOfChar(board[i, j], i, j, str, visitedGrid);
            }
        }
    }


    /// <summary>
    /// search each adjacent elements for the board element
    /// </summary>
    /// <param name="ch"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="str1"></param>
    /// <param name="isVisited"></param> mark as visted if it is already traversed
    
    void FindAjacentOfChar(char ch, int i, int j, string str1, bool[,] isVisited)
    {
        str1 = str1 + ch;
        isVisited[i, j] = true;
        // if string length greater three then it will check in the dictinary 
        if (str1.Length > 3)
        {
           if( IsValidString(str1))
            {                
                possibleWords.Remove(str1);
                FoundWordsText.text += (str1 + ',');

            }
        }

        //To Traverse each non-visites element of board
       for (int row = i - 1; row <= i + 1 && row < rows; row++)
       {
            for (int col = j - 1; col <= j + 1 && col < columns; col++)
            {
             
                if (row >= 0 && col >= 0 && !isVisited[row, col])
                {
                    FindAjacentOfChar(board[row, col], row, col, str1, isVisited);

                }
            }
       }

       isVisited[i, j] = false;

     }

    /// <summary>
    /// To validate current string in the dictionary
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    bool IsValidString(string str)
    {
        return possibleWords.ContainsKey(str);
    }

    /// <summary>
    /// Destroys older elements to create new
    /// </summary>
    /// <param name="parent"></param>
    void ClearOldElement( Transform parent)
    {
        for(int i=0;i<parent.childCount;i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
}

