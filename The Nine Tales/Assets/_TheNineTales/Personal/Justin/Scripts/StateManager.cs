using System;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public enum GameState { Platforming, Narrative, Dialogue, StillImage, Paused }

public class StateManager : MonoBehaviour
{
    public Player characterController;
    public GameState startingState = GameState.Dialogue;
    public Flowchart startingFlowchart;
    public Text stateText;

    private static Player cc;

    private static GameState currentGameState;
    private static GameState lastGameState;

    public static GameState CurrentGameState
    {
        get => currentGameState;
    }

    public static bool debugMode = true;

    private static CameraController cam;

    private void Awake()
    {
        if (cc == null) cc = characterController;
    }

    private void Start()
    {
        if (cam == null) cam = Camera.main.GetComponent<CameraController>();

        SetState(startingState);
    }
    private void Update()
    {
        stateText.text = CurrentGameState.ToString();
    }

    public void ExitDialogueToPlatforming()
    {
        SetState(GameState.Platforming);
    }
    public void ExitDialogueToNarrative()
    {
        SetState(GameState.Narrative);
    }

    public static void SetState(string state)
    {
        SetState(Enum.Parse<GameState>(state, true));
    }

    public static void SetState(GameState state)
    {
        lastGameState = currentGameState;
        currentGameState = state;

        switch (state)
        {
            case GameState.Narrative:
                cam.SetCameraZoom(true);
                cc.enabled = true;
                break;
            case GameState.Platforming:
                cam.SetCameraZoom(false);
                cc.enabled = true;
                break;
            case GameState.Dialogue:
                cam.SetCameraZoom(true);
                cc.enabled = false;
                break;
            case GameState.StillImage:
                cam.SetCameraZoom(true);
                cc.enabled = false;
                break;
            case GameState.Paused:
                cc.enabled = false;
                break;
        }
    }
    public void ReturnToLastGameState() {
      SetState(lastGameState);
    }
    public static void UndoGameState()
    {
        SetState(lastGameState);
    }
}
