using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void JoiningTableData_Load(object sender, EventArgs e)
        {
            // Entity Framework DbContext
            var dbcontext = new BooksExamples.BooksEntities();

            var authorByTitle =
                      from title in dbcontext.Titles
                      orderby title.Title1
                      select new
                      {
                          Name = title.Title1,
                          Author =
                            from a in title.Authors
                            select a
                      };

            outputTextBox.AppendText("\r\n\r\na. Get a count of all the authors grouped by title, sorted by title. It should display a title and number of authors who have written that title. ");

            foreach (var title in authorByTitle)
            {
             

                outputTextBox.AppendText($"\r\n\t\t{title.Name,-10}\r\t{title.Author.Count()}");
             

                //foreach (var author in title.Author)
                //{

                //    outputTextBox.AppendText($"\r\n\t\t{author.LastName,-10} {author.FirstName,-10}");
                //}
            }

            var titlesByAuthor =
               from author in dbcontext.Authors
               orderby author.FirstName
               select new
               {
                    author,
                     titles =
                     from a in author.Titles
                     orderby a.Title1
                     select a
               };

                outputTextBox.AppendText("\r\n\r\nb.  Get a list of all the titles grouped by author name, sorted by author; for a given author name sort the titles alphabetically. ");
      
                foreach (var author in titlesByAuthor)
                {
                    outputTextBox.AppendText($"\r\n{author.author.FirstName} {author.author.LastName}");

                foreach (var title in author.titles)
                {

                    outputTextBox.AppendText($"\r\n\t\t{title.Title1,-10}");
                }
            }
        }
    }
}
