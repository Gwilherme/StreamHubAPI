using API.Model;
using System;
namespace API
{
	public class Conteudo
	{
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public Criador Criador { get; set; }
        string comentarios { get; set; }
		int like { get; set; }
	}
}
