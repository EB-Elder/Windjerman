using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class IntrerfaceManagerScript : MonoBehaviour
{
    [SerializeField] private Canvas canvasInterface;
    [SerializeField] private Text titre;
    [SerializeField] private Text score;
    [SerializeField] private Button play;
    [SerializeField] private Button choix1;
    [SerializeField] private Button choix2;
    [SerializeField] private Button retourMenu;
    [SerializeField] private Text vs;
    [SerializeField] private Text finDePartie;
    [SerializeField] private Button bouttonquitter;

    // Start is called before the first frame update
    void Start()
    {
        finDePartie.gameObject.SetActive(false);
        retourMenu.gameObject.SetActive(false);
        score.gameObject.SetActive(false);


        //action des boutons
        bouttonquitter.onClick.AddListener(Quitter);
        retourMenu.onClick.AddListener(RetourMenu);
        play.onClick.AddListener(MaskMenu);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //fonction pour quitter le jeu
    private void Quitter()
    {
        Application.Quit();

    }

    //retour au menu
    private void RetourMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    //mise à jour du score
    public void UpdateScore(int scoreJ1 , int scoreJ2)
    {
        score.text = (scoreJ1 + " - " + scoreJ2);

    }

    //
    public void MaskMenu()
    {
        choix1.interactable = false;
        choix1.gameObject.SetActive(false);

        choix2.interactable = false;
        choix2.gameObject.SetActive(false);

        bouttonquitter.interactable = false;
        bouttonquitter.gameObject.SetActive(false);

        vs.gameObject.SetActive(false);

        play.interactable = false;
        play.gameObject.SetActive(false);

        score.gameObject.SetActive(true);
        titre.gameObject.SetActive(false);


    }
}
