using UnityEngine;
using CualitChallenge.Characters.Player;
using UnityEngine.Events;

namespace CualitChallenge.Game
{

    [RequireComponent(typeof(EnemyWavesSpawner))]
    public class GameController : MonoBehaviour
    {
        [SerializeField] GameObject startCamera;
        [SerializeField] GameObject gameCamera;
        [SerializeField] GameObject player;
        [SerializeField] UnityEvent onCombatStart;
        [SerializeField] UnityEvent onCombatEnd;

        private bool isPlaying = false;
        private bool isCursorLocked = false;

        private PlayerMovement playerMovement;
        private PlayerCombat playerCombat;
        private EnemyWavesSpawner wavesSpawner;

        private int currentWave = 0;
        private bool waitingForNextWave = false;

        private void Awake()
        {
            wavesSpawner = GetComponent<EnemyWavesSpawner>();
            wavesSpawner.OnWaveEnd.AddListener(WaveEnded);
        }

        void Start()
        {
            SetCursorLocked(true);
            playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement == null) Debug.LogError("Missing PlayerMovement behaviour attached to player");
            playerCombat = player.GetComponent<PlayerCombat>();
            if (playerCombat == null) Debug.LogError("Missing PlayerCombat behaviour attached to player");

            wavesSpawner.SetPlayerTransform(player.transform);

            SetPlayerInputsEnabled(false);
            startCamera.SetActive(true);
            gameCamera.SetActive(false);
        }


        void Update()
        {
            if (!isPlaying)
            {
                if (Input.GetKeyDown(KeyCode.Escape)) SetCursorLocked(!isCursorLocked);
                if (Input.GetKeyDown(KeyCode.Space)) StartGame();
                return;
            }

            if (waitingForNextWave)
            {
                if (Input.GetKeyDown(KeyCode.Space)) StartWave();
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
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !locked;
            this.isCursorLocked = locked;
        }

        public void SetPlayerInputsEnabled(bool inputsEnabled) => playerMovement.enabled = playerCombat.enabled = inputsEnabled;


        public void StartWave()
        {
            waitingForNextWave = false;
            playerCombat.SetInCombatArea(true);
            wavesSpawner.CleanUp();
            wavesSpawner.StartWave(currentWave);
            onCombatStart.Invoke();
        }

        public void WaveEnded()
        {
            currentWave++;
            waitingForNextWave = true;
            playerCombat.SetInCombatArea(false);
            onCombatEnd.Invoke();
        }

    }

}