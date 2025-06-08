
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static event Action OnQueueChanged;
    public static GameManager Instance;

    [SerializeField] int _startingMoney;
    [SerializeField] private Transform _characterSpawnPoint;
    
    public Queue<CharacterButton> characterQueue = new Queue<CharacterButton>(); 
    private bool isDeploying = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("Multiple GameManager instances detected!");
            Destroy(gameObject);
        }
        PlayerCurrency.Instance.AddMoney(_startingMoney);
    }

    public void CharacterButtonPressed(CharacterButton button)
    {
        if (button.CanDeploy())
        {
            PlayerCurrency.Instance.SubtractMoney(button._cost);
            // Add the character to the queue
            characterQueue.Enqueue(button);
            OnQueueChanged?.Invoke();

            // If no deployment is currently happening, start the queue process
            if (!isDeploying)
            {
              DeployNextCharacter();
            }
        }
    }

    private void DeployNextCharacter()
    {
        if (characterQueue.Count > 0)
        {
            //remove button from queue
            CharacterButton nextCharacter = characterQueue.Dequeue();
            OnQueueChanged?.Invoke();
            //deploy logic
            isDeploying = true;
            StartCoroutine(DeployDelayTime(nextCharacter._characterPrefab, nextCharacter._deployDelayTime));
        }
        else
        {
            isDeploying = false;
        }
    }
    
    private IEnumerator DeployDelayTime(GameObject characterPrefab, float deployDelayTime)
    {
        yield return new WaitForSeconds(deployDelayTime);

        //TODO: implent animation for deployment 
        //to show index that character is deploing  

        Instantiate(characterPrefab, _characterSpawnPoint.position, _characterSpawnPoint.rotation);

        // Deployment finished, check for the next character in the queue
        isDeploying = false;
        DeployNextCharacter();
    }
    private void Update()
    {
        Test();
    }

    public int _moneyToAdd;
    public int _moneyToSubstract;
    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerCurrency.Instance.AddMoney(_moneyToAdd);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerCurrency.Instance.SubtractMoney(_moneyToSubstract);
        }
        //PlayerCurrency.Instance.DisplyMoneyInConsole();

    }
}



