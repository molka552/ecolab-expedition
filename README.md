# EcoLab Expedition

**Molka Boughanmi · Unity / C# · Aventure educative 3D**

Guide **Nori**, un petit robot de jardin, dans trois jardins oublies. Ramasse les graines dorees, trouve les carnets, contourne les ronces puis resous les enigmes des bornes pour restaurer chaque zone.

## Ouvrir cette nouvelle version

1. Extraire l'archive dans un **nouveau dossier EcoLabExpedition**. Ne pas ecraser ton ancien EcoLab.
2. Dans Unity Hub > Add project from disk, selectionner le dossier contenant directement **Assets / Packages / ProjectSettings**.
3. Utiliser **Unity 2022.3.62f2**, ta version installee.
4. Ouvrir **Assets/Scenes/Main.unity**, cliquer Play, puis **PARTIR EN EXPEDITION** dans Game.
5. Agrandir l'onglet Game par double-clic si l'interface parait petite.

Le decor se construit au lancement. Une scene qui semble vide avant Play est normale. Aucun asset externe requis.

## Boucle de jeu

1. Explorer la carte avec Nori.
2. Ramasser les **3 graines dorees** en marchant dessus : +100 points chacune.
3. Passer sur le **carnet blanc** pour lire une observation utile.
4. Eviter les **ronces rouges** : un contact retire un coeur et ne deplace pas le robot.
5. Rejoindre la **borne ronde lumineuse**, puis appuyer sur **E** ou OUVRIR LA BORNE.
6. Resoudre l'enigme, voir la floraison, lire le score et les etoiles, puis passer au jardin suivant.

Il n'y a pas de limite de temps : le chronometre ajoute seulement un bonus au score.

## Commandes

| Commande | Action |
|---|---|
| Fleches / WASD / ZQSD | Avancer d'une case, attendre la fin de la petite animation |
| E | Interagir avec la borne quand tu es juste a cote |
| Echap | Pause / reprendre |
| Souris | Direction, borne et enigmes via les boutons |
| RECOMMENCER | Repartir au debut du jardin courant |

Le joueur doit etre sur la borne ou sur une case voisine directe. La borne reste verrouillee tant qu'il manque une graine. Un rocher bloque le mouvement. Les ronces restent dangereuses : ne pas insister dans leur direction.

## Les trois jardins

- **Le jardin oublie** : parcours d'initiation sans ronces, choix des deux sources utiles a une graine.
- **Les canaux perdus** : exploration avec ronces et enigme de quantites d'eau.
- **La source des nuages** : chemins differents, carnet et reconstitution du cycle de l'eau.

Les jardins suivants sont debloques apres victoire et la progression est sauvegardee localement.

## Score et rejouabilite

- 100 points par graine.
- Victoire : 500 points + 50 par coeur restant + bonus temps de 0 a 180.
- 3 etoiles : aucune erreur et aucun indice utilise.
- 2 etoiles : au plus une erreur, ou un indice utilise sans plus d'une erreur.
- 1 etoile : deux erreurs et mission terminee.
- Un record par jardin est sauvegarde. Le score d'expedition cumule les jardins enchaines.
- Trois coeurs par jardin : les ronces et mauvaises reponses retirent un coeur. A zero, GameOver et REESSAYER.

## Correspondance avec les exigences

| Consigne | Realisation |
|---|---|
| Unity et C# | Projet Unity 2022.3, scripts C# |
| Theme | Education en solo, avec exploration |
| Quelques enigmes | Trois enigmes integrees aux bornes |
| Menu et instructions | Menu, COMMENT JOUER, credits, quitter |
| Un niveau minimum en solo | Trois cartes jouables |
| Win / GameOver | Victoire par jardin, finale et echec apres trois erreurs |
| Assets | Robot, cartes, plantes et sons integres au projet |
| Responsive, recommande | Interface ajustee au ratio et a la taille de la fenetre |
| Demonstration PC | Export depuis Unity et test sur le PC de validation |
| Desktop / Web / Mobile, recommandes | Export et tests a faire dans Unity, voir ci-dessous |

## Faire le build PC

1. Menu **EcoLab Expedition > Preparer le build**.
2. File > Build Settings > PC, Mac & Linux Standalone.
3. Choisir Linux ou Windows selon la machine de demonstration. Installer le module via Hub s'il manque.
4. Build dans **Builds/PC**. Tester le jeu hors de l'editeur et conserver tous les fichiers du dossier de sortie.
5. Pour WebGL : ajouter le module, changer de plateforme, compiler et servir via HTTP.
6. Pour Mobile : ajouter Android/iOS Build Support, compiler puis tester sur un appareil. Les boutons a l'ecran permettent le deplacement ; le confort et les performances mobiles restent a verifier.

Aucun executable precompile n'est inclus.

## Comprendre et personnaliser le projet

| Fichier | Role |
|---|---|
| GardenGame.cs | Etat du jeu, Nori, decor, mouvement, collecte, risques, score et sauvegarde |
| GardenUI.cs | Menu, interface, instructions, enigmes, victoire et GameOver |
| GardenRules.cs | Conditions des trois enigmes et indices |
| ExpeditionMaps.cs | Trois cartes, titres et carnets |
| EcoLabTools.cs | Raccourcis dans l'editeur, preparation du build, verification C# |

Ton nom est defini dans `GardenGame.Author`. Tu peux modifier les couleurs, les cartes et les textes pour personnaliser ton rendu. Ne change pas le nombre de jardins sans adapter les bornes et la progression.

Format des cartes : `#` rocher, `.` chemin, `@` depart, `*` graine, `!` ronces, `C` borne, `?` carnet. Chaque carte doit avoir trois graines, un depart, une borne et un carnet accessibles.

## GitHub

Creer un depot vide sur GitHub puis, depuis le dossier de ce projet :

```bash
git init
git add .
git commit -m "Initial version of EcoLab Expedition"
git branch -M main
git remote add origin https://github.com/TON_COMPTE/ecolab-expedition.git
git push -u origin main
```

Remplacer TON_COMPTE. Verifier tes informations avec `git config user.name` et `git config user.email`. Le nom affiche dans le jeu et l'auteur des commits sont deux reglages differents. Le `.gitignore` Unity est fourni.

## Tests realises et tests a faire

- `python3 Tools/verify_rules.py` : 99 combinaisons de reponses, une solution correcte par enigme et sequences incompletes refusees.
- `python3 Tools/verify_maps.py` : toutes les graines et les carnets sont accessibles ; la borne peut etre rejointe sans toucher les ronces. Routes calculees dans VALIDATION.md.
- Menu **EcoLab Expedition > Verifier les regles** : execute les tests des enigmes directement en C# dans Unity.
- Unity n'est pas installe dans l'environnement de preparation : compilation, rendu, gameplay et exports restent a tester sur ton poste.

Avant remise : lancer les trois cartes, tester murs et ronces, collecte unique de chaque graine, borne verrouillee avant 3 graines, victoire, GameOver, reessayer, pause, son, sauvegarde du record et build PC. Tester 1440x900, 1280x720 et 1024x768.

La note depend du bareme du professeur, du bon fonctionnement et de ta capacite a expliquer le travail. Voir PRESENTATION.md pour preparer la demonstration.
