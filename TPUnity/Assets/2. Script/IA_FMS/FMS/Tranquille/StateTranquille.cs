using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Linq;

public class StateTranquille : FSMState<StateInfo>
{
    public float PeriodSitting = UnityEngine.Random.Range(2.0f,4.5f);  //Assis à rien faire entre 2 et 4.5s
    public float PeriodWriting = UnityEngine.Random.Range(8.0f,15f); //Ecris entre 8 et 15s
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
        "Sucer c'est pas tromper\n",
        "6 x 3 ? 43\n",
        "Mais une femelle ne peut pas battre un mâle\n",
        "Erwan Temple\n",
        "Ma bite n'aura servi à rien\n",
        "Que le dieu du coding room bénisse votre repas\n",
        "Imagine tu prends ta mère\n",
        "Ah c'était pas des tampons?\n",
        "on dit kebabier ou kebabiste ?\n",
        "on dit gif ou jif ?\n",
        "j'aime bien les lolis\n",
        "scouteur et couteur\n",
        "Hélias, tu es un chacal\n",
        "Désolé je connais pas votre méta\n",
        "Son corps a officiellement 8 ans\n",
        "Il est plus bas mon trou de balle\n",
        "J'ai le droit de juger, j'ai mon permis\n",
        "Je suis très fort pour tenir les queues mo\n",
        "C'est pas les reins qui repoussent?\n",
        "Je vais pas a Disney land sans mon handicapé\n",
        "Ce n'est pas la vitesse qui compte, c'est le nombre de coups\n",
        "Je suis trop gros je peux pas rentrer dans le trou\n",
        "Note a moi même : ne pas laisser Lisa près de ma bouche\n",
        "On est juste un giga gros dé humain\n",
        "Je ne sais pas s'il est naturellement long et epais\n",
        "Ha oui mais c'est du harcèlement constructif\n",
        "ça y est tu retourne dans ton pays?\n",
        "Tant qu'il est pas 9h on est en avance\n",
        "Tant que Pauline me déclare pas en retard je suis en avance\n",
        "Réel\n",
        "Je ne peux pas mettre Guillaume dans Océane, il y a trop de conflits\n",
        "Toi écarte moi\n",
        "Tu conduis comme la chatte de Guillaume\n",
        "Je suis nul en braquage\n",
        "C'est comme ta mère et son concombre\n",
        "Mais une femelle ne peut pas battre un mâle ?!\n",
        "C'est Unity, c'est de la merde.\n",
        "Arrête de me bully stp\n",
        "Tu as les pouces qui pointent\n",
        "Les seuls que j'ai leché c'est les couilles de mammouth\n",
        "Tout le monde la déjà touché\n",
        "T'as quoi comme cul raph\n",
        "Moi à Nantes, je m'amusais à compter les clochards\n",
        "Tu crois Yuna elle dirait quoi pour pain au fromage?\n",
        "Les poneys je les mange\n",
        "Oh il est mort, boh c'était un peu triste\n",
        "Je suis pas venue pour souffrir ok?\n",
        "Hélias j'le bouffe\n",
        "J'adore les non-anniversaires\n",
        "Oula ils ont pas une ordonnance d'éloignement normalement eux?\n",
        "trop bi-zarre les gens de cette école quand même\n",
        "Coche tout au pif, Coche rien au pif\n",
        "Have You Tried Turning It Off And On?\n",
        "la taille de la capote si c'est ça, sheesh\n",
        "Pour ou contre éteindre la lumière dans la salle?\n",
        "Hélias x Raphaël\n",
        "céréales après le lait\n",
        "ça y est tu vas pouvoir aller te doucher\n",
        "J'aime bien les douches quand même\n",
        "micro usb en 2022?\n",
        "Merci de m'avoir lu\n",
        "j'ai l'impression d'être un migrant mexicain\n",
        "qu'est-ce qu'il fait tout-le-monde? il joue\n",
        "MGQC9-9R7V9-FHF23-KV6KV-QPM4Z\n",
        "je vais me foutre en l'air là\n",
        "Aled j'ai oublié mon badge\n",
        "et l'attaque au couteaux en bas de chez moi aussi\n",
        "99% vodka - 1% redbull\n",
        "et là en fait ils ont buté qqun\n",
        "je fais mon coming out\n",
        "Il s'est pas lavé depuis combien d'années ?\n",
        "oui singe\n",
        "le rage quitte est total\n",
        "Je vais faire un génocide\n",
        "oui j'aime le chômage\n",
        "la moine nagui t'as protégé du feur\n",
        "le prof regarde sous ma jupe, j'ai peur..\n",

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
            if(infos.cheatingBonus)
            {
                FillText(_spaceToWrite);
                infos.cheatingBonus = false;
            }
                
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
        if((space >= 0 && _textToWrite[_spaceToWrite].Length < 34) || space >= 1) //Si on a encore de la place pour écrire
        {
            var canvas = _IaController.MyPaper.transform.GetChild(0).gameObject;
            canvas.SetActive(true);
            canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += _textToWrite[_spaceToWrite];
            if (_textToWrite[_spaceToWrite].Length > 33) _spaceToWrite--;
            _spaceToWrite--;
        }
     }
}