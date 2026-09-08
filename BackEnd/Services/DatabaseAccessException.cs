namespace BackEnd.Services;

// Indica que a pesquisa falhou ao aceder à base de dados, não por o Pokémon não existir.
public class DatabaseAccessException : Exception
{
    public DatabaseAccessException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
