
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private CharacterButton[] characterButtons;
    [SerializeField] private Transform _characterSpawnPoint;

    private Queue<CharacterButton> characterQueue = new Queue<CharacterButton>(); 
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
    }

    public void CharacterButtonPressed(CharacterButton button)
    {
        if (button.CanDeploy())
        {
            PlayerCurrency.Instance.SubtractMoney(button._cost);
            // Add the character to the queue
            characterQueue.Enqueue(button);

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
        PlayerCurrency.Instance.DisplyMoneyInConsole();

    }
}








//using Assets.Scripts;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class GameManager : MonoBehaviour
//{
//    public static GameManager Instance;
//    [SerializeField] private CharacterButton[] characterButtons;
//    [Tooltip("The Spawn point for the Characters to diploy")]
//    [SerializeField] private Transform _characterSpawnPoint;

//    private void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//        {
//            Debug.LogError("Multiple GameManager instances detected!");
//            Destroy(gameObject);
//        }
//    }
//    public void CharacterButtonPressed(CharacterButton button)
//    {
//        int selectedIndex = System.Array.IndexOf(characterButtons, button);
//        if (selectedIndex != -1)
//        {
//            if (button.CanDeploy())
//            {
//                Deploy(button._characterPrefab, button._deployDelayTime);    
//                PlayerCurrency.Instance.SubtractMoney(button._cost);
//            }
//            //Debug.Log($"Selected Character index is: {selectedIndex}, character cost : {button._cost}, can diploy : {button.CanDeploy()}");
//        }
//        else
//        {
//            Debug.LogError("Button not found in the array!");
//        }
//    }


//    private void Deploy(GameObject characterPrefab, float diployDelayTime)
//    {
//        StartCoroutine(DeployDelayTime(characterPrefab, diployDelayTime));
//    }

//    IEnumerator DeployDelayTime(GameObject characterPrefab, float diployDelayTime)
//    {
//        yield return new WaitForSeconds(diployDelayTime);
//        Instantiate(characterPrefab, _characterSpawnPoint.position, _characterSpawnPoint.rotation);
//    }


//    private void Update()
//    {
//        Test();
//    }

//    public int _moneyToAdd;
//    public int _moneyToSubstract;
//    private void Test()
//    {
//        if (Input.GetKeyDown(KeyCode.C))
//        {
//            PlayerCurrency.Instance.AddMoney(_moneyToAdd);
//        }
//        if (Input.GetKeyDown(KeyCode.F))
//        {
//            PlayerCurrency.Instance.SubtractMoney(_moneyToSubstract);
//        }
//        PlayerCurrency.Instance.DisplyMoneyInConsole();

//    }

//}



