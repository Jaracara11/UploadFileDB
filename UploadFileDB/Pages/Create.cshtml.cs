using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UploadFileDB.Models;

namespace UploadFileDB.Pages
{
    public class CreateModel : PageModel
    {
        public readonly DatabaseContext _databaseContext;

        public CreateModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public Invoices Invoices { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _databaseContext.Invoices.Add(Invoices);

            await _databaseContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
