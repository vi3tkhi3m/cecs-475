using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Stocks
{
    public class StockBroker
    {
        public string BrokerName { get; set; }
        public List<Stock> stocks = new List<Stock>();

        public static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
        readonly string docPath = @"C:\Users\khiem\Dropbox\CSULB\CECS 475\Labs\Lab3\Lab3_output.txt";
        public string titles = "Broker".PadRight(10) + "Stock".PadRight(15) + "Value".PadRight(10) + "Changes".PadRight(10) + "Date and Time";

        /// <summary>
        /// The stockbroker object
        /// </summary>
        /// <param name="brokerName">The stockbroker's name</param>
        public StockBroker(string brokerName)
        {
            BrokerName = brokerName;
        }

        /// <summary>
        /// Adds stock objects to the stock list
        /// </summary>
        /// <param name="stock">Stock object</param>
        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
            stock.StockEvent += EventHandler;
        }
        /// <summary>
        /// The eventhandler that raises the event of a change
        /// </summary>
        /// <param name="sender">The sender that indicated a change</param>
        /// <param name="e">Event arguments</param>
        void EventHandler(Object sender, EventArgs e)
        {
            myLock.EnterWriteLock();
            try
            {
                Stock newStock = (Stock)sender;

                string displayCurrentValue = newStock.CurrentValue.ToString();
                string displayNumChanges = newStock.NumChanges.ToString();
                string statement = BrokerName.PadRight(10) + newStock.StockName.PadRight(15)
                    + displayCurrentValue.PadRight(10) + displayNumChanges.PadRight(10);

                Console.WriteLine(statement);

                WriteStockToFile(newStock, displayCurrentValue, displayNumChanges);
            }
            catch (Exception ex)
            {
                Console.WriteLine("EventHandler failed! Message: {0}", ex.Message);
            }
            finally
            {
                myLock.ExitWriteLock();
            }
        }

        private void WriteStockToFile(Stock newStock, string displayCurrentValue, string displayNumChanges)
        {
            using (StreamWriter outputFile = new StreamWriter(docPath, true))
            {
                outputFile.WriteLine(DateTime.Now.ToString().PadRight(30) + newStock.StockName.PadRight(15) +
                        displayCurrentValue.PadRight(15) + displayNumChanges);
            }
        }
    }
}
