using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMDesktop.ViewModels;

public class StatusViewModel : Screen
{
    public string Header { get; set; }
    public string Message { get; set; }

    public void UpdateMessage(string header, string message)
    {
        Header = header;
        Message = message;

        NotifyOfPropertyChange(() => Header);
        NotifyOfPropertyChange(() => Message);
    }

    public async Task Close()
    {
        await TryCloseAsync();
    }
}
