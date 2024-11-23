namespace API.Model
{
    public class Criador
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        List<Conteudo> Conteudos { get; set; }
    }
}
