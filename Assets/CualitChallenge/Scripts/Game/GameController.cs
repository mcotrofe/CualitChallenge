using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using CualitChallenge.Player;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject startCamera;
    [SerializeField] GameObject gameCamera;
    [SerializeField] GameObject player;

    private bool isPlaying = false;
    private bool isCursorLocked = false;

    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;

    void Start()
    {
        SetCursorLocked(true);
        playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement == null) Debug.LogError("No PlayerMovement behaviour attached to player");
        playerCombat = player.GetComponent<PlayerCombat>();
        if (playerCombat == null) Debug.LogError("No PlayerCombat behaviour attached to player");

        SetPlayerInputsEnabled(false);
        startCamera.SetActive(true);
        gameCamera.SetActive(false);
    }


    void Update()
    {
        if (!isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) SetCursorLocked(!isCursorLocked);
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) StartGame();
            return;
        }

        
    }

    public void StartGame()
    {
        if (isPlaying) return;
        isPlaying = true;
        SetCursorLocked(true);
        SetPlayerInputsEnabled(true);
        startCamera.SetActive(false);
        gameCamera.SetActive(true);
    }


    public void SetCursorLocked(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.Confined;
        Cursor.visible = !locked;
        this.isCursorLocked = locked;
    }

    public void SetPlayerInputsEnabled(bool inputsEnabled) => playerMovement.enabled = playerCombat.enabled = inputsEnabled;
}
