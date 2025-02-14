using TMPro;
using UnityEngine;

public class ColorCounter : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private string currentTag = "";

    [SerializeField] private int redCount = 0;
    [SerializeField] private int blueCount = 0;
    [SerializeField] private int yellowCount = 0;
    [SerializeField] private int greenCount = 0;

    [SerializeField] private Transform cameraTransform; // 카메라 Transform

    [SerializeField] private TextMeshProUGUI pickupTextUI;
    [SerializeField] private TextMeshProUGUI interactTextUI;
    [SerializeField] private TextMeshProUGUI resetTextUI;
    [SerializeField] private TextMeshProUGUI countDisplayUI;

    [SerializeField] private ColorChanger colorChanger; // ColorChanger 오브젝트를 직접 참조

    void Start()
    {
        pickupTextUI.text = "";
        pickupTextUI.gameObject.SetActive(false);

        interactTextUI.text = "";
        interactTextUI.gameObject.SetActive(false);

        resetTextUI.text = "";
        resetTextUI.gameObject.SetActive(false);

        countDisplayUI.text = GetCountDisplayText();
        countDisplayUI.gameObject.SetActive(false); // 초기에는 비활성화
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            IncrementColorCount();
        }

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.R))
        {
            ResetCounts();
        }
    }

    void IncrementColorCount()
    {
        if (colorChanger == null) return;

        Color currentColor = colorChanger.GetCurrentColor();

        if (currentTag == "red" && currentColor == Color.red)
            redCount++;
        else if (currentTag == "blue" && currentColor == Color.blue)
            blueCount++;
        else if (currentTag == "yellow" && currentColor == Color.yellow)
            yellowCount++;
        else if (currentTag == "green" && currentColor == Color.green)
            greenCount++;

        countDisplayUI.text = GetCountDisplayText();
        Debug.Log(currentColor + " 색상의 카운트가 증가했습니다.");
    }

    void ResetCounts()
    {
        redCount = 0;
        blueCount = 0;
        yellowCount = 0;
        greenCount = 0;

        countDisplayUI.text = GetCountDisplayText();
        Debug.Log("카운트가 초기화되었습니다.");
    }

    private string GetCountDisplayText()
    {
        return $"Red: {redCount}, Blue: {blueCount}, Yellow: {yellowCount}, Green: {greenCount}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("red") || other.CompareTag("blue") || other.CompareTag("yellow") || other.CompareTag("green"))
        {
            isPlayerInRange = true;
            currentTag = other.tag;

            interactTextUI.text = "Press E to interact with the object";
            interactTextUI.gameObject.SetActive(true);

            resetTextUI.text = "Press R to reset";
            resetTextUI.gameObject.SetActive(true);

            countDisplayUI.text = GetCountDisplayText();
            countDisplayUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(currentTag))
        {
            isPlayerInRange = false;
            currentTag = "";

            interactTextUI.gameObject.SetActive(false);
            resetTextUI.gameObject.SetActive(false);
            countDisplayUI.gameObject.SetActive(false);
        }
    }
}
