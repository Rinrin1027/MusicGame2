using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;
}

[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

public class NotesManager : MonoBehaviour
{
    public List<float>[] LaneNotesTimes { get; private set; }
    public List<int>[] LaneNoteTypes { get; private set; }

    private Data songData;

    public List<GameObject> NotesObj { get; private set; } = new List<GameObject>();

    [SerializeField] private GameObject noteObj;

    private void OnEnable()
    {
        LaneNotesTimes = new List<float>[4];
        LaneNoteTypes = new List<int>[4];
        for (int i = 0; i < 4; i++)
        {
            LaneNotesTimes[i] = new List<float>();
            LaneNoteTypes[i] = new List<int>();
        }

        Load("Blossom");
    }

    private void Load(string songName)
    {
        string inputString = Resources.Load<TextAsset>(songName).ToString();
        songData = JsonUtility.FromJson<Data>(inputString);

        GManager.instance.maxScore = songData.notes.Length * 5;

        ClearNotes(); // Reset the notes lists and remove existing notes objects

        for (int i = 0; i < songData.notes.Length; i++)
        {
            var note = songData.notes[i];
            float kankaku = 60f / (songData.BPM * note.LPB);
            float beatSec = kankaku * note.LPB;
            float time = (beatSec * note.num / (float)note.LPB) + songData.offset * 0.01f;
            float z = time * GManager.instance.noteSpeed;

            NotesObj.Add(Instantiate(noteObj, new Vector3(note.block - 1.5f, 0.55f, z), Quaternion.identity));

            // Add to LaneNotesTimes and LaneNoteTypes
            LaneNotesTimes[note.block].Add(time);
            LaneNoteTypes[note.block].Add(note.type);
        }
    }

    private void ClearNotes()
    {
        // Remove existing notes objects
        foreach (GameObject note in NotesObj)
        {
            Destroy(note);
        }

        // Clear the list
        NotesObj.Clear();

        // Clear LaneNotesTimes and LaneNoteTypes
        foreach (var laneNotesTime in LaneNotesTimes)
        {
            laneNotesTime.Clear();
        }

        foreach (var laneNoteType in LaneNoteTypes)
        {
            laneNoteType.Clear();
        }
    }

    public void ResetNotesData()
    {
        ClearNotes();
        // Perform any additional reset operations here if needed
    }
}
