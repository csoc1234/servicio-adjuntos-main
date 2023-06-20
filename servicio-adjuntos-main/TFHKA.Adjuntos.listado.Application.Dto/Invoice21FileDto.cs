namespace TFHKA.Adjuntos.listado.Application.Dto
{
    public class Invoice21FileDto
    {
        public int Id { get; set; }
        public int IdInvoice { get; set; }
        public string NameFile { get; set; }
        public string PathFile { get; set; }
        //public string Link { get; set; }
        public string Format { get; set; }
        public string NameDisplay { get; set; }
        public bool? Active { get; set; }
        public string CrcSha1 { get; set; }
        public int? Size { get; set; }
        public int? Type { get; set; }
    }
}