
using TMPro;
using UnityEngine;


public class Message : MonoBehaviour {

    [SerializeField]
    GameObject _title;
    [SerializeField]
    GameObject _message;
    private TextMeshProUGUI title;
    private TextMeshProUGUI message;
    private void Start() {
        title = _title.GetComponent<TextMeshProUGUI>();
        message = _message.GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    public void ShowMessage(string title, string message) {
        gameObject.SetActive(true);
        FindObjectOfType<GameManager>().Pause();
        this.title.text = title;
        this.message.text = message;
        GameEvents.current.OnActionPressed += EndMessage;
    }

    private void EndMessage() {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().Unpause();
        GameEvents.current.OnActionPressed -= EndMessage;
    }
}