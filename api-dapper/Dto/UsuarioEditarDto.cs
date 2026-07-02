namespace api_dapper.Dto
{
    public class UsuarioEditarDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public double Salario { get; set; }
        public string CPF { get; set; }
        public bool Ativo { get; set; }
    }
}
