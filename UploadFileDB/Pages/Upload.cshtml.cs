using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UploadFileDB.Models;

namespace UploadFileDB.Pages
{
    public class UploadModel : PageModel
    {
        private readonly DatabaseContext _databaseContext;

        public UploadModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public int? myId { get; set; }

        [BindProperty]
        public IFormFile file { get; set; }

        [BindProperty]
        public int? Id { get; set; }

        public void OnGet(int? id)
        {
            myId = id;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (file != null)
            {
                if (file.Length > 0 && file.Length < 300000)
                {
                    var myInvoice = _databaseContext.Invoices.FirstOrDefault(
                        x => x.Id == Id);

                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        myInvoice.Attachment = target.ToArray();
                    }

                    _databaseContext.Invoices.Update(myInvoice);

                    await _databaseContext.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
