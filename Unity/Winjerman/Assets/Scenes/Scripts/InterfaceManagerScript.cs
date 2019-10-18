//Auteurs : Régis, Shen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum listeChoix
{
    HUMAN = 0,
    RANDOM = 1,
    RANDOMROLLOUT = 2,
    DIJKSTRA = 3,
    MCTS = 4
}


public class InterfaceManagerScript : MonoBehaviour
{
    [Header ("Composants d'interface")]
    [SerializeField] private Canvas canvasInterface;
    [SerializeField] private Text titre;
    [SerializeField] private Text score;
    [SerializeField] private Button play;
    [SerializeField] private Button choix1;
    [SerializeField] private Text textButtonChoix1;
    [SerializeField] private Button choix2;
    [SerializeField] private Text textButtonChoix2;
    [SerializeField] private Button retourMenu;
    [SerializeField] private Text vs;
    [SerializeField] private Text finDePartie;
    [SerializeField] private Button bouttonquitter;
    [SerializeField] private Text pause;
    [SerializeField] private Text timer;

    [Header("Références")]
    [SerializeField] private GameSystemScript gameSystem;

    private listeChoix choix1Value = listeChoix.HUMAN;
    private listeChoix choix2Value = listeChoix.RANDOM;

    private void Awake()
    {
        //action des boutons
        bouttonquitter.onClick.AddListener(Quitter);
        retourMenu.onClick.AddListener(RetourMenu);
        play.onClick.AddListener(MaskMenu);
        choix1.onClick.AddListener(ChangeAgent1);
        choix2.onClick.AddListener(ChangeAgent2);
    }

    public void UpdateTimer(int nbSecRestante)
    {
        timer.text = nbSecRestante.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        //masquer les éléments inutiles au menu principal
        finDePartie.gameObject.SetActive(false);
        retourMenu.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);

        //activation des boutons
        play.gameObject.SetActive(true);
        play.interactable = true;

        bouttonquitter.gameObject.SetActive(true);
        bouttonquitter.interactable = true;
    }

    //fonction pour quitter le jeu
    public void Quitter()
    {
        Application.Quit();
    }

    //retour au menu
    public void RetourMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //mise à jour du score
    public void UpdateScore(int scoreJ1 , int scoreJ2)
    {
        score.text = (scoreJ1 + " - " + scoreJ2);
    }

    //fonction de mise en pause
    public void Pause(bool etat)
    {
        if(etat)
        {
            pause.gameObject.SetActive(true);
        }
        else
        {
            pause.gameObject.SetActive(false);
        }
    }

    public void FinDePartie()
    {
        finDePartie.gameObject.SetActive(true);
        retourMenu.gameObject.SetActive(true);
        retourMenu.interactable = true;
    }

    //fonction pour masquer le menu
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
        timer.gameObject.SetActive(true);

        titre.gameObject.SetActive(false);

        //lancer la partie
        gameSystem.StartGame(choix1Value, choix2Value);
    }

    //fonction pour changer l'agent du joueur 1
    public void ChangeAgent1()
    {
        switch(choix1Value)
        {
            case listeChoix.HUMAN:

                textButtonChoix1.text = "RANDOM";
                choix1Value = listeChoix.RANDOM;
                break;

            case listeChoix.RANDOM:

                textButtonChoix1.text = "RANDOM ROLLOUT";
                choix1Value = listeChoix.RANDOMROLLOUT;
                break;

            case listeChoix.RANDOMROLLOUT:

                textButtonChoix1.text = "DIJKSTRA";
                choix1Value = listeChoix.DIJKSTRA;
                break;

            case listeChoix.DIJKSTRA:

                textButtonChoix1.text = "MCTS";
                choix1Value = listeChoix.MCTS;
                break;

            case listeChoix.MCTS:

                textButtonChoix1.text = "HUMAN";
                choix1Value = listeChoix.HUMAN;
                break;

            default:

                textButtonChoix1.text = "HUMAN";
                choix1Value = listeChoix.HUMAN;
                break;
        }
    }

    //fonction pour changer l'agent du joueur 1
    public void ChangeAgent2()
    {
        switch (choix2Value)
        {

            case listeChoix.HUMAN:

                textButtonChoix2.text = "RANDOM";
                choix2Value = listeChoix.RANDOM;
                break;

            case listeChoix.RANDOM:

                textButtonChoix2.text = "RANDOM ROLLOUT";
                choix2Value = listeChoix.RANDOMROLLOUT;
                break;

            case listeChoix.RANDOMROLLOUT:

                textButtonChoix2.text = "DIJKSTRA";
                choix2Value = listeChoix.DIJKSTRA;
                break;

            case listeChoix.DIJKSTRA:

                textButtonChoix2.text = "MCTS";
                choix2Value = listeChoix.MCTS;
                break;

            case listeChoix.MCTS:

                textButtonChoix2.text = "HUMAN";
                choix2Value = listeChoix.HUMAN;
                break;

            default:

                textButtonChoix2.text = "HUMAN";
                choix1Value = listeChoix.HUMAN;
                break;
        }
    }
}
