namespace TFHKA.Adjuntos.listado.Transversal.Comun
{
    public class Respuesta<T>
    {
        public T Datos { get; set; }
        public bool EsExitosa { get; set; }
        public bool TraeDatos { get; set; }
        public string Mensaje { get; set; }
    }
}