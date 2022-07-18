namespace Modelo.Share
{
    public static class AppConstantes
    {
        public static class Api
        {
            public static class Erros
            {
                public const string NaoEncontrado = "Objeto não encontrado";

                public static class Usuario
                {
                    public const string CpfInvalido = "Cpf não é valido";
                    public const string DadosInvalidos = "CPF ou Email já cadastrados.";
                }
                public static class Venda
                {
                    public const string EstoqueInsuficiente = "Pelo menos um produto não tem a quantidade solitada no estoque.";
                }
                                          
            }

            public static class Sucesso
            {
                public const string Cadastro = "Cadastro realizado com sucesso.";
                public const string Busca = "Busca realizada com sucesso.";
            }
        }
    }
}