using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class WorldCheckpoints : MonoBehaviour
{
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerIncorrectCheckpoint;

    private List<CheckpointSingle> checkpointSingleList = new List<CheckpointSingle>();
    private int nextCheckpointSingleIndex;

    private void Awake()
    {

        Transform checkpointsTransform = transform.Find("Checkpoints");

        //checkpointSingleList = new List<CheckpointSingle>();
        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetCheckpoints(this);
            checkpointSingleList.Add(checkpointSingle);
        }

        nextCheckpointSingleIndex = 0;

    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            //Rigtige checkpoint
            Debug.Log("Forkert checkpoint");
            CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
            correctCheckpointSingle.Hide();

            nextCheckpointSingleIndex = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            //Forkert checkpoint, måske reset til start eller noget?
            Debug.Log("Forkert checkpoint");
            OnPlayerIncorrectCheckpoint?.Invoke(this, EventArgs.Empty);

            CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
            correctCheckpointSingle.Show();
        }
    }
}
