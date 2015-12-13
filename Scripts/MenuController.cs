using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class MenuController : MonoBehaviour 
{
	public Button startButton;
	public Button continueButton;
	public Button exitButton;
	public Button menuButton;
	public GameObject modalPanel;

	void OnEnable()
	{
		transform.SetAsLastSibling ();
	}

	private static ModalPanel modalPanel;

	public static ModalPanel Instance ()
	{
		if (!modalPanel) {
			modalPanel = FindObjectOfType (typeof(ModalPanel)) as ModalPanel;
			if (!modalPanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}
		return modalPanel;
	}
		
	public void click (UnityAction start, UnityAction cont, UnityAction exit)
	{
		modalPanel.setActive (true);

		startButton.onClick.RemoveAllListeners ();
		startButton.onClick.AddListener (start);
		startButton.onClick.AddListener (closePanel);

		continueButton.onClick.RemoveAllListeners ();
		continueButton.onClick.AddListener (cont);
		continueButton.onClick.AddListener (closePanel);

		exitButton.onClick.RemoveAllListeners ();
		exitButton.onClick.AddListener (exit);
		exitButton.onClick.AddListener (closePanel);

		startButton.gameObject.SetActive (true);
		continueButton.gameObject.SetActive (true);
		exitButton.gameObject.SetActive (true);
	}

	void closePanel()
	{
		modalPanel.setActive (false);
	}
}

