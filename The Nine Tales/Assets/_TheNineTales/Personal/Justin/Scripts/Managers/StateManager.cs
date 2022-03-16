using System;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public enum GameState { Platforming, Narrative, Dialogue, StillImage, Paused }

public class StateManager : MonoBehaviour
{
    public Flowchart startingFlowchart;
    public bool skipOpeningFlowchart;

    public Player characterController;
    public GameState startingState = GameState.Dialogue;
    public Text levelText;
    public Text lastSceneText;
    public Text currentStateText;

    public static Player cc;
    public static Interactor pi;

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
        if (pi == null) pi = cc.GetComponentInChildren<Interactor>();
    }

    private void Start()
    {
        if (cam == null) cam = Camera.main.GetComponent<CameraController>();
        if (characterController == null) characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        SetState(startingState);

        if(startingFlowchart != null)
        {
            if(!skipOpeningFlowchart) startingFlowchart.ExecuteBlock("Starting");
            else startingFlowchart.ExecuteBlock("GiveControl");
        } else
        {
            if (!skipOpeningFlowchart) Debug.LogWarning("No starting flowchart was assigned, but you didn't choose to skip the starting flowchart in " + gameObject.name + "!");
        }
    }
    private void Update()
    {
        levelText.text = LevelTracker.GetCurrentLevel();
        lastSceneText.text = LevelTracker.lastScene;
        currentStateText.text = currentGameState.ToString();
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
                cc.ForceIdleAnimation(false);
                cc.EnableControls();
                cam.SetCameraZoom(true);
                cam.SetCameraFollow(true);
                pi.enabled = true;
                break;
            case GameState.Platforming:
                cc.ForceIdleAnimation(false);
                cc.EnableControls();
                cam.SetCameraZoom(false);
                cam.SetCameraFollow(true);
                pi.enabled = true;
                break;
            case GameState.Dialogue:
                cc.ForceIdleAnimation();
                cc.DisableControls();
                cam.SetCameraZoom(true);
                cam.SetCameraFollow(false);
                pi.enabled = false;
                cc.m_CurrentHealth = cc.m_FullHealth;
                GlobalVolumeController.Singleton.CameraFadeDark(cc.m_CurrentHealth, cc.m_FullHealth);
                break;
            case GameState.StillImage:
                cc.ForceIdleAnimation();
                cc.DisableControls();
                cam.SetCameraZoom(true);
                cam.SetCameraFollow(false);
                pi.enabled = false;
                cc.m_CurrentHealth = cc.m_FullHealth;
                GlobalVolumeController.Singleton.CameraFadeDark(cc.m_CurrentHealth, cc.m_FullHealth);
                break;
            case GameState.Paused:
                cc.ForceIdleAnimation();
                cc.DisableControls();
                cam.SetCameraFollow(true);
                pi.enabled = false;
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
