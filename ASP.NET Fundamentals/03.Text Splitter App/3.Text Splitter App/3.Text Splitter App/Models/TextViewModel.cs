using System.ComponentModel.DataAnnotations;

namespace _3.Text_Splitter_App.Models
{
    public class TextViewModel
    {
        [Required]
        [StringLength (30, MinimumLength = 4)]
        public string Text { get; set; } = null!;

        public string SplitText { get; set; } = null!;
    }
}
