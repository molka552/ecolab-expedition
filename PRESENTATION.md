# Presenter EcoLab Expedition

## Pitch

EcoLab Expedition est une aventure educative en 3D. Le joueur guide Nori, un robot jardinier, pour retrouver des graines et restaurer trois jardins en resolvant des enigmes sur les besoins des plantes, les quantites d'eau et le cycle de l'eau.

## Demonstration en quatre minutes

1. Montrer le menu et les commandes.
2. Dans le premier jardin, ramasser une graine et lire le carnet. Montrer le compteur de graines et le score.
3. Approcher la borne avant trois graines : elle explique pourquoi elle reste verrouillee.
4. Terminer la collecte et ouvrir l'enigme avec E. Resoudre, montrer victoire, score et etoiles.
5. Dans le second jardin, montrer une case rouge et expliquer les coeurs. Utiliser PAUSE.
6. Montrer la progression vers les trois cartes, puis expliquer le GameOver et la possibilite de recommencer.
7. Presenter le build PC et quelques fichiers de code que tu as etudies ou modifies.

## Questions techniques a preparer

- **Comment le robot se deplace ?** Une position logique sur grille est validee, puis le modele 3D se deplace progressivement vers cette position.
- **Pourquoi le robot ne traverse pas les rochers ?** La prochaine case doit appartenir a l'ensemble des cases praticables.
- **Comment la collecte evite les doublons ?** Une graine ramassee est retiree du dictionnaire des objets collectables.
- **Comment est geree la progression ?** Une machine a etats separe menu, exploration, enigme, pause, victoire et echec.
- **Que sauvegarde le jeu ?** Le jardin debloque et le meilleur score par jardin, avec PlayerPrefs.
- **Comment sont verifiees les enigmes ?** Des fonctions de regles renvoient vrai uniquement pour la bonne combinaison.
- **Comment est calcule le score ?** Collecte, victoire, coeurs restants et bonus temps positif.
- **Comment tester une nouvelle carte ?** Verifier les chemins, les objets obligatoires et le build ; le solveur Python peut verifier les acces sans ronces.

## Solutions des bornes

1. Soleil ON, Eau ON, Fumee OFF, puis VERIFIER.
2. Gauche 1, Milieu 2, Droite 3, puis VERIFIER.
3. Soleil, Nuage, Pluie.

Les solutions de navigation sont dans VALIDATION.md. Appuyer une touche a la fois en attendant la fin du deplacement.

## Personnalisation utile

Changer au moins une carte, un texte de carnet ou une couleur, puis savoir expliquer ce changement. Garder un historique Git de tes modifications et comprendre le code presente. Expliquer honnetement ton travail et les aides utilisees selon les regles du cours.
