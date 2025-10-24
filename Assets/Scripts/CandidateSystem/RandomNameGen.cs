using UnityEngine;

public static class RandomNameGen {
    private static readonly string[] FirstNamePool = {
        "Raian",
        "Ryan",
        "Kian",
        "John",
        "Jack",
        "Jane",
        "Jill",
        "Joe",
        "Farquad",
        "Frank",
        "Ohma",
        "Billy",
        "Esmerelda",
        "Spike",
        "Edward",
        "El",
        "Louis",
        "James",
        "Biscuit",
        "Hideki",
        "Imai",
        "Seishuu",
        "Kaede",
        "Alexandra",
        "Rei",
        "Light",
        "L",
        "Chika",
        "Kirill",
        "Andre",
        "Ivan",
        "Barou"
    };

    private static readonly string[] LastNamePool = {
        "D. Monkey",
        "Kure",
        "Cloutier",
        "Von Agnes",
        "Mikazuchi",
        "Wu",
        "Tokita",
        "Niko",
        "Yamashita",
        "Smith",
        "Edwards",
        "Suzuki",
        "Cosmo",
        "Malone",
        "Aguero",
        "Messi",
        "Ronaldo",
        "Grant",
        "Gonzales",
        "Floyd",
        "Wright",
        "Beckman",
        "Trafalgar",
        "Thompson",
        "Nelson",
        "Scott",
        "Knapp",
        "Bautista",
        "Dixon",
        "Zimmerman",
        "Muller",
        "Schulz",
        "Schneider",
        "Wagner",
        "Petrov",
        "Ivanov",
        "Dimitriev",
        "Karmazov"
    };

    public static string GetRandomFirstName() {
        return FirstNamePool[Random.Range(0, FirstNamePool.Length)];
    }

    public static string GetRandomLastName() {
        return LastNamePool[Random.Range(0, LastNamePool.Length)];
    }
}