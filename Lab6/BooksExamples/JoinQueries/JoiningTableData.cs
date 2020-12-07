// JoiningTableData.cs
// Using LINQ to perform a join and aggregate data across tables.
using System;
using System.Linq;
using System.Windows.Forms;

namespace JoinQueries
{
    public partial class JoiningTableData : Form
    {
        public JoiningTableData()
        {
            InitializeComponent();
        } // end constructor

        private void JoiningTableData_Load(object sender, EventArgs e)
        {
            // Entity Framework DBContext
            BooksEntities dbcontext =
               new BooksEntities();

            var titleAuthorsOrderByTitle =
                from book in dbcontext.Titles
                from author in book.Authors
                orderby book.Title1
                select new
                {
                    book.Title1,
                    author.FirstName,
                    author.LastName
                };

            outputTextBox.AppendText("Titles and Authors:");

            foreach (var element in titleAuthorsOrderByTitle)
            {
                outputTextBox.AppendText(
                   String.Format("\r\n\t{0,-10} {1,-10} {2,-10}",
                      element.Title1, element.FirstName, element.LastName));
            };

            var titleAuthorsOrderByTitleLastFirstName =
                from book in dbcontext.Titles
                from author in book.Authors
                orderby book.Title1, author.LastName, author.FirstName
                select new
                {
                    book.Title1,
                    author.FirstName,
                    author.LastName
                };

            outputTextBox.AppendText("\r\n\r\nAuthors and titles with authors sorted for each title:");

            foreach (var element in titleAuthorsOrderByTitleLastFirstName)
            {
                outputTextBox.AppendText(
                   String.Format("\r\n\t{0,-10} {1,-10} {2,-10}",
                      element.Title1, element.FirstName, element.LastName));
            }

            var titlesGroupedByAuthor =
                from book in dbcontext.Titles
                orderby book.Title1
                select new
                {
                    Titles = book.Title1,
                    Names =
                        from author in book.Authors
                        orderby author.LastName, author.FirstName
                        select author.FirstName + " " + author.LastName 
                };

            outputTextBox.AppendText("\r\n\r\nTitles grouped by author:");

            foreach (var book in titlesGroupedByAuthor)
            {
                outputTextBox.AppendText("\r\n\t" + book.Titles + ":");

                foreach (var author in book.Names)
                {
                    outputTextBox.AppendText("\r\n\t\t" + author);
                }
            }
             
                        
               
        } // end method JoiningTableData_Load
    } // end class JoiningTableData
} // end namespace JoinQueries

