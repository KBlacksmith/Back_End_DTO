namespace WebAPI_Peliculas.Entidades
{
    public class DirectorPelicula
    {
        public int DirectorId { get; set; }
        public int PeliculaId { get; set; }
        public int Orden { get; set; }
        public Director Director { get; set; }
        public Pelicula Pelicula { get; set; }
    }
}
