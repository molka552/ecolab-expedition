// Pure puzzle rules, shared by the game and the editor verification command.
public static class GardenRules
{
    public static bool Light(bool sun, bool water, bool smoke) { return sun && water && !smoke; }
    public static bool Water(int left, int middle, int right) { return left == 1 && middle == 2 && right == 3; }
    public static bool Cycle(int[] input) { return input.Length == 3 && input[0] == 0 && input[1] == 1 && input[2] == 2; }
    public static readonly string[] Names = { "Le reveil de la graine", "La juste dose", "Le cycle de l'eau" };
    public static readonly string[] Stories = {
        "Une petite graine attend ton aide. Choisis ce qui lui permet de pousser.",
        "La serre manque d'eau. Regle les trois vannes pour remplir le reservoir sans gaspiller.",
        "Relance le cycle naturel pour que la serre puisse vivre toute seule."
    };
    public static readonly string[] Clues = {
        "La graine veut deux sources utiles. Une source l'eclaire, une autre abreuve ses racines. Ecarte ce qui pollue.",
        "Gauche : 1 litre. Milieu : le double de gauche. Total des trois vannes : 6 litres.",
        "Retrouve le cycle : l'eau monte, se rassemble dans le ciel, puis revient au sol. Quel element provoque chaque etape ?"
    };
    public static readonly string[] Hints = {
        "Allume SOLEIL et EAU. Laisse FUMEE eteinte, puis clique sur VERIFIER.",
        "Regle GAUCHE sur 1, MILIEU sur 2 et DROITE sur 3. Chaque clic ajoute 1 ; apres 3, on revient a 0.",
        "Clique sur SOLEIL, puis NUAGE, puis PLUIE."
    };
}
