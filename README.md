# SENSEI SIMU

Projet fait en rendu de TP individuel Unity 3D. Les éléments demandés sont : des objets custom, du scripting, un environnement, de l'audio, un contrôleur, de l'animation, et une IA.
[Trailer du jeu fait pour le TP](https://youtu.be/GZ6z9dZaLls)

## Installation

Télécharger le fichier QuocBao_NGUYEN_TPunity.zip    → Vous y trouverez les 3 documents ci-dessous
- un readme contenant les instructions du jeu (celui que vous lisez actuellement)
- un dossier build du projet avec un exécutable
- un fichier .bat pour un lien Microsoft Edge vers le trailer mp4 du jeu (le lien ci dessus [Trailer du jeu fait pour le TP](https://youtu.be/GZ6z9dZaLls)) - inspiré/parodié du trailer WOW : WOTLK

## Histoire

Vous êtes un professeur japonais (sensei) et vous vous occupez d'une classe. Vous allez veiller au bon déroulement d'un examen de la distribution des feuilles à la notation des élèves.

## Contrôles

### Souris

- Déplacement de la souris pour viser/ déplacer le curseur sur l'écran
- Clique gauche pour tirer / appuyer sur les boutons

### Clavier

- Flèches directionnelles ←↑→↓/ ZQSD pour se déplacer
- Touche TABULATION pour le score actuel du joueur
- Touche Echap pour mettre en pause, reprendre, recommencer, quitter la partie ou régler la sensibilité

## Déroulement du jeu

### Menu / Tutoriel

Après lancement du jeu, le tutoriel vidéo s'affiche avec les boutons suivants :
- Précédent / Suivant → la précédente/prochaine phase du tutoriel
- Skip Tuto → passe le tutoriel et commence la partie
- Exit → Quitte le jeu et ferme l'application

### Partie

- Phase 1 : Distributions des sujets. Chaque élève doit posséder un sujet sur sa table, envoyer les sujets par clique gauche.
- Phase 2 : Surveiller les élèves et les empêcher de tricher. Toucher un tricheur avec une craie le fait sortir de la salle, toucher un innocent avec une craie vous fait perdre de la vie. Le joueur possède 2 points de vie initialement
- Phase 3 : Notation escalier. Selon les réponses de l'élèves sur ce sujet de philosophie, les noter en envoyant sa feuille sur la marche voulue.

### Fin de jeu

- Tableau récapitulatif de la partie
## Moteur de jeu

  - [Unity3D](https://unity.com/) - C#

## Auteur

Quoc-Bao Nguyen

[Github](https://github.com/Baokebab)

## Résumé de l'implémentation

- Contrôleur : New input system de Unity, les mouvements du joueur par add velocity, clamping de la caméra pour la rotation verticale
- Lancer d'objet : Instantiation simple et directe des préfabs avec un addForce et AddTorque dépendant de la position et rotation de la caméra
- IA : Un mix de stateMachine, NavMesh, script normal
- Animation : Animator de Unity, deux layers avec un pour le corps entier et un pour le UpperBody
- Boids : Aquarium
- Le nombre de tricheur est fixe par choix, leur sélection est aléatoire
- Un grand nombre d'améliorations possibles et surtout de l'optimisation et de nettoyage de code mais ce projet reste un TP de 1 semaine en plus de l'assimilation du cours. Je ne reviendrai probablement pas sur ce projet passé le rendu du 15/11/2022

## Remerciements

  - CodeMonkey pour son code avec les graphes en UI
  - [Adobe Mixamo](https://www.mixamo.com/) pour leurs animations
  - Nathan GIROD - [Github](https://github.com/Blowerlop/) et son package Synty Studio pour mon environnement
  - Le site [Youfulca](https://youfulca.com/2022/08/07/priest/) pour les voix japonaises gratuites
  - Kevin MacLeod, Naruto, Komiku pour leur musique de fonds
