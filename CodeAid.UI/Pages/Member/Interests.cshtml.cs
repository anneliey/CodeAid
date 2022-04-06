using CodeAid.UI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member
{
    [BindProperties]
    public class InterestsModel : PageModel
    {
        public string Name { get; set; } = String.Empty;
        public List<InterestModel> AllInterests { get; set; }
        public async Task OnGet()
        {
            InterestManager manager = new();
            AllInterests = await manager.GetInterests();
        }
        //public void OnPost()
        //{
        //    InterestModel interest = new()
        //    {
        //        Name = Interest.Name
        //    }.ToList();
        //}
    }
}
