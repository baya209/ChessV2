
# Échec En 3D

Jeu d'échec 3D pour débutant, 
est un jeu visant l'apprentissage des règles de bases d'échec.
L'utilisateur a la possibilité d'intéragir avec une interface 3D dans laquelle évolue le programme.
Grâce à une IA (NegaMax) intégrée, chaque joueur aura la meilleur proposition de jeu évalué dépendamment de la profondeur d'évaluation de l'IA (allant de 1 à 3).
Le jeu se lance dans un réseau local (LAN) instancié par le joueur hôte. 
 


## Auteurs

- Alia Cayer
- Baya-Kenzza Fortas
- Richardson Bazelais
- Loïc Beaudin-Kerzérho



## Technologies
- Unity version 2022.3.38.f1
- System.Net.Sockets;
- System.Threading;
- GitHub Desktop
- VS code 
- Ryder

## Instruction
Le menu principal contient trois boutons
- Paramètre
- Jouer 
- Informations

Les Paramètres permettent de changer le volume à l'aide de la barre de son et permettent de changer la profondeur de calcul de l'algorithme à l'aide du menu déroulant

Le bouton "Information" donne accès au crédits et aux règles du jeu

Le bouton "Jouer" du menu Principal amène sur la page de connexion, permettant de choisir d'héberger
ou de rejoindre une partie.
Une fois l'une des options choisies, la partie se lance sur le plateau d'échec.

Lors du déroulement de la partie, il faut cliquer avec la souris sur la pièce à bouger et cliquer sur la case finale. 
La case survolée est en jaune et la case proposée est en vert fluo.


## Installation

Installer le projet en le clonant de GIT. Il faut ensuite ouvrir le Fichier .exe

 
## Structure
Les scripts, scènes, images, musique de fond, etc. sont dans les sous dossiers de Asset du même nom.

## Prérequis
- Windows 10
- MacOS 10
- GPU avec DX10 11 ou 12 sur windows ou Metal sur Mac

## Roadmap

- Changement de décors

- Sauvegarde de la partie localement et sur le nuage

- Simplification de la logique

## Fonctionalité de l'IA
Permet de prédire le meilleur coup au maximum de trois coups en avance 
en se basant sur deux aspects :
- L'importance d'occuper le milieu
- La valorisation de chaque pièce


