using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System;
using Rules = WindjermanGameStateRules;

public struct NodeAStar
{
    
    public WindjermanGameState gsNode;
    public float distanceFromFrisbee;
    public bool hasFrisbee;
    public bool highGround;
    public bool evenHight;
    public int playerID;
    public int score;

    public void FindHighGround()
    {
        float ecartJoueurs = Unity.Mathematics.math.abs(gsNode.playerPosition1.y - gsNode.playerPosition2.y);

        if(playerID == 0)
        {
            if(ecartJoueurs > 4)
            {
                if (gsNode.playerPosition1.y > gsNode.playerPosition2.y) highGround = true;
                else highGround = false;

                evenHight = false;
            }
            else
            {
                evenHight = true;
            }
        }
        else
        {
            if(ecartJoueurs > 4)
            {
                if (gsNode.playerPosition2.y > gsNode.playerPosition1.y) highGround = true;
                else highGround = false;

                evenHight = false;
            }
            else
            {
                evenHight = true;
            }
        }
    }

    public void CalculerDistanceFromFrisbee()
    {
        if(playerID == 0)
        {
            distanceFromFrisbee = Vector2.Distance(gsNode.playerPosition1, gsNode.frisbeePosition);
        }
        else
        {
            distanceFromFrisbee = Vector2.Distance(gsNode.playerPosition2, gsNode.frisbeePosition);
        }
    }

    public float getDistanceFromFrisbee()
    {
        return distanceFromFrisbee;
    }
}

public class AAgentScript : IAgent
{
    private int playerID;
    private float currentDistanceFromFrisbee;
    private bool highGround;
    private bool evenHight;

    public void FindHighGround(ref WindjermanGameState gs)
    {
        float ecartJoueurs = Unity.Mathematics.math.abs(gs.playerPosition1.y - gs.playerPosition2.y);

        if (playerID == 0)
        {
            if (ecartJoueurs > 4)
            {
                if (gs.playerPosition1.y > gs.playerPosition2.y) highGround = true;
                else highGround = false;

                evenHight = false;
            }
            else
            {
                evenHight = true;
            }
        }
        else
        {
            if (ecartJoueurs > 4)
            {
                if (gs.playerPosition2.y > gs.playerPosition1.y) highGround = true;
                else highGround = false;

                evenHight = false;
            }
            else
            {
                evenHight = true;
            }
        }
    }

    public AAgentScript(int playerID)
    {
        this.playerID = playerID;
    }

    public float CalculerCurrentDistanceFromFrisbee(ref WindjermanGameState gs)
    {
        if (playerID == 0) return Vector2.Distance(gs.playerPosition1, gs.frisbeePosition);
        else return Vector2.Distance(gs.playerPosition2, gs.frisbeePosition);
    }

    public int Act(ref WindjermanGameState gs, NativeList<int> availableActions)
    {
        //déterminer la distance entre le joueur et le frisbee au moment T
        currentDistanceFromFrisbee = CalculerCurrentDistanceFromFrisbee(ref gs);
        FindHighGround(ref gs);

        //création de la liste des nodes
        var listeNodes = new NativeArray<NodeAStar>();

        //pour chaque action disponible
        for (int i = 0; i < availableActions.Length; i++)
        {
            //on crée un node
            NodeAStar n = new NodeAStar();
            n.distanceFromFrisbee = 0;
            n.playerID = this.playerID;
            n.gsNode = Rules.Clone(ref gs);

            //on execute ladite action pour avoir le gamestate T+1 associé
            if(playerID == 0)
            {
                Rules.Step(ref n.gsNode, availableActions[i], 0);
            }
            else
            {
                Rules.Step(ref n.gsNode, 0, availableActions[i]);
            }

            //déterminer si le joueur a le frisbee à T+1
            if (playerID == 0)
            {
                if (n.gsNode.isFreeze1) n.hasFrisbee = true;
                else n.hasFrisbee = false;
            }
            else
            {
                if (n.gsNode.isFreeze2) n.hasFrisbee = true;
                else n.hasFrisbee = false;
            }

            //déterminer la distance entre le joueur et le frisbee si celui-ci ne l'a pas en main à T+1
            if (!n.hasFrisbee) n.CalculerDistanceFromFrisbee();

            //si le joueur a le frisbee, déterminer sa situation par rapport à l'autre joueur
            else n.FindHighGround();

            //ajouter le node à la liste
            listeNodes[i] = n;
        }

        //une fois les nodes créés, on détermine l'action à réaliser
        return FindBestAction(listeNodes, ref gs);
    }

    public int FindBestAction(NativeArray<NodeAStar> listeNodes, ref WindjermanGameState gs)
    {
        float newDistancefromFrisbee = currentDistanceFromFrisbee;
        int indexClosestToFrisbee = 0;

        //si le joueur n'a pas le frisbee a l'instant T les bonnes action sont celles qui rapprochent le joueur de celui-ci
        if(playerID == 0)
        {
            if(!gs.isFreeze1)
            {
                for(int i = 0; i < listeNodes.Length; i++)
                {
                    //si l'action permet d'attraper le frisbee on la sélectionne
                    if(listeNodes[i].hasFrisbee)
                    {
                        listeNodes.Dispose();
                        return i;
                    }
                    else
                    {
                        //si l'action réduit la distance entre le joueur et le frisbee, on conserve le résultat de cette action
                        if(listeNodes[i].getDistanceFromFrisbee() < currentDistanceFromFrisbee)
                        {
                            indexClosestToFrisbee = i;
                            currentDistanceFromFrisbee = listeNodes[i].getDistanceFromFrisbee();
                        }
                    }
                }

                listeNodes.Dispose();
                return indexClosestToFrisbee;
            }
            else
            {/*
                //si le joueur a le frisbee, les bonnes actions sont celles qui lancent le frisbee loin de l'autre joueur
                for(int i = 0; i < listeNodes.Length; i++)
                {
                    //si le joueur a le highground, tirer tout droit est la meilleure option
                    if(listeNodes[i].gsNode.)
                    {
                        //tirer tout droit est la meilleure option mais on ajoute une variation pour rendre le bot moins prédictible
                    }
                }
                */
            }
        }
        else
        {
            if(!gs.isFreeze2)
            {
                for (int i = 0; i < listeNodes.Length; i++)
                {
                    //si l'action permet d'attraper le frisbee on la sélectionne
                    if (listeNodes[i].hasFrisbee)
                    {
                        listeNodes.Dispose();
                        return i;
                    }
                    else
                    {
                        //si l'action réduit la distance entre le joueur et le frisbee, on conserve le résultat de cette action
                        if (listeNodes[i].getDistanceFromFrisbee() < currentDistanceFromFrisbee)
                        {
                            indexClosestToFrisbee = i;
                            currentDistanceFromFrisbee = listeNodes[i].getDistanceFromFrisbee();
                        }
                    }
                }

                listeNodes.Dispose();
                return indexClosestToFrisbee;
            }
            else
            {

            }
        }
        

        return 0;
    }
}
