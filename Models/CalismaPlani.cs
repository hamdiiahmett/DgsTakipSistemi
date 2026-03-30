using System.ComponentModel.DataAnnotations;

namespace DgsTakipSistemi.Models
{
    public class CalismaPlani
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string KullaniciId { get; set; }

        [Required(ErrorMessage = "Lütfen bir görev veya konu adı giriniz.")]
        [Display(Name = "Görev / Konu")]
        public string GorevAdi { get; set; }

        [Required(ErrorMessage = "Lütfen gün seçiniz.")]
        [Display(Name = "Hangi Gün?")]
        public string Gun { get; set; }

        [Display(Name = "Tamamlandı mı?")]
        public bool TamamlandiMi { get; set; } = false;
    }
}