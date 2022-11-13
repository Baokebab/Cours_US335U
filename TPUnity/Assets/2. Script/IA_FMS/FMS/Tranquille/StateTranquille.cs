using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Linq;

public class StateTranquille : FSMState<StateInfo>
{
    public float PeriodSitting = UnityEngine.Random.Range(2.0f,5f);  //Assis à rien faire entre 3 et 5s
    public float PeriodWriting = UnityEngine.Random.Range(5.0f,10f); //Ecris entre 5 et 10s
    private float TempoTranquille = 0;
    private bool Init = true;

    private int _spaceToWrite = 16;
    private string[] _textToWrite = new string[]
    {
        "Je n'ai aucune idée de ce que j'écris\n",
        "(a+b)²=a²+b². ça m'a l'air pas mal\n",
         "Je m'appelle Teuse\n",
        "C'est l'histoire de deux pédophiles..\n",
        "Eikichi Onizuka, 22 ans célibataire\n",
        "Je pense donc je suis\n",
        "I am Batman.\n",
        "Never gonna give you up, never gonna let you down\n",
        "Ils sont là!Dans nos campagnes..\n",
        "Personne ne va lire ça toute façon\n",
        "Ooh Eeh Ooh AhAh TingTang WallaWalla Bing\n",
        "Mel.. Assieds-toi toi faut j'te parle\n",
        "Envie de baiser? Envoie SMS au 06\n",
        "Je peux pas aller au Yemen\n",
        "C'est quoi le plan? LA GUERRE.\n",
        "Please delete Yuumi.\n",
        "TP9 > Michael Jordan\n",
        "Damn les gens! Auj c'est abdos\n",
        "Si tu cherches l'échec, demandes à Kaaris.\n",
        "Whoa on voit à travers le pare-brise!\n",
        "Oh non Guillaume tu m'as déjà détruit le cul..\n",
        "Le problème c'est que si tu bute quelqu'un du fn..\n",
        "Whoa il avale bien!\n",
        "Hélias j'le bouffe\n",
        "Hélias j'le bouffe\n",
        "Hélias j'le bouffe\n",
        "6 x 3 ? 43\n",
        "Mais une femelle ne peut pas battre un mâle\n",
        "Erwan Temple\n",
        "Ma bite n'aura servi à rien\n",
        "Que le dieu du coding room bénisse votre repas\n",
        "Imagine tu prends ta mère\n",
        "Ah c'était pas des tampons?\n",
        "on dit kebabier ou kebabiste ?\n",
        "on dit gif ou jif ?\n",
    };
    public override void doState(ref StateInfo infos)
    {
        TempoTranquille += infos.PeriodUpdate;

        if ((TempoTranquille > PeriodSitting && isActiveSubstate<StateIdleSitting>()) || Init) 
        {
            Init = false;
            TempoTranquille = 0;
            PeriodSitting = UnityEngine.Random.Range(2.0f, 5f);
            addAndActivateSubState<StateWriting>();
            infos.writingRemaining--;
            FillText(_spaceToWrite);
            _spaceToWrite--;
        }
        else if (TempoTranquille > PeriodWriting && isActiveSubstate<StateWriting>())
        {
            TempoTranquille = 0;
            PeriodWriting = UnityEngine.Random.Range(5.0f, 10f);
            addAndActivateSubState<StateIdleSitting>();
        }
        KeepMeAlive = true; //Sinon on perds la tempo, l'init etc...
    }

    void FillText(int space)
    {
        if(space == 16)
        {
            _textToWrite = _textToWrite.OrderBy(x => Guid.NewGuid()).ToArray();
        }
        if(space >= 0) //Si on a encore de la place pour écrire
        {
            var canvas = _IaController.MyPaper.transform.GetChild(0).gameObject;
            canvas.SetActive(true);
            canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += _textToWrite[_spaceToWrite];
            if (_textToWrite[_spaceToWrite].Length > 37) _spaceToWrite--;
        }
     }
}