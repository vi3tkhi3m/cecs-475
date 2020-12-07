using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace Lab4Wpf
{
    /// <summary>
    /// Interaction logic for ExpenseReportPage.xaml
    /// </summary>
    
    public partial class ExpenseReportPage : Page
    {
        public ExpenseReportPage()
        {
            InitializeComponent();
        }

        public ExpenseReportPage(object data):this()
        {
            this.DataContext = data;
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            FrameworkElementAutomationPeer.FromElement(LiveRegion)?.RaiseAutomationEvent(AutomationEvents.LiveRegionChanged);
            FrameworkElementAutomationPeer.FromElement(NameLiveRegion)?.RaiseAutomationEvent(AutomationEvents.LiveRegionChanged);
        }
    }
  
}
