using Models;
using Xceed.Words.NET;
using Xceed.Document.NET;
using System;

namespace BusinessLogicLayer.Services
{
    public class OfferService : IOfferService
    {
        public void CreateWordOffer(Client client, DateOnly date)
        {
            var doc = DocX.Create($"{client.UNP}-Дата{date.ToString("yyyy-MM-dd")}.docx");

            var paragraph = doc.InsertParagraph();
            paragraph.Append("Уважаемые: ").Append(client.Name).Append(", информируем вас о том, что вы не погасили кредит");
            paragraph.AppendLine("Ваша задолженность:");

            var table = doc.AddTable(2, 3);
            table.AutoFit = AutoFit.Window;

            table.Rows[0].Cells[0].Paragraphs[0].Append("Наименование").Bold();
            table.Rows[0].Cells[1].Paragraphs[0].Append("УНП").Bold();
            table.Rows[0].Cells[2].Paragraphs[0].Append("Сумма").Bold();

            table.Rows[1].Cells[0].Paragraphs[0].Append(client.Name);
            table.Rows[1].Cells[1].Paragraphs[0].Append(client.UNP.ToString());
            table.Rows[1].Cells[2].Paragraphs[0].Append($"{client.Sum} р.");

            doc.InsertTable(table);

            doc.InsertParagraph("\nПлатите и фигней не занимайтесь:");

            doc.Save();
        }
    }
}
