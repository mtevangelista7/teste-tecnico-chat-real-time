namespace TesteTecnicoDiscord.Domain.Util;

public static class Messages
{
    public static class Errors
    {
        public const string MessageNull = "A mensagem não pode ser nula.";
        public const string GuildNotFound = "Guild com ID {0} não encontrada.";
        public const string ChannelNotFound = "Canal com ID {0} não encontrado.";
        public const string UserNotFound = "Usuário com o username {0} não encontrado.";
        public const string ItemNotFound = "Item(s) {0} não encontrado(s).";
        public const string UngeneratedToken = "Não foi possível criar o token para o usuário {0}.";
        public const string UserCreationFailed = "Não foi possível criar o usuário.";
        public const string EntityCannotBeNull = "A entidade não pode ser nula.";
    }
}