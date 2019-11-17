/************************************************************
************************************************************/
using UnityEngine;
using System.IO;

public struct DT_Wave
{
    public DT_Wave(int id=-1, int nWaves = -1, int nEnemies = -1){
        _id = id;
        _numWaves = nWaves;
        _numEnemies = nEnemies;
    }
    public int                                  _id;            // substitute for a string for the name.
    public int                                  _numEnemies;
    public int                                  _numWaves;
}

public static class IO_Wave
{
    public static void FSaveWave(DT_Wave w)
    {
        if(w._id == -1 || w._numEnemies == -1 || w._numWaves == -1)
        {
            Debug.Log("This wave was not set up properly");
            return;
        }

        Debug.Log("Saving...");
        StreamWriter sw = new StreamWriter(Application.dataPath+"/FILE_IO/Waves/" + w._id.ToString()+".txt");
        sw.WriteLine("ID");
        sw.WriteLine(w._id);
        sw.WriteLine("Num Waves");
        sw.WriteLine(w._numWaves);
        sw.WriteLine("Num Enemies");
        sw.WriteLine(w._numEnemies);

        sw.Close();
    }

    public static DT_Wave FLoadWave(string fileName)
    {
        DT_Wave w = new DT_Wave();

        string path = Application.dataPath+"/FILE_IO/Waves/"+fileName+".txt";
        string[] sLines = System.IO.File.ReadAllLines(path);

        for(int i=0; i<sLines.Length; i++){
            if(sLines[i].Contains("ID")){
                w._id = int.Parse(sLines[i+1]);
            }
            if(sLines[i].Contains("Waves")){
                w._numWaves = int.Parse(sLines[i+1]);
            }
            if(sLines[i].Contains("Enemies")){
                w._numEnemies = int.Parse(sLines[i+1]);
            }
        }

        return w;
    }

}
