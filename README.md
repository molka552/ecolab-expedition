<div align="center">

# 🌿 EcoLab Expedition

### Une graine trouvée. Un jardin qui recommence.

**Une aventure éducative en 3D avec Nori, le petit robot jardinier.**

![Unity](https://img.shields.io/badge/Unity-2022.3.62f2-222222?logo=unity&logoColor=white)
![C Sharp](https://img.shields.io/badge/Langage-C%23-684D95)
![Genre](https://img.shields.io/badge/Genre-Aventure_éducative-2E7D32)
![Mode](https://img.shields.io/badge/Mode-Solo-1976D2)
![Licence](https://img.shields.io/badge/Licence-MIT-D9A441)

**Un projet de [Molka Boughanmi](https://github.com/molka552)**

[🚀 Installation](#-installation) · [🎮 Comment jouer](#-comment-jouer) · [🌍 Les jardins](#-les-trois-jardins) · [📦 Export](#-exporter-le-jeu) · [🧪 Tests](#-tests-et-validation)

</div>

---

## 🌱 L’aventure

Trois jardins oubliés attendent de retrouver la vie.

Aux commandes de **Nori**, explore les chemins, récupère les graines dorées et découvre les observations laissées dans les carnets. Active ensuite les bornes pour résoudre des énigmes sur **les plantes, l’eau et les cycles naturels**.

Chaque jardin restauré ouvre la voie au suivant. À toi de prendre soin du vivant !

## ✨ Fonctionnalités

| | Dans le jeu |
|---|---|
| 🤖 **Exploration** | Un robot contrôlable sur une grille et trois cartes à parcourir |
| 🌰 **Collecte** | Trois graines dorées à retrouver dans chaque jardin |
| 📖 **Observation** | Des carnets contenant des indices utiles |
| 🧩 **Réflexion** | Trois énigmes différentes intégrées aux bornes |
| ❤️ **Défi** | Trois cœurs et des ronces à éviter |
| 🏆 **Progression** | Points, étoiles, jardins à débloquer et records locaux |
| 🌸 **Récompense** | Une floraison à la réussite de chaque jardin |
| 🔊 **Confort** | Pause, sons désactivables, indices et nouvelle tentative |
| 🖥️ **Interface** | Menu, instructions, crédits, victoire et GameOver |

## 🚀 Installation

### Prérequis

- **Unity Hub**
- **Unity 2022.3.62f2**
- **Git**, pour cloner le dépôt — ou télécharger le ZIP depuis GitHub

### Ouvrir le projet

```bash
git clone https://github.com/molka552/ecolab-expedition.git
```

1. Dans **Unity Hub**, choisir **Add → Add project from disk**.
2. Sélectionner le dossier contenant directement `Assets`, `Packages` et `ProjectSettings`.
3. Ouvrir le projet avec **Unity 2022.3.62f2** et attendre la compilation.
4. Ouvrir [Assets/Scenes/Main.unity](Assets/Scenes/Main.unity).
5. Cliquer sur **Play ▶**, puis sur **PARTIR EN EXPÉDITION** dans l’onglet **Game**.

> 💡 Le décor est construit au lancement : une scène vide avant **Play** est normale. Aucun asset externe n’est nécessaire. Double-cliquer sur l’onglet **Game** permet d’agrandir l’affichage.

## 🎮 Comment jouer

1. 🧭 **Explore** le jardin avec Nori.
2. 🌰 **Ramasse les trois graines dorées** en marchant dessus.
3. 📖 **Lis le carnet blanc** pour obtenir une observation utile.
4. 🌵 **Contourne les ronces rouges** pour préserver tes cœurs.
5. 💡 **Rejoins la borne lumineuse**, puis appuie sur **E**.
6. 🧩 **Résous l’énigme**, admire la floraison et découvre le jardin suivant.

> ⏱️ **Pas de compte à rebours !** Le temps sert uniquement à calculer un bonus. Tu peux réfléchir à ton rythme.

### ⌨️ Commandes

| Commande | Action |
|---|---|
| **↑ ↓ ← →** / **WASD** / **ZQSD** | Avancer d’une case |
| **E** | Activer une borne à proximité |
| **Échap** | Mettre en pause ou reprendre |
| **Souris** | Utiliser les boutons de déplacement et résoudre les énigmes |
| **RECOMMENCER** | Repartir au début du jardin courant |
| **INDICE** | Afficher une aide pour l’énigme |

Attendre la fin de chaque petit déplacement avant le suivant. Une borne s’active depuis sa case ou une case voisine directe, après la collecte des **trois graines**.

Les rochers bloquent le passage. Entrer dans une ronce retire un cœur et laisse Nori sur sa case : insister dans cette direction peut donc épuiser les cœurs.

## 🌍 Les trois jardins

| Jardin | Exploration | Énigme |
|---|---|---|
| 🌱 **01 · Le jardin oublié** | Un parcours d’initiation sans ronces | Identifier les deux sources utiles à une graine |
| 💧 **02 · Les canaux perdus** | Des ronces et des passages à contourner | Répartir une quantité d’eau à partir d’indices |
| ☁️ **03 · La source des nuages** | Un nouvel itinéraire et un carnet à retrouver | Reconstituer le cycle de l’eau |

🔓 Les jardins se débloquent après chaque victoire. La progression est enregistrée localement.

## 🏆 Points, étoiles et records

### Le score

| Action | Récompense |
|---|---:|
| 🌰 Ramasser une graine | **+100 points** |
| 🌸 Restaurer un jardin | **+500 points** |
| ❤️ Garder un cœur jusqu’à la victoire | **+50 points par cœur** |
| ⏱️ Terminer rapidement | **Bonus de 0 à 180 points** |

Le score d’expédition cumule les jardins enchaînés. Un **meilleur score par jardin** est conservé localement.

### Les étoiles

| Résultat | Condition |
|---|---|
| ⭐⭐⭐ | Aucune erreur et aucun indice utilisé |
| ⭐⭐ | Au plus une erreur, sans remplir la condition des trois étoiles |
| ⭐ | Deux erreurs et mission terminée |

Une ronce ou une mauvaise réponse retire un cœur. À **zéro cœur**, l’écran **GameOver** permet de réessayer.

## 📦 Exporter le jeu

### 🖥️ Desktop — Linux ou Windows

1. Choisir **EcoLab Expedition → Préparer le build**.
2. Ouvrir **File → Build Settings → PC, Mac & Linux Standalone**.
3. Sélectionner **Linux** ou **Windows**, selon le PC cible.
4. Installer le module correspondant dans Unity Hub s’il manque.
5. Construire le jeu dans `Builds/PC`, puis le tester **hors de l’éditeur**.

> 📁 Pour partager le jeu, conserver **tout le dossier de sortie**, pas seulement le fichier exécutable.

### 🌐 WebGL

Installer **WebGL Build Support**, sélectionner **WebGL → Switch Platform**, puis utiliser **Build And Run** pour tester dans le navigateur via le serveur local de Unity.

### 📱 Mobile

Installer le module **Android** ou **iOS**, compiler et tester sur un appareil. Des commandes à l’écran sont disponibles ; la lisibilité, le confort tactile et les performances restent à vérifier.

> ℹ️ Ce dépôt contient le **projet source**. Aucun exécutable précompilé n’est inclus. Les exports Web et Mobile doivent être validés sur leurs plateformes.

## 🛠️ Organisation du code

| Fichier | Responsabilité |
|---|---|
| [GardenGame.cs](Assets/Scripts/GardenGame.cs) | États du jeu, robot, décor, déplacements, collecte, dangers, score et sauvegarde |
| [GardenUI.cs](Assets/Scripts/GardenUI.cs) | Menu, interface, commandes, énigmes, victoire et GameOver |
| [GardenRules.cs](Assets/Scripts/GardenRules.cs) | Conditions de réussite et indices des énigmes |
| [ExpeditionMaps.cs](Assets/Scripts/ExpeditionMaps.cs) | Cartes, noms des jardins et carnets |
| [EcoLabTools.cs](Assets/Editor/EcoLabTools.cs) | Outils de l’éditeur, préparation du build et vérification des règles |

### 🎨 Personnalisation

- **Identité** : `GardenGame.Author`
- **Décor et couleurs** : `GardenGame.cs`
- **Textes et interface** : `GardenUI.cs`
- **Énigmes** : `GardenRules.cs`
- **Parcours** : `ExpeditionMaps.cs`

### 🗺️ Légende des cartes

| Symbole | Élément |
|:---:|---|
| `#` | Rocher |
| `.` | Chemin |
| `@` | Départ de Nori |
| `*` | Graine |
| `!` | Ronces |
| `C` | Borne |
| `?` | Carnet |

Chaque carte doit contenir **trois graines, un départ, une borne et un carnet accessibles**. Modifier le nombre de jardins demande aussi d’adapter la progression.

## 🧪 Tests et validation

### Vérifications automatisées

```bash
python3 Tools/verify_rules.py
python3 Tools/verify_maps.py
```

- ✅ **99 combinaisons de réponses** : une solution correcte par énigme et rejet des séquences incomplètes.
- ✅ **Accessibilité des cartes** : toutes les graines et les carnets peuvent être atteints, puis la borne rejointe, sans toucher les ronces.
- 🔎 Dans Unity : **EcoLab Expedition → Vérifier les règles** exécute les vérifications des énigmes en C#.

Les itinéraires calculés figurent dans [VALIDATION.md](VALIDATION.md).

### Checklist avant une démonstration

- [ ] Parcourir les trois jardins.
- [ ] Vérifier les rochers, les ronces et la collecte unique des graines.
- [ ] Tester le verrouillage de la borne avant trois graines.
- [ ] Vérifier victoire, GameOver, pause et nouvelle tentative.
- [ ] Tester les sons et la sauvegarde des records.
- [ ] Contrôler l’affichage en **1440 × 900**, **1280 × 720** et **1024 × 768**.
- [ ] Lancer un build PC hors de Unity.
- [ ] Tester séparément les exports Web ou Mobile s’ils sont présentés.

> Les vérifications automatiques portent sur les règles et les cartes. Elles ne remplacent pas les tests de compilation, de rendu et de gameplay dans Unity.

## 🎓 Objectifs du projet

| Consigne | Réalisation |
|---|---|
| Unity / C# | Projet Unity 2022.3 et scripts C# |
| Thème autorisé | Éducation en solo, avec exploration |
| Énigmes | Trois défis intégrés aux bornes |
| Menu et instructions | Menu, guide de jeu, crédits et quitter |
| Un niveau minimum en solo | Trois cartes jouables |
| Win / GameOver | Victoire par jardin, finale et échec après trois erreurs |
| Assets | Robot, cartes, plantes et sons intégrés |
| Interface adaptable | Mise à l’échelle ; résolutions à tester |
| Démonstration PC | Export et validation sur la machine de présentation |
| Web / Mobile — recommandations | Export et tests à réaliser sur les plateformes cibles |

🗣️ Le guide de démonstration est disponible dans [PRESENTATION.md](PRESENTATION.md).

## 🔄 Contribuer au dépôt

Après une modification locale :

```bash
git add .
git commit -m "Describe your changes"
git push
```

Le `.gitignore` exclut les dossiers temporaires de Unity. Conserver également les builds générés hors des fichiers suivis par Git.

## 📄 Licence

Code distribué sous [licence MIT](LICENSE). Unity reste soumis à ses propres conditions de licence.

---

<div align="center">

**🌿 Explorer · Observer · Comprendre · Restaurer**

**Molka Boughanmi · EcoLab Expedition**

</div>
