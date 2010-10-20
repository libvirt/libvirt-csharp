using System;
using Gtk;
using LibvirtBindings;
using System.Runtime.InteropServices;

public partial class MainWindow : Gtk.Window
{
	struct AuthData
    {
        public string user_name;
        public string password;
    }
	
	ListStore domainListStore = new ListStore(typeof(string));
	TreeViewColumn tvcDomains = new TreeViewColumn();
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		
		tvcDomains.Title = "Domains";
		treeview1.AppendColumn(tvcDomains);
		
		treeview1.Model = domainListStore;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
	{
		// Fill a structure to pass username and password to callbacks
        AuthData authData = new AuthData { password = entry3.Text, user_name = entry2.Text };
		
		// Fill a virConnectAuth structure
        virConnectAuth auth = new virConnectAuth
        {
            cbdata = authData,                  // The authData structure
            cb = AuthCallback,                  // the method called by callbacks
            CredTypes = new[]
                            {
                                virConnectCredentialType.VIR_CRED_AUTHNAME,
                                virConnectCredentialType.VIR_CRED_PASSPHRASE
                            }          // The list of credentials types
        };
		
		// Request the connection
        IntPtr conn = virConnect.OpenAuth(entry1.Text, ref auth, 0);
		
		if (conn != IntPtr.Zero)
        {
            // Get the number of defined (not running) domains
            int numOfDefinedDomains = virConnect.NumOfDefinedDomains(conn);
            string[] definedDomainNames = new string[numOfDefinedDomains];
            if (virConnect.ListDefinedDomains(conn, ref definedDomainNames, numOfDefinedDomains) == -1)
            {
				ShowError("Unable to list defined domains");
                return;
            }
			
            // Add the domain names to the listbox
            foreach (string domainName in definedDomainNames)
			{
                AddDomainInTreeView(domainName);
			}
			
            // Get the number of running domains
            int numOfRunningDomain = virConnect.NumOfDomains(conn);
            int[] runningDomainIDs = new int[numOfRunningDomain];
            if (virConnect.ListDomains(conn, runningDomainIDs, numOfRunningDomain) == -1)
            {
                ShowError("Unable to list running domains");
                return;
            }
			
            // Add the domain names to the listbox
            foreach (int runningDomainID in runningDomainIDs)
            {
                IntPtr domainPtr = virDomain.LookupByID(conn, runningDomainID);
                if (domainPtr == IntPtr.Zero)
                {
                    ShowError("Unable to lookup domains by id");
                    return;
                }
                string domainName = virDomain.GetName(domainPtr);
                if (string.IsNullOrEmpty(domainName))
                {
                    ShowError("Unable to get domain name");
                    return;
                }
                AddDomainInTreeView(domainName);
            }
            virConnect.Close(conn);
        }
        else
        {
            ShowError("Unable to connect");
        }
	}
	
	private void ShowError(string errorMessage)
	{
		MessageDialog errorMsg = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, errorMessage);
        errorMsg.Title = "Error";
		if ((ResponseType) errorMsg.Run() == ResponseType.Close)
		{ 
        	errorMsg.Destroy(); 
		}
	}
	
	private void AddDomainInTreeView(string domainName)
	{
		domainListStore.AppendValues(domainName);
		CellRendererText crtDomainCell = new CellRendererText();
		tvcDomains.PackStart(crtDomainCell, true);
		tvcDomains.AddAttribute(crtDomainCell, "text", 0);
	}
	
	private static int AuthCallback(IntPtr creds, uint ncred, IntPtr cbdata)
        {
            // Get the AuthData structure
            AuthData authData = (AuthData)Marshal.PtrToStructure(cbdata, typeof(AuthData));
            int offset = 0;
            int credIndex = 0;

            while (credIndex < ncred)
            {
                IntPtr currentCred = new IntPtr(creds.ToInt32() + offset);

                virConnectCredential cred = (virConnectCredential) Marshal.PtrToStructure(currentCred, typeof(virConnectCredential));
                offset += Marshal.SizeOf(cred);
                switch (cred.type)
                {
                    case virConnectCredentialType.VIR_CRED_AUTHNAME:
                        // Fill the user name
                        cred.Result = authData.user_name;
                        cred.resultlen = (uint)authData.user_name.Length;
                        break;
                    case virConnectCredentialType.VIR_CRED_PASSPHRASE:
                        // Fill the password
                        cred.Result = authData.password;
                        cred.resultlen = (uint)authData.password.Length;
                        break;
                    default:
                        return -1;
                }
                // Write structure to the unmanaged address
                Marshal.StructureToPtr(cred, currentCred, true);

                credIndex++;
            }
            return 0;
        }
	
	
}

