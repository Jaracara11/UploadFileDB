using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadFileDB.Models;

namespace UploadFileDB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DatabaseContext _databaseContext;

        public IndexModel(ILogger<IndexModel> logger, DatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        public IList<Invoices> Invoices { get; set; }

        public void OnGet()
        {
            Invoices = _databaseContext.Invoices.ToList();
        }

        public async Task<IActionResult> OnPostDownloadAsync(int? id)
        {
            var myInvoice = await _databaseContext.Invoices.FirstOrDefaultAsync(
                x => x.Id == id);

            if (myInvoice == null)
            {
                return NotFound();
            }
            if (myInvoice.Attachment == null)
            {
                return Page();
            }
            else
            {
                byte[] byteArray = myInvoice.Attachment;
                var mimeType = "application/pdf";

                return new FileContentResult(byteArray, mimeType)
                {
                    FileDownloadName = $"Invoice_Number_{myInvoice.Number}.pdf"
                };
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            var myInvoice = await _databaseContext.Invoices.FirstOrDefaultAsync(
                x => x.Id == id);

            if (myInvoice == null)
            {
                return NotFound();
            }
            if (myInvoice.Attachment == null)
            {
                return Page();
            }
            else
            {
                myInvoice.Attachment = null;

                _databaseContext.Update(myInvoice);

                await _databaseContext.SaveChangesAsync();
            }

            Invoices = await _databaseContext.Invoices.ToListAsync();

            return Page();
        }
    }
}
