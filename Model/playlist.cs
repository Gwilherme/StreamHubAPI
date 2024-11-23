using API;
using System;

public class Playlist
{
	public int ID { get;set;}
	public string Nome { get;set;}
	public Usuario Usuario { get; set; }
	public List<Conteudo> conteudos { get; set; }

}
